using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public class PaymentService
    {
        public string HideNumbers(string creditCardNumber)
        {
            string newNumbers = "";

            for (int i = 0; i < creditCardNumber.Length; i++)
            {
                if (i < 12)
                {
                    newNumbers = newNumbers + "*";
                }
                else
                {
                    newNumbers = newNumbers + creditCardNumber[i].ToString();
                }
            }
            return newNumbers;
        }
    }
}
