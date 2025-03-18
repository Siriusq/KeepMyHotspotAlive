using KeepMyHotspotAlive.Properties;
using System;
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
        private const int pingInterval = 15000; // Ping间隔默认15秒

        /// <summary>
        /// 初始化
        /// </summary>
        public Tray()
        {
            // 初始化语言
            LanguageManager.Initialize();

            // 初始化系统托盘图标
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.KeepMyHotspotAlive,
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

        /// <summary>
        /// 检测到设备连接的无线网络变化时自动暂停ping操作
        /// </summary>
        private bool DetectNetworkAddressChange()
        {
            // 获取新网关地址
            string newGateway = GetGatewayIP();

            // 网关发生变化时处理
            if (newGateway != gatewayIP)
            {
                // 自动暂停
                if (pingTimer.Enabled)
                {
                    pingTimer.Stop();
                    trayIcon.ContextMenuStrip.Items[0].Text = Resources.MenuResume;
                    ShowBalloonTip(Resources.StatusNetworkChanged);
                    UpdateTooltip(Resources.StatusNetworkChanged);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取网关IP地址
        /// </summary>
        /// <returns>网关IP地址</returns>
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

        /// <summary>
        /// 初始化计时器
        /// </summary>
        private void InitializeTimer()
        {
            pingTimer = new System.Timers.Timer(pingInterval);
            pingTimer.Elapsed += async (s, e) => await PerformPing();
            pingTimer.AutoReset = true;
            pingTimer.Start();
        }

        /// <summary>
        /// 执行Ping操作并更新托盘图标提示
        /// </summary>
        private async Task PerformPing()
        {
            try
            {
                // 检测网关地址是否变化
                if (DetectNetworkAddressChange())
                {
                    return;
                }

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

        /// <summary>
        /// 更新托盘图标提示
        /// </summary>
        private void UpdateTooltip(string text)
        {
            if (trayIcon != null)
            {
                trayIcon.Text = text;
            }
        }

        /// <summary>
        /// 侦测到网络变动时弹出Toast通知
        /// </summary>
        private void ShowBalloonTip(string text)
        {
            if (trayIcon != null)
            {
                trayIcon.BalloonTipIcon = ToolTipIcon.Warning;
                trayIcon.BalloonTipTitle = "Keep My Hotspot Alive";
                trayIcon.BalloonTipText = text;
                trayIcon.ShowBalloonTip(0);
            }
        }

        /// <summary>
        /// 暂停/继续Ping功能
        /// </summary>
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
                gatewayIP = GetGatewayIP(); // 刷新路由器IP
                Task.Run(() => PerformPing()); // 立即执行一次Ping
            }
        }

        /// <summary>
        /// 退出程序
        /// </summary>
        private void OnExit(object sender, EventArgs e)
        {
            pingTimer?.Dispose();
            trayIcon.Visible = false;
            Application.Exit();
        }

        /// <summary>
        /// 弹出报错信息并自动退出
        /// </summary>
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
