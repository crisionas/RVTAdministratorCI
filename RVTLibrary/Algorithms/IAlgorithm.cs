using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace RVTLibrary.Algoritms
{
    [ContractClass(typeof(AlgorithmContract))]
    public interface IAlgorithm
    {
        string GetHash(string data);
        string GetHash(IHashable data);
    }
    [ContractClassFor(typeof(IAlgorithm))]
    internal abstract class AlgorithmContract : IAlgorithm
    {
        string IAlgorithm.GetHash(string data)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(data), $"Hashing was not possible. Argument  {nameof(data)} don't have data.");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
            return string.Empty;
        }

        string IAlgorithm.GetHash(IHashable data)
        {
            Contract.Requires<ArgumentNullException>(data != null, $"Hashing was not possible. Argument {nameof(data)} is null.");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
            return string.Empty;
        }
    }
}
