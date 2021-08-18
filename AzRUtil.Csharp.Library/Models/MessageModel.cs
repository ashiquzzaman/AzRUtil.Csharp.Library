using AzRUtil.Csharp.Library.Constants;

namespace AzRUtil.Csharp.Library.Models
{
    public class MessageModel
    {
        public MessageModel()
        {
            LoadPosition = "mainContent";
            SaveContinueType = LibConstants.SaveContinueType.None;
            LayoutType = LibConstants.LayoutType.MultiplePages;
        }
        public LibConstants.ExceptionType Type { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
        public string Color { get; set; }
        public string LoadPosition { get; set; }

        public LibConstants.SaveContinueType SaveContinueType { get; set; }
        public string ReturnViewName { get; set; }
        public object RouteValue { get; set; }
        public LibConstants.LayoutType LayoutType { get; set; }
    }


}
