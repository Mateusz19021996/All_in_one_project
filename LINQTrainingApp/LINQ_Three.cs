using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTrainingApp
{
    public class LINQ_Three
    {

        public static void CheckOrderAndFirst()
        {
            var personsLoad = LoadPerson("persons.csv");

            var persons = personsLoad
                .OrderByDescending(x => x.Name)
                .ThenBy(x => x.Age);

            var isAny = personsLoad.Any();
            var isAny2 = personsLoad.Any(x => x.Name == "Mateusz");
            var areAll = personsLoad.All(x => x.Age > 1);
            //var containOp = personsLoad.Contains(OBJECT)

            var onePerson = personsLoad
                .OrderByDescending(x => x.Name)
                .ThenBy(x => x.Age)
                //not deferred, pobiera pierwszą wartość
                .First(x => x.Name == "Mateusz");
            // FirstOrDefault
            // LastOrDefault - obie metody zwracaja domyslna wartosc dla danego obiektu / typu jesli nie istnieje
            // prawdopodobnie bardziej wydajne będzie najpierw pokazanie warunku w WHERE a potem dopiero .Take(1)

            foreach (var person in persons.Take(5))
            {
                Console.WriteLine(person.Name);
            }

            var query = from person in personsLoad
                        where person.Age > 18
                        orderby person.Name descending, person.Nationality ascending
                        select person;

            var queryForOne = (from person in personsLoad
                               where person.Age > 18
                               orderby person.Name descending, person.Nationality ascending
                               select person).First();
        }


        //projekcja danych
        public static List<Person> LoadPerson(string path)
        {
            var myFile = File.ReadAllLines(path)
                .Skip(1)
                // here we take only lines where we have literally something
                .Where(line => line.Length > 1)
                .Select(Person.ParseFromCsv)
                .ToList();

            //var query = from line in File.ReadAllLines(path).Skip(1)
            //            where line.Length > 1
            //            select Person.ParseFromCsv(line);
            //query.ToList();

            var myFile2 = File.ReadAllLines(path)
                .Skip(1)
                // here we take only lines where we have literally something
                .Where(line => line.Length > 1)
                .ToPerson();

            return myFile;
        }        
    }

    public static class PersonExtensions
    {
        //jako drugi parametr mozna dać Func i wtedy mozna dodawać logikę
        public static IEnumerable<Person> ToPerson(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                // rozdziela nam to co jest pomiędzy przecinkami na tablicę stringow
                var columns = line.Split(',');

                yield return new Person
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
}

/*
 * 
 * 
 * 
 * 
 */
