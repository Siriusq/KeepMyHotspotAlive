using System.Globalization;
using System.Threading;

namespace KeepMyHotspotAlive
{
    static class LanguageManager
    {
        public static void Initialize()
        {
            // 获取系统首选语言
            var culture = CultureInfo.CurrentUICulture;

            // 判断是否为简体中文
            if (culture.Name.StartsWith("zh-CN"))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            }
            else
            {
                // 默认使用英语
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
        }
    }
}
