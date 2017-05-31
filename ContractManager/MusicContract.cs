using System;
using System.Collections.Generic;

namespace ContractManager
{
    public class MusicContract: IContract<MusicContract>
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string[] Usages { get; set; }
        public DateTime? StartDate { get; set; }
        public string InputStartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string InputEndDate { get; set; }

        public string ConvertToString(string usage)
        {
            return $"{Artist}|{Title}|{usage}|{InputStartDate}|{InputEndDate}";
        }

        public override string ToString()
        {
            return $"{Artist}|{Title}|{Usages[0]}|{InputStartDate}|{InputEndDate}";
        }

        public MusicContract GetContractObject(IDictionary<string, string> dataRow)
        {
            return new MusicContractConvertor().ConvertToObject(dataRow);
        }
    }

    public interface IContract<T>
    {
        T GetContractObject(IDictionary<string, string> dataRow);
    }
}
