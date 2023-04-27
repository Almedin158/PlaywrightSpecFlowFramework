using System.Text;

namespace PSF.Support
{
    internal static class Utility
    {
        public static string GenerateName(int minLength, int maxLength)
        {
            Random random = new Random();

            int length = random.Next(minLength, maxLength + 1);

            StringBuilder nameBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                char c = (char)random.Next('a', 'z' + 1);

                if (i == 0)
                {
                    c = char.ToUpper(c);
                }

                nameBuilder.Append(c);
            }

            return nameBuilder.ToString();
        }
        public static string GenerateEmail(string name)
        {
            string domain = "email.dummy";
            return $"{name}@{domain}";
        }
        public static int GenerateRandomNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            return random.Next(minNumber, maxNumber + 1);
        }

    }
}
