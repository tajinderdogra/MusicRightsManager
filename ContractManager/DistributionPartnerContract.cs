using System.Collections.Generic;

namespace ContractManager
{
    public class DistributionPartnerContract : IContract<DistributionPartnerContract>
    {
        public string Partner { get; set; }
        public string Usage { get; set; }

        public DistributionPartnerContract GetContractObject(IDictionary<string, string> dataRow)
        {
            return new DistributionPartnerContractConvertor().ConvertToObject(dataRow);
        }
    }
}
