using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorTrainingApp.Service;

namespace RazorTrainingApp.Service
{


    public class Calculations
    {
        public double CalculateDiscount(int numberOfFlys, int ticketPrice)
        {
            if(numberOfFlys < 0)
            {
                throw new ArgumentException("Value cant be less than zero!");
            }

            switch (numberOfFlys)
            {
                case var x when x > 0 && x <= 10:
                    return 1.0 * ticketPrice;
                case var x when x > 10 && x <= 15:
                    return 0.8 * ticketPrice;
                case var x when x > 15 && x <= 20:
                    return 0.6 * ticketPrice;
                case var x when x > 20:
                    return 0.5 * ticketPrice;
            }

            throw new Exception("Bad value");
        }

        public KindOfClient GetKindOfClient(int numbersOfFlys)
        {
            switch (numbersOfFlys)
            {
                case var x when x > 0 && x <= 10:
                    return KindOfClient.New;
                case var x when x > 10 && x <= 15:
                    return KindOfClient.Regular;
                case var x when x > 15 && x <= 20:
                    return KindOfClient.Premium;
                case var x when x > 20:
                    return KindOfClient.Platinium;     
            }

            throw new Exception("Bad value");
        }
    }
}
