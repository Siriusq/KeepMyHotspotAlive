using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Drawing;
using System.Diagnostics;

namespace KeepMyHotspotAlive
{
    class Tray : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private System.Timers.Timer pingTimer;
        private string gatewayIP;
        private const int pingInterval = 30000; // 30秒

        public Tray()
        {
            // 初始化系统托盘图标
            trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Application,
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true
            };

            // 获取默认网关IP
            gatewayIP = GetGatewayIP();
            if (string.IsNullOrEmpty(gatewayIP))
            {
                ShowErrorAndExit("无法自动检测到iPhone热点网关IP，请确认已连接热点");
                return;
            }

            // 初始化定时器
            InitializeTimer();

            // 创建上下文菜单
            var pauseResumeItem = new ToolStripMenuItem("暂停", null, OnPauseResume);
            var exitItem = new ToolStripMenuItem("退出", null, OnExit);
            trayIcon.ContextMenuStrip.Items.AddRange(new ToolStripItem[] { pauseResumeItem, exitItem });

            // 首次立即执行一次Ping
            Task.Run(() => PerformPing());
        }

        private string GetGatewayIP()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                {
                    foreach (GatewayIPAddressInformation gateway in ni.GetIPProperties().GatewayAddresses)
                    {
                        if (gateway.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return gateway.Address.ToString();
                        }
                    }
                }
            }
            return null;
        }

        private void InitializeTimer()
        {
            pingTimer = new System.Timers.Timer(pingInterval);
            pingTimer.Elapsed += async (s, e) => await PerformPing();
            pingTimer.AutoReset = true;
            pingTimer.Start();
        }

        private async Task PerformPing()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(gatewayIP, 1000);
                    UpdateTooltip(reply.Status == IPStatus.Success ?
                                 $"最后Ping: {DateTime.Now:T} 成功" :
                                 $"最后Ping: {DateTime.Now:T} 失败");
                }
            }
            catch
            {
                UpdateTooltip($"最后Ping: {DateTime.Now:T} 错误");
            }
        }

        private void UpdateTooltip(string text)
        {
            if (trayIcon != null)
            {
                trayIcon.Text = text;
            }
        }

        private void OnPauseResume(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;

            if (pingTimer.Enabled)
            {
                pingTimer.Stop();
                menuItem.Text = "继续";
                trayIcon.Text = "已暂停";
            }
            else
            {
                pingTimer.Start();
                menuItem.Text = "暂停";
                Task.Run(() => PerformPing()); // 立即执行一次Ping
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            pingTimer?.Dispose();
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void ShowErrorAndExit(string message)
        {
            MessageBox.Show(message, "Hotspot Keeper",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
