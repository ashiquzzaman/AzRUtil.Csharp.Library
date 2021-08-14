using System;
using System.IO;
using System.Reflection;

namespace AzRUtil.Csharp.Library
{
    public static class AppConstant
    {

        public static string AssemblyRootPath
        {
            get
            {

                var startupPath = new DirectoryInfo(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))).FullName;
                return startupPath;
            }
        }
        public static string AssemblyStartupPath
        {
            get
            {

                return new DirectoryInfo(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path))).Parent.FullName;

            }
        }

        public static string StartupPath => Directory.GetCurrentDirectory();
        public static string WwwrootPath => StartupPath + "\\" + "wwwroot";

    }
}
