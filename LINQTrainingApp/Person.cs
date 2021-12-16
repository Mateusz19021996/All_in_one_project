using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTrainingApp
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //private by default
        int _age;
        public int Age 
        { get
            {
                //throw new Exception("My exception");
                //Console.WriteLine($"returning {Name} {_age}");
                return _age;
            }
            set
            {
                _age = value;
            }
        }

        public string Surname { get; set; }

        public string Nationality { get; set; }

        internal static Person ParseFromCsv(string line)
        {
            // rozdziela nam to co jest pomiędzy przecinkami na tablicę stringow
            var columns = line.Split(',');

            return new Person
            {
                Id = int.Parse(columns[0]),
                Name = columns[1],
                Surname = columns[2],
                Age = int.Parse(columns[3]),
                Nationality = columns[4]
            };
        }
    }
}
