using System;
using System.Text;

namespace Core.Helpers.Auth
{
    public class CodeHelper
    {
        public string GenerateRandomCode(int length)
        {
            StringBuilder code = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                code.Append(letter);
            }

            return code.ToString();
        }
    }
}
