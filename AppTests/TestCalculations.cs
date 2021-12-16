using Moq;
using RazorTrainingApp;
using RazorTrainingApp.Models;
using RazorTrainingApp.Repository;
using RazorTrainingApp.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace AppTests
{
    public class TestCalculations
    {
        private readonly Calculations _sut;

        public TestCalculations()
        {
            _sut = new Calculations();
        }
        //nameOfMethod_Scenario_result
        [Fact]
        public void GetKindOfClient_ifNumberOfFlyIsSeventeen_Premium()
        {
            var numberOfFly = 17;

            KindOfClient result =  _sut.GetKindOfClient(numberOfFly);

            Assert.Equal(KindOfClient.Premium, result);
        }

        //we can create multiple scenarios in one test
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public void GetKindOfClient_ifNumberOfFlyIsInt_New_single(int numberOfFlys)
        {
            KindOfClient result = _sut.GetKindOfClient(numberOfFlys);
            Assert.Equal(KindOfClient.New, result);
        }

        [Theory]
        [InlineData(1 , KindOfClient.New)]
        [InlineData(10, KindOfClient.New)]
        [InlineData(13, KindOfClient.Regular)]
        [InlineData(21, KindOfClient.Platinium)]
        public void GetKindOfClient_ifNumberOfFlyIsGiven_CorrectClassification(int numberOfFlys, KindOfClient kindOfClient)
        {
            KindOfClient result = _sut.GetKindOfClient(numberOfFlys);
            Assert.Equal(kindOfClient, result);
        }

        [Theory]
        [InlineData(1, 100, 100)]
        [InlineData(10, 100, 100)]
        [InlineData(13, 100, 80)]
        [InlineData(17, 100, 60)]
        [InlineData(24, 100, 50)]
        public void CalculateDiscount_ForGivenNumberOFFlysAndPrice_ReturnCorrectPrice(int numberOfFlys, int ticketPrice, double finalPrice)
        {
            double result = _sut.CalculateDiscount(numberOfFlys, ticketPrice);
            Assert.Equal(finalPrice, result);
        }


        [Theory]
        [InlineData(-1, 9)]
        public void CalculateDiscount_ForInvalidArguments_ThrowArgumentException(int numberOfFlys, int ticketPrice)
        {
            //we have to "box" this value in delegate action, cuz we dont care about return thats why action ( void )
            Action action = () => _sut.CalculateDiscount(numberOfFlys, ticketPrice);
            Assert.Throws<ArgumentException>(action);
        }
    }
}
