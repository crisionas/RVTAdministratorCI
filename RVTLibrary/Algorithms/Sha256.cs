using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RVTLibrary.Algoritms
{
    public class Sha256 : IAlgorithm
    {
        private SHA256 _sha256 = null;
        public Sha256()
        {
            _sha256 = SHA256.Create();
        }
        public string GetHash(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var hashByte = _sha256.ComputeHash(bytes);
            var hash = BitConverter.ToString(hashByte);

            var formattedHash = hash.Replace("-", "")
                                    .ToLower();

            return formattedHash;
        }
        public string GetHash(IHashable data)
        {
            var dataBeforeHash = data.GetStringForHas();
            var hash = GetHash(dataBeforeHash);
            return hash;
        }
        public override string ToString()
        {
            return "SHA 256";
        }
    }
}
