using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ContractManager
{
    public class MusicContractConvertor
    {
        public MusicContract ConvertToObject(IDictionary<string, string> dataRow)
        {
            if (dataRow == null || dataRow.Count == 0) return null;

            return new MusicContract
            {
                Artist = dataRow.ContainsKey("Artist")? dataRow["Artist"].Trim() : String.Empty,
                Title = dataRow.ContainsKey("Title") ? dataRow["Title"].Trim() : String.Empty,
                InputStartDate = dataRow.ContainsKey("StartDate") ? dataRow["StartDate"].Trim() : String.Empty,
                InputEndDate = dataRow.ContainsKey("EndDate") ? dataRow["EndDate"].Trim() : String.Empty,
                StartDate = dataRow.ContainsKey("StartDate") ? new DateConverter().ConvertToDateTime(dataRow["StartDate"]) : null,
                EndDate = dataRow.ContainsKey("EndDate") ? new DateConverter().ConvertToDateTime(dataRow["EndDate"]) : null,
                Usages = dataRow.ContainsKey("Usages") ? ParseUsages(dataRow) : new string[]{}                
            };
        }

        private static string[] ParseUsages(IDictionary<string, string> dataRow)
        {
            if (string.IsNullOrEmpty(dataRow["Usages"])) return new string[] {};
            return dataRow["Usages"].Split(',').Select(x => x.Trim()).ToArray();
        }

        
    }
}