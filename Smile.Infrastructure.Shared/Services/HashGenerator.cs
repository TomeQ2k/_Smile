using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Smile.Core.Application.Extensions;
using Smile.Core.Application.Services;

namespace Smile.Infrastructure.Shared.Services
{
    public class HashGenerator : IHashGenerator
    {
        public void GenerateHash(string text, string textSalt, out string saltedTextHash)
        {
            var textBinary = Encoding.UTF8.GetBytes(text);
            var textSaltBinary = Encoding.UTF8.GetBytes(textSalt);

            using (SHA512 sha512 = SHA512.Create())
            {
                var textHashBinary = sha512.ComputeHash(CombineByteArrays(textBinary, textSaltBinary));
                saltedTextHash = textHashBinary.ConvertHashToString();
            }
        }

        public string CreateSalt(int saltSize = 128)
        {
            var rngCryptoService = new RNGCryptoServiceProvider();

            byte[] saltBinary = new byte[saltSize];
            rngCryptoService.GetBytes(saltBinary);

            var salt = BitConverter.ToString(saltBinary).Replace("-", "");

            return salt;
        }

        public bool VerifyHash(string text, string saltedTextHash, string textSalt)
        {
            string textHashToCheck = string.Empty;

            GenerateHash(text, textSalt, out textHashToCheck);

            for (int i = 0; i < textHashToCheck.Length; i++)
                if (textHashToCheck[i] != saltedTextHash[i]) return false;

            return true;
        }

        public byte[] CombineByteArrays(params byte[][] arrays)
        {
            byte[] resultArray = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;

            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, resultArray, offset, data.Length);
                offset += data.Length;
            }

            return resultArray;
        }
    }
}