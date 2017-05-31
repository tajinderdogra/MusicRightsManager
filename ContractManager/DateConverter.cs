using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ContractManager
{
    public class DateConverter
    {
        public DateTime? ConvertToDateTime(string dateInput)
        {
            if (string.IsNullOrEmpty(dateInput)) return null;

            var splitDate = dateInput.Split(' ');
            var dayPart = Regex.Match(splitDate[0], "\\d+").Value;
            var reformatDate = $"{dayPart}/{GetMonth(splitDate[1])}/{splitDate[2]}";
            DateTime parsedDate;
            if (DateTime.TryParseExact(reformatDate, "d/MMMM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                return parsedDate;
            return null;
        }

        private string GetMonth(string month)
        {
            string[] months =
            {
                "january", "february", "march", "april", "may", "june", "july", "august", "september", "october",
                "november", "december"
            };

            foreach (string storedMonth in months)
            {
                if (storedMonth.StartsWith(month.ToLower())) return storedMonth;
            }

            return "";
        }
    }
}
