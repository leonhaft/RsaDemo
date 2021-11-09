using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------start-------------");

            //Generate a public/private key pair.  
            RSA rsa = RSA.Create();
            //Save the public key information to an RSAParameters structure.  
            RSAParameters rsaKeyInfo = rsa.ExportParameters(false);

            var p = DateTime.Now.Ticks;

            var date = new DateTime(p);
            Console.WriteLine(date);

            var rsaServices = new List<IRsaService>
            {
                new RsaPemService(),
                new RsaXmlService()
            };


            var text = "Forza Horizon 5";
            foreach (var service in rsaServices)
            {
                Console.WriteLine($"---------------{service.Title}-------------");

                service.Export(rsa);

                var encryptText = service.Encrypt(text);

                Console.WriteLine($"decryt:{encryptText}");

                var decrytText = service.Decrypt(encryptText);


                Console.WriteLine($"decryt:{decrytText}");

                Console.WriteLine($"---------------{service.Title}-------------");

            }


            Console.WriteLine("---------------end-------------");

        }

    }
}
