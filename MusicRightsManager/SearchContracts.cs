using System;
using System.Collections.Generic;
using System.Linq;
using ContractManager;

namespace MusicRightsManager
{
    public class SearchContracts
    {
        private readonly string _distributorcontractfile;
        private readonly string _musiccontractfile;

        public SearchContracts(string distributorcontractfile, string musiccontractfile)
        {
            _distributorcontractfile = distributorcontractfile;
            _musiccontractfile = musiccontractfile;
        }

        public IEnumerable<string> Search(string partner, DateTime? searchdate)
        {
            IList<string> searchusages = new List<string>();

            using (ContractsReader<DistributionPartnerContract> reader =
                new ContractsReader<DistributionPartnerContract>(_distributorcontractfile)
            )
            {
                foreach (var distributionPartnerContract in reader.ReadAll().Where(x => MatchString(x.Partner, partner)))
                {
                    searchusages.Add(distributionPartnerContract.Usage);
                }
            }

            using (ContractsReader<MusicContract> reader =
                new ContractsReader<MusicContract>(_musiccontractfile)
            )
            {
                foreach (var searchusage in searchusages)
                {
                    foreach (var musicContract in reader.ReadAll()
                        .Where(x => x.Usages.Contains(searchusage) && IsDateXGreaterThenDateY(searchdate, x.StartDate))
                        .OrderBy(x => x.Artist).ThenBy(x => x.Title)
                    )
                    {
                        yield return musicContract.ConvertToString(searchusage);
                    }
                }


            }
        }

        private static bool MatchString(string searchstring, string keyword)
        {
            return searchstring.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsDateXGreaterThenDateY(DateTime? dateX, DateTime? dateY)
        {
            return dateX > dateY;
        }
    }
}
