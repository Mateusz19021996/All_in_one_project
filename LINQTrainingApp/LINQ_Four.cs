using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQTrainingApp
{
    public static  class LINQ_Four
    {
        public static IEnumerable<Continent> LoadContinents(string path)
        {
            var myFile = File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line => line.Length > 1)
                    .ToContinent();

            return myFile;
        }

        public static void JoinCheck()
        {
            var persons = LINQ_Three.LoadPerson("persons.csv");
            var continents = LoadContinents("continents.csv");

            //LINQ query 
            var query = from person in persons
                        join continent in continents 
                        on person.Nationality equals continent.Nationality
                        // join po więcej niz jednym propertisie, w przypadku jak są rózne nazwy trzeba zrobic jak z Propertis1
                        //on { person.Propertis1, person.Propertis2} equals { Propertis1 = continent.prop1, contonent.Propertis2}
                        orderby person.Nationality
                        select new
                        {
                            person.Name,
                            person.Nationality,
                            continent.Continentt
                        };

            var syntax = persons
                .Join(continents,
                        p => p.Nationality,
                        c => c.Nationality,
                        (p, c) => new
                        {
                            p.Name,
                            p.Age,
                            c.Nationality,
                            c.Continentt

                            //Person = p,
                            //Continent = c
                            // + select, mamy dostęp do wszystkich propertis
                        })
                .OrderByDescending(n => n.Age)
                .ThenBy(n => n.Nationality);
            //.Select(n => new
            // {
            //    n.Person.Cokolwiek,
            //    n.Continent.cokolwiek
            // })

            // DOUBLE JOIN SYNTAX
            //var syntax2 = persons
            //    .Join(continents,
            //            p => new { p.Nationality, p.Propertis2}
            //            c => new { Propertis1 = continent.prop1, contonent.Propertis2}
            //            (p, c) => new
            //            {
            //                p.Name,
            //                p.Age,
            //                c.Nationality,
            //                c.Continentt
            //            })
            //    .OrderByDescending(n => n.Age)
            //    .ThenBy(n => n.Nationality);

            foreach (var joinedPerson in query)
            {
                Console.WriteLine($"{joinedPerson.Name} {joinedPerson.Nationality} {joinedPerson.Continentt}");
            }

            foreach (var joinedPerson in syntax)
            {
                Console.WriteLine($"{joinedPerson.Name} {joinedPerson.Nationality} {joinedPerson.Continentt}");
            }
        }

        public static void GroupCheck()
        {
            var persons = LINQ_Three.LoadPerson("persons.csv");
            var continents = LoadContinents("continents.csv");

            var query = from person in persons
                        group person by person.Nationality into per
                        orderby per.Key
                        select per;

            var syntax = persons
                .GroupBy(x => x.Nationality)
                .OrderBy(y => y.Key);

            var query2 = from continent in continents
                         join person in persons on continent.Nationality equals person.Nationality 
                         into personGroup
                         orderby continent.Nationality
                         select new
                         {
                             SingleContinent = continent,
                             Persons = personGroup
                         };

            var syntax2 = continents.GroupJoin(persons, c => c.Nationality, p => p.Nationality,
                (c, p) => new
                {
                    SingleContinent = c,
                    Persons = p

                }).OrderBy(c => c.SingleContinent.Nationality);

            foreach (var person in query2)
            {
                Console.WriteLine($"Nationality: {person.SingleContinent.Nationality}");
                foreach (var groupOfPersons in person.Persons)
                {
                    Console.WriteLine(groupOfPersons.Name);
                }
            }

            Console.WriteLine("--------------------------------------------");

            foreach (var person in syntax2)
            {
                Console.WriteLine($"Nationality: {person.SingleContinent.Nationality}");
                foreach (var groupOfPersons in person.Persons)
                {
                    Console.WriteLine(groupOfPersons.Name);
                }
            }

            //foreach (var person in query)
            //{
            //    Console.WriteLine($"Nationality: {person.Key} has {person.Count()} persons");
            //}

        }

        public static void AggregatingCheck()
        {
            var persons = LINQ_Three.LoadPerson("persons.csv");
            var continents = LoadContinents("continents.csv");

            var query =
                from person in persons
                group person by person.Nationality into personGropu
                select new
                {
                    Name = personGropu.Key,
                    MaxAge = personGropu.Max(p => p.Age),
                    MinAge = personGropu.Min(p => p.Age),
                    Avg = personGropu.Average(p => p.Age)
                } into result
                orderby result.MaxAge
                select result;

            foreach (var result in query)
            {
                Console.WriteLine($"Nationality: {result.Name}");
                Console.WriteLine($"Max: {result.MaxAge}");
                Console.WriteLine($"Min: {result.MinAge}");
            }
        }
        //test
    }

    public static class ContinentsExtensions
    {
        public static IEnumerable<Continent> ToContinent(this IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var columns = line.Split(',');

                yield return new Continent
                {
                    Nationality = columns[0],
                    Continentt = columns[1]
                };

            }
        }
    }
}

/*
 * IQueryable - notatnki, w skrócie strzela do bazy bardziej dokładnym zapytaniem i nie 
 * wyciąga z niej wszsytkiego do pamięci tylko to co potrzeba
 */