using System;
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
            if (IsSimplifiedChinese(culture))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            }
            else if (IsTraditionalChinese(culture))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-TW");
            }
            else
            {
                // 默认使用英语
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
        }

        private static bool IsSimplifiedChinese(CultureInfo culture)
        {
            // 层级检测：文化链 -> zh-SG -> zh-Hans -> zh
            while (culture != null && !culture.Equals(CultureInfo.InvariantCulture))
            {
                if (culture.Name.StartsWith("zh-CN") ||
                    culture.Name.Equals("zh-Hans", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                culture = culture.Parent;
            }
            return false;
        }

        private static bool IsTraditionalChinese(CultureInfo culture)
        {
            // 层级检测：文化链 -> zh-TW -> zh-Hant
            while (culture != null && !culture.Equals(CultureInfo.InvariantCulture))
            {
                if (culture.Name.StartsWith("zh-TW") ||
                    culture.Name.Equals("zh-Hant", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                culture = culture.Parent;
            }
            return false;
        }
    }
}
