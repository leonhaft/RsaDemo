using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsaDemo
{
    public class RsaXmlService : IRsaService
    {
        private static string _privateKeyFileName = "private_xml.key";

        private static string _publicKeyFileName = "public_xml.key";

        public string Title { get => "XML"; set => throw new NotImplementedException(); }

        public string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();

            var dataByte = Convert.FromBase64String(data);

            //var pBytes = File.ReadAllText(_privateKeyFileName);
            //rsa.ImportFromPem(pBytes);
            rsa.FromXmlString(File.ReadAllText(_privateKeyFileName));
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return Encoding.UTF8.GetString(decryptedByte);
        }

        public string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            //var pBytes = File.ReadAllText(_publicKeyFileName);
            //rsa.ImportFromPem(pBytes);

            rsa.FromXmlString(File.ReadAllText(_publicKeyFileName));

            var dataToEncrypt = Encoding.UTF8.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false);
            return Convert.ToBase64String(encryptedByteArray);
        }

        public void Export(RSA rsa)
        {
            File.WriteAllText(_privateKeyFileName, rsa.ToXmlString(true));
            File.WriteAllText(_publicKeyFileName, rsa.ToXmlString(false));
        }

        public void Export(RSACryptoServiceProvider rsa)
        {
            Export(rsa as RSA);
        }
    }
}
