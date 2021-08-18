using AzRUtil.Csharp.Library.Constants;

namespace AzRUtil.Csharp.Library.Models
{
    public class SuccessModel
    {
        public SuccessModel()
        {
            LayoutType = LibConstants.LayoutType.MultiplePages;

        }
        public string FeatureName { get; set; }
        public bool IsAddContinue { get; set; }
        public bool IsEditContinue { get; set; }

        public dynamic OldId { get; set; }
        public dynamic NewId { get; set; }
        public string Message { get; set; }
        public string ReturnViewName { get; set; }
        public object RouteValue { get; set; }
        public LibConstants.LayoutType LayoutType { get; set; }

    }
}