using AzRUtil.Csharp.Library.Constants;
using System.Collections.Generic;

namespace AzRUtil.Csharp.Library.Models
{
    public class ChangeLog
    {
        public ChangeLog()
        {
            Changes = new List<ObjectChangeLog>();
        }
        public long ActionTime { get; set; }
        public LibConstants.ActionType ActionType { get; set; }
        public string ActionTypeName { get; set; }
        public string ActionBy { get; set; }
        public string ActionUrl { get; set; }
        public string ActionAgent { get; set; }
        public List<ObjectChangeLog> Changes { get; set; }
        public string KeyFieldId { get; set; }
        public string EntityName { get; set; }
        public string EntityFullName { get; set; }
        public string EntityFriendlyName { get; set; }
    }

}