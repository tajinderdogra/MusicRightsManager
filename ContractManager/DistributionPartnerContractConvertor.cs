using System;
using System.Collections.Generic;

namespace ContractManager
{
    public class DistributionPartnerContractConvertor
    {
        public DistributionPartnerContract ConvertToObject(IDictionary<string, string> dataRow)
        {
            if (dataRow == null || dataRow.Count == 0) return null;

            return new DistributionPartnerContract
            {
                Partner = dataRow.ContainsKey("Partner") ? dataRow["Partner"].Trim() : String.Empty,
                Usage = dataRow.ContainsKey("Usage") ? dataRow["Usage"].Trim() : String.Empty,             
            };
        }

    }
}