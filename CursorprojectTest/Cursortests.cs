using Xunit;
using System.Linq;

namespace CursorprojectTest
{
    using cursorproject.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using cursorproject; // Adjust if WeatherForecast is in a different namespace

    public class Cursortests
    {
        private WeatherForecastController CreateController()
        {
            var logger = new LoggerFactory().CreateLogger<WeatherForecastController>();
            return new WeatherForecastController(logger);
        }

        [Fact]
        public void Get_Returns15Items()
        {
            var controller = CreateController();

            var result = controller.Get();

            Assert.Equal(15, result.Count());
        }

        [Fact]
        public void GetAllWeatherForecastduplicate2_Returns15Items()
        {
            var controller = CreateController();

            var result = controller.GetAllWeatherForecastduplicate2();

            Assert.Equal(15, result.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(15)]
        public void GetById_ValidId_ReturnsOk(int id)
        {
            var controller = CreateController();

            var result = controller.GetById(id);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<WeatherForecast>(okResult.Value);
        }

        [Theory]
        [InlineData(1, "Kishan Y")]
        [InlineData(5, "Bharathi T")]
        [InlineData(15, "Raja G")]
        public void GetById_ValidId_ReturnsExpectedWeatherForecast(int id, string expectedSummary)
        {
            var controller = CreateController();

            var result = controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var weatherForecast = Assert.IsType<WeatherForecast>(okResult.Value);
            Assert.Equal(expectedSummary, weatherForecast.Summary);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(16)]
        [InlineData(100)]
        public void GetById_InvalidId_ReturnsNotFound(int id)
        {
            var controller = CreateController();

            var result = controller.GetById(id);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(16)]
        public void GetById_InvalidId_ReturnsExpectedNotFoundMessage(int id)
        {
            var controller = CreateController();

            var result = controller.GetById(id);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Record with Id {id} not found", notFoundResult.Value);
        }

        // Negative test: Ensure Get does not return null or empty
        [Fact]
        public void Get_ShouldNotReturnNullOrEmpty()
        {
            var controller = CreateController();

            var result = controller.Get();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        // Negative test: Ensure GetAllWeatherForecastduplicate2 does not return null or empty
        [Fact]
        public void GetAllWeatherForecastduplicate2_ShouldNotReturnNullOrEmpty()
        {
            var controller = CreateController();

            var result = controller.GetAllWeatherForecastduplicate2();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        // Negative test: Ensure GetById returns NotFound for negative id
        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void GetById_NegativeId_ReturnsNotFound(int id)
        {
            var controller = CreateController();

            var result = controller.GetById(id);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
