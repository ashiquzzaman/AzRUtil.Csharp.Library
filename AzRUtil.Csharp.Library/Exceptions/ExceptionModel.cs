using System;
using System.Collections.Generic;
using System.Linq;

namespace AzRUtil.Csharp.Library.Exceptions
{
    public class ExceptionModel
    {

        public string FileName { get; set; }
        public string MethodName { get; set; }//Or Action
        public string EntityName { get; set; }//Or Controller
        public string EntityFullName { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }//or Request Data

        public string ExtraStackTrace { get; set; }//or Request Header


        public int LineNumber { get; set; }
        public int ColumnNumber { get; set; }

        public DateTime ErrorTime { get; set; }
        public string RequestUrl { get; set; }
        public string RequestBody { get; set; }

        public static string GetAllMessage(IEnumerable<ExceptionModel> exErrors)
        {
            return exErrors.Aggregate(string.Empty,
                (current, ex) => current
                                 + (DateTime.Now.ToString("MMM dd, yyyy h:mm tt")
                                  + ":: " + ex.FileName
                                  + ":: " + ex.EntityFullName
                                  + ":: " + ex.MethodName
                                  + ":: " + ex.LineNumber
                                  + ":: " + ex.Message
                                  + Environment.NewLine));
        }
        public string Get()
        {
            return (DateTime.Now.ToString("MMM dd, yyyy h:mm tt")
                    + ":: " + FileName
                    + ":: " + EntityFullName
                    + ":: " + MethodName
                    + ":: " + LineNumber
                    + ":: " + Message
                    + Environment.NewLine);
        }
    }
}
