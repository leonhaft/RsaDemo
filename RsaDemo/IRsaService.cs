using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public interface IRsaService
    {
        string Title { get; set; }
        void Export(RSA rsa);

        string Encrypt(string data);

        string Decrypt(string data);
    }
}
