using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
        private Task<Timeslot> timeslot = Task.FromResult(new Timeslot());

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
            mocktimeslotRepo.Setup(r => r.GetByIdAsync(3)).Returns(timeslot);
            var controller = new TimeslotsController(mocktimeslotRepo.Object);

            //act
            var result = await controller.Details(3);
            ViewResult viewResult = result as ViewResult;
            //assert
            Assert.IsNotNull(result);
            //Assert.IsNotNull(viewResult.Model);
            //Assert.IsInstanceOfType(viewResult.Model, typeof(Timeslot));
            //Assert.AreEqual(9, ((Timeslot)viewResult.Model).Id);

        }

        private Task<List<Room>> roomlist = Task.FromResult(new List<Room>());
        [TestMethod]
        public async Task roomController_Index_Return_Rooms()
        {
            var mockroomrepo = new Mock<IRoomRepository>();
            mockroomrepo.Setup(r => r.GetAllAsync()).Returns(roomlist);

            ILogger<RoomsController> logger = new Logger<RoomsController>(new NullLoggerFactory());

            var controller = new RoomsController(mockroomrepo.Object, logger);

            var result = await controller.Index();
            ViewResult viewResult = result as ViewResult;
            //assert
            Assert.IsNotNull(result);
            //Assert.IsNotNull(viewResult.Model);
            //Assert.IsInstanceOfType(viewResult.Model, typeof(Timeslot));
            //Assert.AreEqual(9, ((Timeslot)viewResult.Model).Id);

        }


    }
}
