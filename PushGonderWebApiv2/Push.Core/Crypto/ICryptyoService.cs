using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Crypto
{
    public interface ICryptyoService
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
        string Base64Decode(string base64EncodedData);
        string Base64Encode(string plainText);
        string HashPassword(string password);
    }
}
    