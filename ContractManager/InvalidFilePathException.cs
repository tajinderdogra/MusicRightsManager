using System;

namespace ContractManager
{
    public class InvalidFilePathException : Exception
    {
        public InvalidFilePathException(string filePathCannotBeEmptyOrNull) : base(filePathCannotBeEmptyOrNull){}
    }
}