using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AzRUtil.Csharp.Library.Exceptions
{
    public static class ExceptionExtensions
    {
        public static string RequestUrl;
        public static string RequestBody;
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem,
            Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }
        public static IEnumerable<Exception> GetAllExceptions(this Exception exception)
        {
            yield return exception;

            if (exception is AggregateException)
            {
                var aggrEx = exception as AggregateException;
                foreach (Exception innerEx in aggrEx.InnerExceptions.SelectMany(e => e.GetAllExceptions()))
                {
                    yield return innerEx;
                }
            }
            else if (exception.InnerException != null)
            {
                foreach (Exception innerEx in exception.InnerException.GetAllExceptions())
                {
                    yield return innerEx;
                }
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }
        public static string GetAllMessages(this Exception exception)
        {
            var messages = exception
                .FromHierarchy(ex => ex.InnerException)
                .Select(ex => ex.Message).ToList();
            return string.Join(Environment.NewLine, messages);
        }

        public static IEnumerable<ExceptionModel> GetAll(this Exception exception)
        {
            try
            {
                var exceptions = exception.FromHierarchy(ex => ex.InnerException);
                var list = from ex in exceptions
                           let st = new StackTrace(ex, true)
                           let frame = st.GetFrame(st.FrameCount - 1)
                           let declaringType = frame?.GetMethod()?.DeclaringType
                           select new ExceptionModel
                           {
                               ErrorTime = DateTime.UtcNow,
                               FileName = Path.GetFileName(frame.GetFileName()),
                               MethodName = frame?.GetMethod()?.Name,
                               LineNumber = frame.GetFileLineNumber(),
                               Message = ex.Message,
                               ColumnNumber = frame.GetFileColumnNumber(),
                               EntityName = declaringType != null ? declaringType.Name : frame.GetFileName(),
                               EntityFullName = declaringType != null ? declaringType.FullName : frame.GetFileName(),
                               StackTrace = ex.StackTrace,
                               ExtraStackTrace = "",
                               RequestUrl = RequestUrl,
                               RequestBody = RequestBody,
                           };

                return list;
            }
            catch (Exception)
            {
                return new List<ExceptionModel>();
            }
        }
        public static void ToTextFileLog(this Exception ex)
        {

            ToTextFileLog(ex, AppConstant.WwwrootPath);
        }

        public static void ToTextFileLog(this Exception ex, string startupPath, string folderName = "Log", string fileName = "ErrorLog.txt")
        {
            string message = string.Empty;
            var filePath = startupPath + "\\" + folderName + "\\" + fileName;
            var exceptions = ex.GetAll();

            foreach (var item in exceptions)
            {

                var msg = "--------------------------------------------------" + Environment.NewLine;

                msg += "Date: " + DateTime.UtcNow.ToString("yyyy-MM-dd") + Environment.NewLine
                        + "Time: " + DateTime.UtcNow.ToString("hh:mm:ss") + Environment.NewLine
                        + "..............................." + Environment.NewLine
                        + "Request URL: " + item.RequestUrl + Environment.NewLine
                        + "Request Body: " + item.RequestBody + Environment.NewLine
                        + "..............................." + Environment.NewLine
                        + "File Name: " + item.FileName + Environment.NewLine
                        + "Entity Name: " + item.EntityFullName + Environment.NewLine
                        + "Method Name: " + item.MethodName + Environment.NewLine
                        + "Line Number: " + item.LineNumber + Environment.NewLine
                        + "Column Number: " + item.ColumnNumber + Environment.NewLine
                        + "Message : " + item.Message + Environment.NewLine
                        + "Stack Trace : " + item.StackTrace + Environment.NewLine;
                msg += "--------------------------------------------------" + Environment.NewLine;
                message += msg;

            }

            File.AppendAllText(filePath, message);
        }
        public static void ToWriteMessageLog(this string message, string methodName = "", string folderName = "Log", string fileName = "MessageLog.txt")
        {
            var startupPath = AppConstant.WwwrootPath;


            var msg = "--------------------------------------------------" + Environment.NewLine;
            msg += "Date: " + DateTime.Now.ToString("yyyy-MM-dd") + Environment.NewLine
                 + "Time: " + DateTime.Now.ToString("hh:mm:ss") + Environment.NewLine;

            if (!string.IsNullOrWhiteSpace(methodName))
            {
                msg += "Function : " + methodName + Environment.NewLine;
            }
            msg += "Message : " + message + Environment.NewLine;
            msg += "--------------------------------------------------" + Environment.NewLine;

            var filePath = startupPath + "\\" + folderName + "\\" + fileName;
            File.AppendAllText(filePath, msg);
        }

        public static void ToWriteLog(this Exception ex)
        {
            ex.ToTextFileLog();
        }
    }

}
