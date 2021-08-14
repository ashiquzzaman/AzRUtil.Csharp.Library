using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace AzRUtil.Csharp.Library.Extensions
{
    public static class StringExtensions
    {
        public static string AppendTimeStamp(this string fileName)
        {
            return String.Concat(Path.GetFileNameWithoutExtension(fileName) + "_",
                DateTime.UtcNow.ToString("yyyy-MM-dd-HHmmssfff"),
                Path.GetExtension(fileName)
                );
        }
        public static string GenerateUniqueId(this Guid guid)
        {
            long i = 1;

            foreach (byte b in guid.ToByteArray())
            {
                i *= (b + 1);
            }

            return $"{i - DateTime.Now.Ticks:x}".ToUpper();
        }


        public static string IntToChar(this int index)
        {
            var columnBase = 26;
            var digitMax = 7; // ceil(log26(Int32.Max))
            var digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (index <= 0)
                throw new IndexOutOfRangeException("index must be a positive number");

            if (index <= columnBase)
                return digits[index - 1].ToString();

            var sb = new StringBuilder().Append(' ', digitMax);
            var current = index;
            var offset = digitMax;
            while (current > 0)
            {
                sb[--offset] = digits[--current % columnBase];
                current /= columnBase;
            }
            return sb.ToString(offset, digitMax - offset);
        }


        public static string ToRemoveHtmlTag(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }


        public static string ToUrlFriendly(this string value)
        {
            value = value.ToLowerInvariant().Replace(" ", "-");
            value = Regex.Replace(value, @"[^0-9a-z-]", string.Empty);

            return value;
        }
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str, 0))
                return str;

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        public static string ToSentence(this string valueString)
        {
            return new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture).Replace(valueString.ToLower(),
                s => s.Value.ToUpper());
        }
        public static string GetLast(this string source, int returnLength)
        {
            if (returnLength >= source.Length)
                return source;
            return source.Substring(source.Length - returnLength);
        }

        public static string MakeId(this string maxValue, string prefix, int returnLength, char fillValue = '0')
        {
            const string uniq = "0";
            maxValue = string.IsNullOrEmpty(maxValue) ? prefix + uniq.PadLeft(returnLength, fillValue) : maxValue;
            var intPart = maxValue.Contains(prefix) ? maxValue.Substring(prefix.Length, maxValue.Length - prefix.Length) : maxValue;
            var id = (BigInteger.Parse(intPart, NumberStyles.Float, CultureInfo.InvariantCulture) + 1).ToString();
            if (returnLength > 1)
            {
                id = prefix + id.PadLeft(returnLength, fillValue);
            }
            return id;
        }
        public static string ConvertMd5Hash(this string value)
        {
            var email = value.ToLower();

            byte[] hash;
            using (var md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(email));
            }

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(i.ToString("x2"));
            }

            return sb.ToString();

        }
        public static string Md5Encrypt(this string toencrypt, string key = "AzR+SK", bool usehashing = true)
        {
            byte[] keyArray;

            // If hashing use get hash code regards to your key
            if (usehashing)
            {
                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                }
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            // set the secret key for the tripleDES algorithm
            // mode of operation. there are other 4 modes.
            // We choose ECB(Electronic code Book)
            // padding mode(if any extra byte added)
            using (var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            using (var transform = tdes.CreateEncryptor())
            {
                try
                {
                    var toEncryptArray = Encoding.UTF8.GetBytes(toencrypt);

                    // transform the specified region of bytes array to resultArray
                    var resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    // Return the encrypted data into unreadable string format
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static string Md5Decrypt(this string todecrypt, string key = "AzR+SK", bool usehashing = true)
        {
            byte[] toEncryptArray;

            // get the byte code of the string
            try
            {
                toEncryptArray = Convert.FromBase64String(todecrypt.Replace(" ", "+")); // The replace happens only when spaces exist in the string (hence not a Base64 string in the first place).
            }
            catch (Exception)
            {
                return string.Empty;
            }

            byte[] keyArray;

            if (usehashing)
            {
                // if hashing was used get the hash code with regards to your key
                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                }
            }
            else
            {
                // if hashing was not implemented get the byte code of the key
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            // set the secret key for the tripleDES algorithm
            // mode of operation. there are other 4 modes. 
            // We choose ECB(Electronic code Book)
            // padding mode(if any extra byte added)
            using (var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            })
            using (var transform = tdes.CreateDecryptor())
            {
                try
                {
                    var resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    // return the Clear decrypted TEXT
                    return Encoding.UTF8.GetString(resultArray);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }


        public static T FromXmlFile<T>(this string path)
        {
            var xmlDoc = new XmlDocument { XmlResolver = null };
            xmlDoc.Load(path);
            var xmlString = xmlDoc.InnerXml;
            var reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(T));
            var instance = (T)serializer.Deserialize(reader);
            return instance;
        }
        public static T FromXmlString<T>(this string xmlString)
        {
            var reader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(T));
            var instance = (T)serializer.Deserialize(reader);
            return instance;
        }
    }
}
