using MeterReadingApi.Domain;
using System.Reflection;

namespace MeterReadingApi.Tests
{
    public class MeterReadingTests
    {
        [Fact]
        public void Constructor_MeterReadValueNegativeValue_ShouldThrowArgumentOutOfRangeException()
        {
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var reading = new MeterReading(1, new DateTime(2000, 1, 1), -1);
            });
        }

        [Fact]
        public void Constructor_MeterReadValueGreaterThan99999_ShouldThrowArgumentOutOfRangeException()
        {
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var reading = new MeterReading(1, new DateTime(2000, 1, 1), 100000);
            });
        }

        [Fact]
        public void Constructor_MeterReadingDateTimeInFuture_ShouldThrowArgumentOutOfRangeException()
        {
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var reading = new MeterReading(1, DateTime.Now.AddDays(1), 1234);
            });
        }

        [Fact]
        public void Constructor_ValidParameters_ShouldCreateInstance()
        {
            //Act
            var reading = new MeterReading(1, new DateTime(2000, 1, 1), 1234);
            //Assert
            Assert.NotNull(reading);
            Assert.Equal(1, reading.AccountId);
            Assert.Equal(new DateTime(2000, 1, 1), reading.MeterReadingDateTime);
            Assert.Equal(1234, reading.MeterReadValue);
        }

        [Fact]
        public void Constructor_MeterReadValueExactly0_ShouldBeValid()
            {
            //Act
            var reading = new MeterReading(1, new DateTime(2000, 1, 1), 0);
            //Assert
            Assert.NotNull(reading);
            Assert.Equal(0, reading.MeterReadValue);
        }

        [Fact]
        public void Constructor_MeterReadValueExactly99999_ShouldBeValid()
        {
            //Act
            var reading = new MeterReading(1, new DateTime(2000, 1, 1), 99999);
            //Assert
            Assert.NotNull(reading);
            Assert.Equal(99999, reading.MeterReadValue);
        }

    }
}