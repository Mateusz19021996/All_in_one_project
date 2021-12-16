using RazorTrainingApp.Models;
using RazorTrainingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorTrainingApp
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAllPersons();
        public Person GetPerson(int id);
        public int CountPersons();
        public string GetPersonByName(string name);
        public string UpperCase(string name);
    }

    public class PersonService: IPersonService
    {
        private readonly IDataBase _db;

        public PersonService(IDataBase db)
        {
            _db = db;
        }

        public IEnumerable<Person> GetAllPersons()
        {
            var persons = _db.GetList();
            if (persons != null)
            {
                return persons;
            }

            return null;
        }

        public Person GetPerson(int id)
        {
            var person = _db.GetPersonById(id);
            if(person != null)
            {
                return person;
            }

            return null;
        }

        public int CountPersons()
        {
            var persons = _db.GetList()
                .Count();

            return persons;
        }

        public string GetPersonByName(string name)
        {
            var person = _db.GetPersonByName(name);

            return person;
        }

        public string UpperCase(string name)
        {
            return name.ToUpper();
        }
    }
}
