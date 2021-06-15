using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjetoDFS.Utils
{
    public class ValidationFunctions
    {
        public static bool IsValidCpf(string cpf)
        {
            if (cpf == null || cpf.Length != 11)
            {
                return false;
            }

            if (!cpf.All(ch => Char.IsDigit(ch)))
            {
                return false;
            }

            // Verify if all digits are the same
            if (cpf.All(ch => ch == cpf[0] || !Char.IsDigit(ch)))
            {
                return false;
            }

            var cpfDigitsArray = cpf.Select(d => Int16.Parse(d.ToString())).ToArray();

            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += cpfDigitsArray[i] * (10 - i);
            }

            int mod = sum % 11;

            int firstDigit = 0;

            if (mod > 1)
            {
                firstDigit = 11 - mod;
            }

            if (firstDigit != cpfDigitsArray[9]) {
                return false;
            }

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += cpfDigitsArray[i] * (11 - i);
            }

            mod = sum % 11;

            int secondDigit = 0;

            if (mod > 1)
            {
                secondDigit = 11 - mod;
            }

            if (secondDigit != cpfDigitsArray[10]) {
                return false;
            }

            return true;
        }

        public static bool IsValidCnpj(string cnpj)
        {
            if (cnpj == null || cnpj.Length != 14)
            {
                return false;
            }

            if (!cnpj.All(ch => Char.IsDigit(ch)))
            {
                return false;
            }

            if (cnpj.All(ch => ch == cnpj[0]))
            {
                return false;
            }

            short[] cnpjDigitsArray = cnpj.Select(d => Int16.Parse(d.ToString())).ToArray();
            short[] weights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += cnpjDigitsArray[i] * weights[i];
            }

            int mod = sum % 11;
            int firstDigit = 0;

            if (mod > 1)
            {
                firstDigit = 11 - mod;
            }

            if (firstDigit != cnpjDigitsArray[12])
            {
                return false;
            }

            sum = cnpjDigitsArray[0] * 6;
            for (int i = 1; i < 13; i++)
            {
                sum += cnpjDigitsArray[i] * weights[i - 1];
            }

            mod = sum % 11;
            int secondDigit = 0;

            if (mod > 1)
            {
                secondDigit = 11 - mod;
            }

            if (secondDigit != cnpjDigitsArray[13])
            {
                return false;
            }

            return true;
        }

        public static bool IsValidEmail(string email)
        {
            if (email != null) {
                Regex pattern = new Regex(
                    "(^[a-zA-Z0-9]+([.]{1}[a-zA-Z0-9]+)*)[@]{1}([a-zA-Z0-9]+[.])*[a-zA-Z]+");
            
                return(pattern.IsMatch(email));
            }

            return false;
        }
    }
}
