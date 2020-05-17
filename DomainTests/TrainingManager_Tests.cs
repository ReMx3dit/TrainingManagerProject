using DataLayer;
using DomainLibrary;
using DomainLibrary.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System;

namespace DomainTests
{
    [TestClass]
    public class TrainingManager_Tests
    {
        private TrainingManager TM;
        private Exception ExpectedException;

        [TestInitialize]
        public void Initialize()
        {
            TM = new TrainingManager(new UnitOfWork(new TrainingContextTest()));
            TM.AddCyclingTraining(new DateTime(2020, 4, 21, 16, 00, 00), 40, new TimeSpan(1, 20, 00), 30, null, TrainingType.Endurance, null, BikeType.RacingBike);
            TM.AddCyclingTraining(new DateTime(2020, 4, 18, 18, 00, 00), 40, new TimeSpan(1, 42, 00), null, null, TrainingType.Recuperation, null, BikeType.RacingBike);
            TM.AddCyclingTraining(new DateTime(2020, 4, 19, 16, 45, 00), null, new TimeSpan(1, 0, 00), null, 219, TrainingType.Interval, "5x5 min 270", BikeType.IndoorBike);
            TM.AddRunningTraining(new DateTime(2020, 4, 17, 12, 30, 00), 5000, new TimeSpan(0, 27, 17), null, TrainingType.Endurance, null);
            TM.AddRunningTraining(new DateTime(2020, 4, 19, 12, 30, 00), 5000, new TimeSpan(0, 25, 48), null, TrainingType.Endurance, null);
            TM.AddRunningTraining(new DateTime(2020, 3, 17, 11, 0, 00), 5000, new TimeSpan(0, 28, 10), null, TrainingType.Interval, "3x700m");
            TM.AddRunningTraining(new DateTime(2020, 3, 17, 11, 0, 00), 8000, new TimeSpan(0, 42, 10), null, TrainingType.Endurance, null);
        }


        [TestMethod, TestCategory("ExceptionsTests")]
        public void AddTraining_ShouldThrowException_IfWhenIsBeforeNow()
        {
            ExpectedException = Should.Throw<DomainException>(() => TM.AddCyclingTraining(DateTime.Now.AddHours(5), null, TimeSpan.FromHours(6), null, null, TrainingType.Endurance, null, BikeType.CityBike));
            ExpectedException.Message.ShouldBe("Training is in the future");

            ExpectedException = Should.Throw<DomainException>(() => TM.AddRunningTraining(DateTime.Now.AddHours(5), 10000, TimeSpan.FromHours(6), null, TrainingType.Endurance, null));
            ExpectedException.Message.ShouldBe("Training is in the future");
        }

        [DataTestMethod, TestCategory("ExceptionsTests")]
        [DataRow(-1)]
        [DataRow(50001)]
        [DataRow(null)]
        public void AddTraining_ShouldThrowException_IfDistanceIncorrect(int distance)
        {
            ExpectedException = Should.Throw<DomainException>(() => TM.AddCyclingTraining(DateTime.Now, distance, TimeSpan.FromHours(6), null, null, TrainingType.Endurance, null, BikeType.CityBike));
            ExpectedException.Message.ShouldBe("Distance invalid value");

            ExpectedException = Should.Throw<DomainException>(() => TM.AddRunningTraining(DateTime.Now, distance, TimeSpan.FromHours(6), null, TrainingType.Endurance, null));
            ExpectedException.Message.ShouldBe("Distance invalid value");
        }

        [TestMethod, TestCategory("ExceptionsTests")]
        public void AddTraining_ShouldThrowException_IfTimeSpanIncorrect()
        {
            ExpectedException = Should.Throw<DomainException>(() => TM.AddCyclingTraining(DateTime.Now, null, TimeSpan.FromTicks(-1), null, null, TrainingType.Endurance, null, BikeType.CityBike));
            ExpectedException.Message.ShouldBe("Time invalid value");

            ExpectedException = Should.Throw<DomainException>(() => TM.AddRunningTraining(DateTime.Now, 10000, TimeSpan.FromHours(25), null, TrainingType.Endurance, null));
            ExpectedException.Message.ShouldBe("Time invalid value");
        }
    }
}
