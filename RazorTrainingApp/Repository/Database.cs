using RazorTrainingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorTrainingApp.Repository
{
    public interface IDataBase
    {
        public IEnumerable<Person> GetList();
        public Person GetPersonById(int id);
        public string GetPersonByName(string name);
    }

    public class Database : IDataBase
    {
        public IEnumerable<Person> persons = new List<Person>()
        {
            new Person(){Id = 1, Name = "Mateusz", Surname = "Marciniak", Age = 48, Nationality = "Poland"},
            new Person(){Id = 2, Name = "Tomasz", Surname = "Stachowiak", Age = 25, Nationality = "Poland"},
            new Person(){Id = 3, Name = "Marcin", Surname = "Wichlecki", Age = 63, Nationality = "Poland"},
            new Person(){Id = 4, Name = "Joasia", Surname = "Robak", Age = 14, Nationality = "Germany"},
            new Person(){Id = 5, Name = "Edward", Surname = "Dekret", Age = 8, Nationality = "Poland"},
            new Person(){Id = 6, Name = "Marlena", Surname = "Iwonarska", Age = 13, Nationality = "England"},
            new Person(){Id = 7, Name = "Tamara", Surname = "Olkowska", Age = 91, Nationality = "Germany"}
        };

        public IEnumerable<Person> GetList()
        {
            return persons;
        }

        public Person GetPersonById(int id)
        {
            var person = persons
                .FirstOrDefault(x => x.Id == id);

            return person;
        }

        public string GetPersonByName(string name)
        {
            var person = persons
                .FirstOrDefault(x => x.Name == name);
            if(person == null)
            {
                return "User does not exist"; 
            }

            return $"{person.Name} {person.Surname}";
        }


    }
}
