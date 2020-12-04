using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020.Models
{
    public class Passport
    {
        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }

        public Passport(IEnumerable<string> input)
        {
            foreach (var field in input)
            {
                var fieldSplit = field.Split(':');
                var name = fieldSplit.ElementAtOrDefault(0);
                var value = fieldSplit.ElementAtOrDefault(1);

                switch (name)
                {
                    case "byr":
                        Byr = value;
                        break;
                    case "iyr":
                        Iyr = value;
                        break;
                    case "eyr":
                        Eyr = value;
                        break;
                    case "hgt":
                        Hgt = value;
                        break;
                    case "hcl":
                        Hcl = value;
                        break;
                    case "ecl":
                        Ecl = value;
                        break;
                    case "pid":
                        Pid = value;
                        break;
                    case "cid":
                        Cid = value;
                        break;
                    default:
                        throw new Exception("Unknown field?");
                }
            }
        }

        public bool Validate()
        {
            return !new[] { Byr, Iyr, Eyr, Hgt, Hcl, Ecl, Pid }.Any(string.IsNullOrWhiteSpace);
        }

        public bool ValidateFormats()
        {
            if (!Regex.IsMatch(Byr, @"^\d\d\d\d$") || !NumberInBetween(int.Parse(Byr), 1920, 2002))
                return false;

            if (!Regex.IsMatch(Iyr, @"^\d\d\d\d$") || !NumberInBetween(int.Parse(Iyr), 2010, 2020))
                return false;

            if (!Regex.IsMatch(Eyr, @"^\d\d\d\d$") || !NumberInBetween(int.Parse(Eyr), 2020, 2030))
                return false;

            var hgtExpr = Regex.Match(Hgt, @"^\s*(\d+)([^\d]{2})$");
            var inValidate = hgtExpr.Groups[2].Value == "in" && NumberInBetween(int.Parse(hgtExpr.Groups[1].Value), 59, 76);
            var cmValidate = hgtExpr.Groups[2].Value == "cm" && NumberInBetween(int.Parse(hgtExpr.Groups[1].Value), 150, 193);
            if (!inValidate && !cmValidate)
                return false;

            if (!Regex.IsMatch(Hcl, @"^#[\da-f]{6}$"))
                return false;

            if (!Regex.IsMatch(Ecl, @"^amb|blu|brn|gry|grn|hzl|oth$"))
                return false;

            if (!Regex.IsMatch(Pid, @"^\d{9}$"))
                return false;

            // Cid is ignored

            return true;
        }

        private static bool NumberInBetween(int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}