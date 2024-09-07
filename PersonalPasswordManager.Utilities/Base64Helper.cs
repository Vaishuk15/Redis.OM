using System.Text;

namespace PersonalPasswordManager.Utilities
{

    public static class Base64Helper
    {
        // Method to encode a string to Base64
        public static string EncodeToBase64(string plainText)
        {
            var plainTextBytes = Encoding.ASCII.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        // Method to decode a Base64 string back to plain text
        public static string DecodeFromBase64(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.ASCII.GetString(base64EncodedBytes);
        }
    }

}
