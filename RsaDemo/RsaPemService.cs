using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class RsaPemService : IRsaService
    {
        private static string _privateKeyFileName = "private_pem.key";

        private static string _publicKeyFileName = "public_pem.key";
        public string Title { get => "PEM"; set => throw new NotImplementedException(); }

        public string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var pKeyString = Convert.FromBase64String(File.ReadAllText(_privateKeyFileName));
            int bytesRead = 0;
            rsa.ImportPkcs8PrivateKey(pKeyString, out bytesRead);

            var decryptBytes = rsa.Decrypt(Convert.FromBase64String(data), false);

            return Encoding.UTF8.GetString(decryptBytes);
        }

        public string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var pKeyString = Convert.FromBase64String(File.ReadAllText(_publicKeyFileName));
            int bytesRead = 0;
            rsa.ImportRSAPublicKey(pKeyString, out bytesRead);

            var decryptBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(data), false);

            return Convert.ToBase64String(decryptBytes);
        }

        public void Export(RSA rsa)
        {
            var pKeyBytes = rsa.ExportPkcs8PrivateKey();
            var pKeyString = Convert.ToBase64String(pKeyBytes);
            File.WriteAllText(_privateKeyFileName, pKeyString);

            var pubKeyBytes = rsa.ExportRSAPublicKey();
            var pubKeyString = Convert.ToBase64String(pubKeyBytes);
            File.WriteAllText(_publicKeyFileName, pubKeyString);
        }
    }
}
