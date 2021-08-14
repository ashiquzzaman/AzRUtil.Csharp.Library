using AzRUtil.Csharp.Library.Enumerations;

namespace AzRUtil.Csharp.Library.Models
{
    public class AppLayout
    {
        public AdminTheme AdminTheme { get; set; }
        public LayoutType LayoutType { get; set; }
        public SaveFormType SaveFormType { get; set; }
        public ContentType ContentType { get; set; }
        public ModalType ModalType { get; set; }
        public bool IsMaterialDesignEnable { get; set; }
    }
}
