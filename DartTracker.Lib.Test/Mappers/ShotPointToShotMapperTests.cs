using CommonStandard.Models.Math;
using DartTracker.Lib.Mappers;
using DartTracker.Model.Drawing;
using DartTracker.Model.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Test.Mappers
{
    [TestClass]
    public class ShotPointToShotMapperTests
    {
        private readonly DartboardDimensions _dimensions;
        private readonly ShotPointToShotMapper _mapper;

        public ShotPointToShotMapperTests()
        {
            _dimensions = new DartboardDimensions()
            {
                BackgroudCircleDiameter = 1000,
                OneOneThousandthOfHeight = 1.5f,
                OneOneThousandthOfWitdth = .65f,
                InnerCircleDiameter = 500,
                CanvasHeight = 1500,
                CanvasWidth = 600,
                DoublesAndTriplesStrokeWidth = 25,
                DoubleBullCircleDiameter = 25,
                BullseyeCircleDiameter = 50,
                BackgroudCircleRadius = 500,
                ShotDiameter = 10,
                WasCalculated = true
            };
            _mapper = new ShotPointToShotMapper(_dimensions);
        }

        [TestMethod]
        public async Task MapFromX0Y0()
        {
            var point = new Point(0, 0);
            var result = await _mapper.Map(point);

            Assert.AreEqual(ContactType.DoubleBullsEye, result.Contact);
        }
    }
}
