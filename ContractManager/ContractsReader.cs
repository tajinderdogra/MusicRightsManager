using System;
using System.Collections.Generic;
using System.IO;

namespace ContractManager
{
    public class ContractsReader<T>: IDisposable where T : IContract<T>, new()
    {
        private readonly StreamReader _reader;

        public ContractsReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new InvalidFilePathException("File path cannot be empty or null");

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            _reader = new StreamReader(filePath);
        }

        public IEnumerable<T> ReadAll()
        {
            string line;
            int lineCount = 0;
            
            string[] header = { };

            while ((line = _reader.ReadLine()) != null)
            {
                if (lineCount == 0)
                {
                    header = line.Split('|');
                }
                else
                {
                    var data = line.Split('|');
                    Dictionary<string, string> row = new Dictionary<string, string>();

                    for (var i = 0; i < data.Length; i++)
                    {
                        row.Add(header[i].Trim(), data[i].Trim());
                    }
                    yield return new T().GetContractObject(row);
                }

                lineCount++;
            }

        }

        public void Dispose()
        {
            _reader.Close();
            _reader.Dispose();
        }
    }
}