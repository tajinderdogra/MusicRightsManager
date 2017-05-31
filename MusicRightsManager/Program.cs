using System;
using ContractManager;

namespace MusicRightsManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your input: ");
            var inputString = Console.ReadLine();
            var inputargs = inputString.Split(' ');

            var partner = inputargs[0].Trim();
            var inputdate = inputString.Replace(partner, "").Trim();

            SearchContracts searchContracts = new SearchContracts(
                @"Data\DistributionPartnerContracts.txt",
                @"Data\MusicContracts.txt");

            Console.WriteLine();
            Console.WriteLine("Artist|Title|Usage|StartDate|EndDate");
            foreach (var musicContract in searchContracts.Search(partner, new DateConverter().ConvertToDateTime(inputdate)))
            {
                Console.WriteLine(musicContract);
            }
            Console.WriteLine();
            Console.ReadLine();
        }

        
    }
}
