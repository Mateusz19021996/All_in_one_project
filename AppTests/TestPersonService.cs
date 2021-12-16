using Moq;
using RazorTrainingApp;
using RazorTrainingApp.Models;
using RazorTrainingApp.Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace AppTests
{
    public class TestPersonService
    {
        private readonly Mock<IDataBase> _dbMock = new Mock<IDataBase>();
        private readonly IPersonService _sut;

        public TestPersonService()
        {
            _sut = new PersonService(_dbMock.Object);
        }

        [Fact]
        public void Person_service_getAll_if_empty_test()
        {
            _dbMock
                .Setup(x => x.GetList())
                .Returns(value: null);

            var persons = _sut.GetAllPersons();

            Assert.Null(persons);
        }

        [Fact]
        public void Person_service_getAll_check_if_method_will_invoke()
        {
            var id = It.IsAny<int>();

            _dbMock
                .Setup(x => x.GetList())
                .Returns(GetPersonsMoq());

            var persons = _sut.GetAllPersons();
            
            //check if method from repository will be invoked
            _dbMock.Verify(v => v.GetPersonById(id), Times.Never);
            _dbMock.Verify(v => v.GetList(), Times.Once);
        }

        [Fact]
        public void Person_service_getById_check_if_person_exist()
        {
            var id = 1;

            _dbMock
                .Setup(x => x.GetPersonById(id))
                .Returns(GetSinglePersonMoq());

            var person = _sut.GetPerson(id);

            Assert.Equal("Mateusz", person.Name);          
        }

        [Fact]
        public void Person_service_count()
        {
            var id = 1;

            _dbMock
                .Setup(x => x.GetList())
                .Returns(GetPersonsMoq);

            var countPersons = _sut.CountPersons();

            Assert.Equal(2, countPersons);
        }

        [Fact]
        public void Person_service_getUserByName_if_Exist()
        {
            var name = "Mateusz";

            _dbMock
                .Setup(x => x.GetPersonByName(name))
                .Returns("Mateusz Marciniak");

            var personName = _sut.GetPersonByName(name);

            Assert.Equal("Mateusz Marciniak", personName);
        }

        [Fact]
        public void Person_service_toUpper()
        {
            var name = "Mateusz";

            var personName = _sut.UpperCase(name);

            Assert.Equal("MATEUSZ", personName);
        }

        public Person GetSinglePersonMoq()
        {
            var person = new Person() { Id = 1, Name = "Mateusz", Surname = "Marciniak", Age = 48, Nationality = "Poland" };

            return person;
        }

        public IEnumerable<Person> GetPersonsMoq()
        {
            var PersonsMoq = new List<Person>()

                {
                    new Person(){Id = 1, Name = "Mateusz", Surname = "Marciniak", Age = 48, Nationality = "Poland"},
                    new Person(){Id = 2, Name = "Tomasz", Surname = "Stachowiak", Age = 25, Nationality = "Poland"},
                };

            return PersonsMoq;
        }
        
    }
}
