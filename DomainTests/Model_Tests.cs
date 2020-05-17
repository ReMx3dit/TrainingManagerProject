using DomainLibrary.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainTests
{
    [TestClass]
    public class Model_Tests
    {
        [TestMethod]
        public void RunningSession_AverageSpeed_Calculated_WhenNotProvided()
        {
            float expected = 1.53846153846F;

            var actual = new RunningSession(DateTime.Now.AddHours(-6.5), 10000, TimeSpan.FromHours(6.5), null, TrainingType.Endurance, "lalala").AverageSpeed;

            Should.Equals(expected, actual);
        }

        [TestMethod]
        public void CyclingSession_AverageSpeed_Calculated_WhenNotProvided()
        {
            float expected = 1.53846153846F;

            var actual = new CyclingSession(DateTime.Now.AddHours(-6.5), 10, TimeSpan.FromHours(6.5), null, 1, TrainingType.Endurance, "lalala", BikeType.IndoorBike).AverageSpeed;

            Should.Equals(expected, actual);
        }
    }
}
