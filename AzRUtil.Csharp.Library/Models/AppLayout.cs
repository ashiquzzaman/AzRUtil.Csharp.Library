using AzRUtil.Csharp.Library.Constants;

namespace AzRUtil.Csharp.Library.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AppLayout
    {
        public LibConstants.FrontTheme FrontTheme { get; set; }
        public LibConstants.AdminTheme AdminTheme { get; set; }
        public LibConstants.LayoutType LayoutType { get; set; }
        public LibConstants.SaveFormType SaveFormType { get; set; }
        public LibConstants.ContentType ContentType { get; set; }
        public LibConstants.ModalType ModalType { get; set; }
        public LibConstants.ThemeBootstrap ThemeBootstrap { get; set; }
        public bool IsMaterialDesignEnable { get; set; }
    }
}
