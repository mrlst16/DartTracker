using DartTracker.Lib.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Test.Helpers
{
    [TestClass]
    public class DartGameIncrementorTests
    {
        [TestMethod]
        public async Task OneShot()
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(2);
            incrementor = incrementor.Increment();

            Assert.AreEqual(1, incrementor.Round);
            Assert.AreEqual(1, incrementor.Turn);
            Assert.AreEqual(0, incrementor.PlayerUp);
            Assert.AreEqual(1, incrementor.Shots);
        }

        [TestMethod]
        public async Task TwoShots()
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(2);
            incrementor = incrementor.Increment(2);

            Assert.AreEqual(1, incrementor.Round);
            Assert.AreEqual(1, incrementor.Turn);
            Assert.AreEqual(0, incrementor.PlayerUp);
            Assert.AreEqual(2, incrementor.Shots);
        }

        [TestMethod]
        public async Task ThreeShots()
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(2);
            incrementor = incrementor.Increment(3);

            Assert.AreEqual(1, incrementor.Round);
            Assert.AreEqual(2, incrementor.Turn);
            Assert.AreEqual(1, incrementor.PlayerUp);
            Assert.AreEqual(3, incrementor.Shots);
        }

    }
}
