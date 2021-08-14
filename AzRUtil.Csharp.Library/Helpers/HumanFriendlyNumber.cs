﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AzRUtil.Csharp.Library.Helpers
{
    public static class HumanFriendlyNumber
    {
        private static readonly string[] Units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] Tens = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        private static readonly string[] Scales = { "", "Thousand", "Lakh", " Million", "Crore", " Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion" };

        public static string IntegerToWord(this int value)
        {
            if (value == 0)
            {
                return "Zero";
            }
            if (value < 0)
            {
                return "Negative " + IntegerToWord(-value);
            }

            return FriendlyInteger(value, "", 0);
        }

        public static string AmountInBanglaWord(this double amount)
        {
            var n = (int)amount;

            if (n == 0)
                return " ";
            else if (n > 0 && n <= 99)
            {
                var arr = new string[] { "এক", "দুই", "তিন", "চার", "পাঁচ", "ছয়", "সাত", "আট", "নয়", "দশ", "এগার", "বারো", "তের", "চৌদ্দ", "পনের", "ষোল", "সতের", "আঠার", "ঊনিশ", "বিশ", "একুশ", "বাইস", "তেইশ", "চব্বিশ", "পঁচিশ", "ছাব্বিশ", "সাতাশ", "আঠাশ", "ঊনত্রিশ", "ত্রিশ", "একত্রিস", "বত্রিশ", "তেত্রিশ", "চৌত্রিশ", "পঁয়ত্রিশ", "ছত্রিশ", "সাঁইত্রিশ", "আটত্রিশ", "ঊনচল্লিশ", "চল্লিশ", "একচল্লিশ", "বিয়াল্লিশ", "তেতাল্লিশ", "চুয়াল্লিশ", "পয়তাল্লিশ", "ছিচল্লিশ", "সাতচল্লিশ", "আতচল্লিশ", "উনপঞ্চাশ", "পঞ্চাশ", "একান্ন", "বায়ান্ন", "তিপ্পান্ন", "চুয়ান্ন", "পঞ্চান্ন", "ছাপ্পান্ন", "সাতান্ন", "আটান্ন", "উনষাট", "ষাট", "একষট্টি", "বাষট্টি", "তেষট্টি", "চৌষট্টি", "পয়ষট্টি", "ছিষট্টি", " সাতষট্টি", "আটষট্টি", "ঊনসত্তর ", "সত্তর", "একাত্তর ", "বাহাত্তর", "তেহাত্তর", "চুয়াত্তর", "পঁচাত্তর", "ছিয়াত্তর", "সাতাত্তর", "আটাত্তর", "ঊনাশি", "আশি", "একাশি", "বিরাশি", "তিরাশি", "চুরাশি", "পঁচাশি", "ছিয়াশি", "সাতাশি", "আটাশি", "উননব্বই", "নব্বই", "একানব্বই", "বিরানব্বই", "তিরানব্বই", "চুরানব্বই", "পঁচানব্বই ", "ছিয়ানব্বই ", "সাতানব্বই", "আটানব্বই", "নিরানব্বই" };
                return arr[n - 1] + " ";
            }
            else if (n >= 100 && n <= 199)
            {
                return AmountInBanglaWord(n / 100) + "এক শত " + AmountInBanglaWord(n % 100);
            }

            else if (n >= 100 && n <= 999)
            {
                return AmountInBanglaWord(n / 100) + "শত " + AmountInBanglaWord(n % 100);
            }
            else if (n >= 1000 && n <= 1999)
            {
                return "এক হাজার " + AmountInBanglaWord(n % 1000);
            }
            else if (n >= 1000 && n <= 99999)
            {
                return AmountInBanglaWord(n / 1000) + "হাজার " + AmountInBanglaWord(n % 1000);
            }
            else if (n >= 100000 && n <= 199999)
            {
                return "এক লাখ " + AmountInBanglaWord(n % 100000);
            }
            else if (n >= 100000 && n <= 9999999)
            {
                return AmountInBanglaWord(n / 100000) + "লাখ " + AmountInBanglaWord(n % 100000);
            }
            else if (n >= 10000000 && n <= 19999999)
            {
                return "এক কোটি " + AmountInBanglaWord(n % 10000000);
            }
            else
            {
                return AmountInBanglaWord(n / 10000000) + "কোটি " + AmountInBanglaWord(n % 10000000);
            }
        }

        public static string AmountInWord(string inputMonetaryValueInNumberic)
        {
            string centPart = string.Empty;
            string dollarPart = string.Empty;
            inputMonetaryValueInNumberic = inputMonetaryValueInNumberic.TrimStart('0');

            if (ValidateInput(inputMonetaryValueInNumberic))
            {

                if (inputMonetaryValueInNumberic.Contains("."))
                {
                    centPart = ProcessCents(inputMonetaryValueInNumberic.Substring(inputMonetaryValueInNumberic.IndexOf(".") + 1));
                    dollarPart = ProcessDollar(inputMonetaryValueInNumberic.Substring(0, inputMonetaryValueInNumberic.IndexOf(".")));
                }
                else
                {
                    dollarPart = ProcessDollar(inputMonetaryValueInNumberic);
                }
                centPart = string.IsNullOrWhiteSpace(centPart) ? string.Empty : " and " + centPart;
                return $"\n\n{dollarPart}{centPart}";
            }
            else
            {
                return "Invalid Input..";
            }
        }



        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }

            string friendlyInt = leftDigits;

            if (friendlyInt.Length > 0)
            {
                friendlyInt += " ";
            }

            if (n < 10)
            {
                friendlyInt += Units[n];
            }
            else if (n < 20)
            {
                friendlyInt += Teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, Tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (Units[n / 100] + " Hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
                if (n % 1000 == 0)
                {
                    return friendlyInt;
                }
            }

            return friendlyInt + Scales[thousands];
        }
        private static string ProcessCents(string cents)
        {
            string english = string.Empty;
            string dig3 = Process3Digit(cents);
            if (!string.IsNullOrWhiteSpace(dig3))
            {
                dig3 = $"{dig3} {GetSections(0)}";
            }
            english = dig3 + english;
            return english;
        }
        private static string ProcessDollar(string dollar)
        {
            string english = string.Empty;
            foreach (var item in Get3DigitList(dollar))
            {
                string dig3 = Process3Digit(item.Value);
                if (!string.IsNullOrWhiteSpace(dig3))
                {
                    dig3 = $"{dig3} {GetSections(item.Key)}";
                }
                english = dig3 + english;
            }
            return english;
        }
        private static string Process3Digit(string digit3)
        {
            string result = string.Empty;
            if (Convert.ToInt32(digit3) != 0)
            {
                int place = 0;
                Stack<string> monetaryValue = new Stack<string>();
                for (int i = digit3.Length - 1; i >= 0; i--)
                {
                    place += 1;
                    string stringValue = string.Empty;
                    switch (place)
                    {
                        case 1:
                            stringValue = GetOnes(digit3[i].ToString());
                            break;
                        case 2:
                            int tens = Convert.ToInt32(digit3[i]);
                            if (tens == 1)
                            {
                                if (monetaryValue.Count > 0)
                                {
                                    monetaryValue.Pop();
                                }
                                stringValue = GetTens((digit3[i].ToString() + digit3[i + 1].ToString()));
                            }
                            else
                            {
                                stringValue = GetTens(digit3[i].ToString());
                            }
                            break;
                        case 3:
                            stringValue = GetOnes(digit3[i].ToString());
                            if (!string.IsNullOrWhiteSpace(stringValue))
                            {
                                string postFixWith = " Hundred";
                                if (monetaryValue.Count > 0)
                                {
                                    postFixWith = postFixWith + " And";
                                }
                                stringValue += postFixWith;
                            }
                            break;
                    }
                    if (!string.IsNullOrWhiteSpace(stringValue))
                        monetaryValue.Push(stringValue);
                }
                while (monetaryValue.Count > 0)
                {
                    result += " " + monetaryValue.Pop().ToString().Trim();
                }
            }
            return result;
        }
        private static Dictionary<int, string> Get3DigitList(string monetaryValueInNumberic)
        {
            Dictionary<int, string> hundredsStack = new Dictionary<int, string>();
            int counter = 0;
            while (monetaryValueInNumberic.Length >= 3)
            {
                string digit3 = monetaryValueInNumberic.Substring(monetaryValueInNumberic.Length - 3, 3);
                monetaryValueInNumberic = monetaryValueInNumberic.Substring(0, monetaryValueInNumberic.Length - 3);
                hundredsStack.Add(++counter, digit3);
            }
            if (monetaryValueInNumberic.Length != 0)
                hundredsStack.Add(++counter, monetaryValueInNumberic);
            return hundredsStack;
        }
        private static string GetTens(string tensPlaceValue)
        {
            string englishEquvalent = string.Empty;
            int value = Convert.ToInt32(tensPlaceValue);
            var tens = new Dictionary<int, string>
            {
                {2, "Twenty"},
                {3, "Thirty"},
                {4, "Forty"},
                {5, "Fifty"},
                {6, "Sixty"},
                {7, "Seventy"},
                {8, "Eighty"},
                {9, "Ninety"},
                {10, "Ten"},
                {11, "Eleven"},
                {12, "Twelve"},
                {13, "Thirteen"},
                {14, "Fourteen"},
                {15, "Fifteen"},
                {16, "Sixteen"},
                {17, "Seventeen"},
                {18, "Eighteen"},
                {19, "Nineteen"}
            };
            if (tens.ContainsKey(value))
            {
                englishEquvalent = tens[value];
            }

            return englishEquvalent;

        }
        private static string GetOnes(string onesPlaceValue)
        {
            int value = Convert.ToInt32(onesPlaceValue);
            string englishEquvalent = string.Empty;
            Dictionary<int, string> ones = new Dictionary<int, string>();
            ones.Add(1, " One");
            ones.Add(2, " Two");
            ones.Add(3, " Three");
            ones.Add(4, " Four");
            ones.Add(5, " Five");
            ones.Add(6, " Six");
            ones.Add(7, " Seven");
            ones.Add(8, " Eight");
            ones.Add(9, " Nine");

            if (ones.ContainsKey(value))
            {
                englishEquvalent = ones[value];
            }

            return englishEquvalent;
        }
        private static string GetSections(int section)
        {
            string sectionName = string.Empty;
            switch (section)
            {
                case 0:
                    sectionName = "Cents";
                    break;
                case 1:
                    sectionName = "Dollars";
                    break;
                case 2:
                    sectionName = "Thousand";
                    break;
                case 3:
                    sectionName = "Million";
                    break;
                case 4:
                    sectionName = "Billion";
                    break;
                case 5:
                    sectionName = "Trillion";
                    break;
                case 6:
                    sectionName = "Zillion";
                    break;
            }
            return sectionName;
        }
        private static bool ValidateInput(string input)
        {
            return Regex.IsMatch(input, "[0-9]{1,18}(\\.[0-9]{1,2})?");
        }
    }
}
