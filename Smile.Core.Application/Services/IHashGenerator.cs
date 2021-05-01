namespace Smile.Core.Application.Services
{
    public interface IHashGenerator
    {
        void GenerateHash(string text, string textSalt, out string saltedTextHash);
        string CreateSalt(int saltSize = 128);
        bool VerifyHash(string text, string saltedTextHash, string textSalt);
        byte[] CombineByteArrays(params byte[][] arrays);
    }
}