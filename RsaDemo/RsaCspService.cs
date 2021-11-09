using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsaDemo
{
    public class RsaCspService : IRsaService
    {
        private static string _privateKeyFileName = "private_csp.key";

        private static string _publicKeyFileName = "public_csp.key";
        public string Title { get => "CspBlob"; set => throw new NotImplementedException(); }

        public string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var pKeyString = Convert.FromBase64String(File.ReadAllText(_privateKeyFileName));
            rsa.ImportCspBlob(pKeyString);

            var decryptBytes = rsa.Decrypt(Convert.FromBase64String(data), false);

            return Encoding.UTF8.GetString(decryptBytes);
        }

        public string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var pKeyString = Convert.FromBase64String(File.ReadAllText(_publicKeyFileName));
            int bytesRead = 0;
            rsa.ImportCspBlob(pKeyString);

            var decryptBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(data), false);

            return Convert.ToBase64String(decryptBytes);
        }

        public void Export(RSACryptoServiceProvider rsa)
        {
            var pKeyBytes = rsa.ExportCspBlob(true);
            var pKeyString = Convert.ToBase64String(pKeyBytes);
            File.WriteAllText(_privateKeyFileName, pKeyString);

            var pubKeyBytes = rsa.ExportCspBlob(false);
            var pubKeyString = Convert.ToBase64String(pubKeyBytes);
            File.WriteAllText(_publicKeyFileName, pubKeyString);
        }

        public void Export(RSA rsa)
        {
            throw new NotSupportedException();
        }
    }
}
