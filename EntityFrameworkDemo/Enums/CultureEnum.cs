using System.ComponentModel;

namespace EntityFrameworkDemo.Enums
{
    public enum CultureEnum
    {
        [Description("zh-TW")] zhTW = 0,
        [Description("zh-CN")] zhCN = 1,
        [Description("en-US")] enUS = 2
    }
}