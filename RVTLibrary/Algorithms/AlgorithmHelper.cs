using System;
using System.Collections.Generic;
using System.Text;

namespace RVTLibrary.Algoritms
{ 
    public static class AlgorithmHelper
    {
        public static string GetHash(this IHashable component, IAlgorithm algorithm = null)
        {
            
            if (algorithm == null)
            {
                algorithm = GetDefaultAlgorithm();
            }

            var hash = algorithm.GetHash(component);
            return hash;
        }
        public static string GetHash(this string text, IAlgorithm algorithm = null)
        {
            if (algorithm == null)
            {
                algorithm = GetDefaultAlgorithm();
            }

            var hash = algorithm.GetHash(text);
            return hash;
        }
        public static IAlgorithm GetDefaultAlgorithm()
        {
            return new Sha256();
        }
    }
}
