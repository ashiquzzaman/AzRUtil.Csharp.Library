using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace AzRUtil.Csharp.Library.Helpers
{
    public static class Util
    {
        public static string CompressFile(string sourceFile)
        {
            using (ZipArchive archive = ZipFile.Open(Path.ChangeExtension(sourceFile, ".zip"), ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(sourceFile, Path.GetFileName(sourceFile));
            }
            return Path.ChangeExtension(sourceFile, ".zip");
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                           + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                           + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

                return Regex.Match(email, validEmailPattern, RegexOptions.IgnoreCase).Success;
            }
            catch
            {
                return false;
            }
        }


        public static string RandomString(int rang, bool lowerChar = false, bool specialChar = false)
        {
            var rsg = new StringGenerator { UseLowerCaseCharacters = lowerChar, UseSpecialCharacters = specialChar };
            var returnValue = rsg.Generate(rang);
            return returnValue;
        }

        public static (string FirstName, string MiddleName, string LastName) SplitFullName(string fullName)
        {
            string firstName;
            string middleName;
            string lastName;
            var nameArr = fullName.Split(' ');

            switch (nameArr.Length)
            {
                case 3:
                    firstName = nameArr[0];
                    middleName = nameArr[1];
                    lastName = nameArr[2];
                    break;
                case 2:
                    firstName = nameArr[0];
                    middleName = string.Empty;
                    lastName = nameArr[1];
                    break;
                case 1:
                    firstName = nameArr[0];
                    middleName = string.Empty;
                    lastName = string.Empty;
                    break;
                default:
                    firstName = string.Empty;
                    middleName = string.Empty;
                    lastName = string.Empty;
                    break;
            }

            return (firstName, middleName, lastName);
        }

        public static Tuple<string, string, string> SplitFullNameOld(string fullName)
        {
            var name = SplitFullName(fullName);
            return Tuple.Create(name.FirstName, name.MiddleName, name.LastName);
        }
    }
}
