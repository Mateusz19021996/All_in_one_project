using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTrainingApp
{
    public static class LINQ_Two
    {
        public static void OwnFilterExecute()
        {
            var list = GetPersons();

            Console.WriteLine("Where:");
            var persons = list
                //zwrotka IEnumerable<perssosn>, pod spodem musimy dodac func<person, bool> czyli czy chcemy dana osobe czy jej nie chcemy
                .Where(p => p.Age > 18);

            foreach (var person in persons)
            {
                //Console.WriteLine(person.Name);
            }

            Console.WriteLine("Yield:");
            var personsYield = list.OwnFilterWithYield(p => p.Age > 18);

            foreach (var person in personsYield)
            {
                //Console.WriteLine(person.Name);
            }

            Console.WriteLine("no Yield:");
            var personsNoYield = list.OwnFilter(p => p.Age > 18);
            foreach (var person in personsNoYield)
            {
                //Console.WriteLine(person.Name);
            }

            Console.WriteLine("Using Enumerator");

            var personsEn = list.OwnFilter(x => x.Age > 18);

            var enumerator = personsEn.GetEnumerator();
            while (enumerator.MoveNext())
            {
                //Console.WriteLine(enumerator.Current.Name);
            }
            
        }

        public static List<Person> GetPersons()
        {
            var persons = new List<Person>()
            {
                    new Person(){Id = 1, Name = "Mateusz", Surname = "Marciniak", Age = 48, Nationality = "Poland"},
                    new Person(){Id = 2, Name = "Tomasz", Surname = "Stachowiak", Age = 25, Nationality = "Poland"},
                    new Person(){Id = 3, Name = "Marcin", Surname = "Wichlecki", Age = 63, Nationality = "Poland"},
                    new Person(){Id = 4, Name = "Joasia", Surname = "Robak", Age = 14, Nationality = "Germany"},
                    new Person(){Id = 5, Name = "Edward", Surname = "Dekret", Age = 8, Nationality = "Poland"},
                    new Person(){Id = 6, Name = "Marlena", Surname = "Iwonarska", Age = 13, Nationality = "England"},
                    new Person(){Id = 7, Name = "Tamara", Surname = "Olkowska", Age = 91, Nationality = "Germany"}
            };
            return persons;
        }
    }
}
