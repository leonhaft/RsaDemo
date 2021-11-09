using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RsaDemo
{
    public interface IRsaService
    {
        string Title { get; set; }
        void Export(RSA rsa);

        void Export(RSACryptoServiceProvider rsa);

        string Encrypt(string data);

        string Decrypt(string data);
    }
}
