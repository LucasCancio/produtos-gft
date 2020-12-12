using System.Security.Cryptography;
using System.Text;

namespace ProdutosGFT.Domain.Util.Helpers
{
    public static class MD5Hash
    {
        public static string CreateHash(string value)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(value);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }

    }
}