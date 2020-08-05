using System;
using Xunit;
using neatproject.Controllers;
using Moq;
using Microsoft.Extensions.Logging;

namespace NeatProject.Api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var logger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(logger.Object);
            Assert.True(true);
        }
    }
}
