using KeepMyHotspotAlive.Properties;
using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // 初始化语言
            LanguageManager.Initialize();

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
                ShowErrorAndExit(Resources.ErrorGatewayMsg, Resources.ErrorGatewayTitle);
                return;
            }

            // 初始化定时器
            InitializeTimer();

            // 创建上下文菜单
            var pauseResumeItem = new ToolStripMenuItem(Resources.MenuPause, null, OnPauseResume);
            var exitItem = new ToolStripMenuItem(Resources.MenuExit, null, OnExit);
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
                                 $"{DateTime.Now:T} Ping {gatewayIP} {Resources.PingSuccess}" :
                                 $"{DateTime.Now:T} Ping {gatewayIP} {Resources.PingFailed}");
                }
            }
            catch
            {
                UpdateTooltip($"{DateTime.Now:T} Ping {gatewayIP} {Resources.PingError}");
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
                menuItem.Text = Resources.MenuResume;
                trayIcon.Text = Resources.MenuPaused;
            }
            else
            {
                pingTimer.Start();
                menuItem.Text = Resources.MenuPause;
                Task.Run(() => PerformPing()); // 立即执行一次Ping
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            pingTimer?.Dispose();
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void ShowErrorAndExit(string message, string title)
        {
            MessageBox.Show(message, title,
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            pingTimer?.Dispose();
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}
