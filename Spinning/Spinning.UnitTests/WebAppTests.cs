using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Spinning.Models;
using Spinning.Models.Repositories;
using Spinning.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Spinning.UnitTests
{
    [TestClass] public class WebAppTests
    {
        private Task<List<Timeslot>> timeslotList = Task.FromResult(new List<Timeslot>());

        private async Task<Timeslot> GetTimeslot(int id = 3)
        {
            var timeslotObj = timeslotList.Result.Single(r => r.Id == id);
            return await Task.Run(() => timeslotObj);
        }

        [TestMethod]
        public async Task timeslotController_Details_Returns_TimeslotObj()
        {
            //arrange
            var mocktimeslotRepo = new Mock<ITimeSlotRepository>();
            mocktimeslotRepo.Setup(r => r.GetByIdAsync(3)).Returns(GetTimeslot(3));
            var controller = new TimeslotsController(mocktimeslotRepo.Object);

            //act
            ViewResult result = await controller.Details(3) as ViewResult;

            //assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Timeslot));
            Assert.AreEqual(9, ((Timeslot)result.Model).Id);
        }

        
    }
}
