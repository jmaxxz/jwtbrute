using ConsoleOptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace jwtbrute
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawToken = null;
            var options = new Options()
            {
                new Option((s)=>rawToken=s, "token", "The json web token to be cracked")
            };
            if (!options.Parse(args)) return;

            var jwt = rawToken.Split('.');
            var prehash = jwt[0]+"."+jwt[1];
            var hash = DecodeUrlBase64(jwt[2]);

            BruteForceSecret(ReadStdInByLine(), prehash, hash);
        }

        private static IEnumerable<string> ReadStdInByLine()
        {
            string s;
            while ((s = Console.ReadLine()) != null)
            {
                yield return s;
            }
            
        }

        private static void BruteForceSecret(IEnumerable<string> words, string prehash, byte[] hash)
        {
            using (HMACSHA256 hm = new HMACSHA256())
            {
                foreach (var w in words)
                {
                    hm.Key = Encoding.UTF8.GetBytes(w);
                    var h = hm.ComputeHash(Encoding.UTF8.GetBytes(prehash));
                    if (StructuralComparisons.StructuralEqualityComparer.Equals(h, hash))
                    {
                        Console.WriteLine("Secret was: " + w);
                        break;
                    }
                }
            }
        }

        private static byte[] DecodeUrlBase64(string p)
        {
            p = p.Replace('-', '+');
            p = p.Replace('_', '/');
            p = p + "==".Substring(0, p.Length % 3);
            return Convert.FromBase64String(p);
        }
    }
}
