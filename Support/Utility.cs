using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace PSF.Support
{
    internal class Utility
    {
        public string GenerateName(int minLength, int maxLength)
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
        public string GenerateEmail(string name)
        {
            string domain = "email.dummy";
            return $"{name}@{domain}";
        }
        public int GenerateNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            return random.Next(minNumber, maxNumber + 1);
        }
        public string GenerateSentence()
        {
            int numberOfWords = GenerateNumber(5, 20);
            string sentence = "";

            for(int i = 0; i < numberOfWords; i++)
            {
                sentence += GenerateName(3, 12);
                sentence += " ";
            }

            sentence = sentence.Substring(0, sentence.Length - 1);
            sentence = sentence.ToLower();

            return char.ToUpper(sentence[0])+ sentence.Substring(1);
        }
        public dynamic ReadFromJsonFile(string filePath)
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            // Read the contents of the file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON into an object
            dynamic obj = JsonConvert.DeserializeObject(json);

            return obj;
        }

        /*
         var obj = _utility.ReadFromJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Json\\DummyJson.json");
            obj.age = 1;
            obj.grades.science = 11;
            _utility.WriteToJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\\Json\\DummyJson.json", obj);
         */

        public void WriteToJsonFile(string filePath, object obj)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            string updatedJson = JsonConvert.SerializeObject(obj);

            File.WriteAllText(filePath, updatedJson);
        }
    }
}
