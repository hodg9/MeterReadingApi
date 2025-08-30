using MeterReadingApi.Application.Dtos;
using MeterReadingApi.Application.Interfaces;
using MeterReadingApi.Application.Services;
using MeterReadingApi.Domain;
using Moq;

namespace MeterReadingApi.Tests
{
    public class MeterReadingMapperTests
    {
        [Fact]
        public void Map_GivenDuplicateInTheSameFile_ShouldReject()
        {
            //Arrange
            var testAccountId = 1;
            var yesterday = DateTime.Now.AddDays(-1);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId)).Returns(new Account("Test", "User"));
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            mockMeterReadingRepository.Setup(r => r.GetReading(1, yesterday)).Returns((MeterReading?)null);
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId, yesterday, 1234),
                new(testAccountId, yesterday, 1234)
            };

            //Act
            var result = mapper.Map(data);

            //Assert
            Assert.Single(result.ValidRecords);
            Assert.Single(result.InvalidRecords);
        }

        [Fact]
        public void Map_GivenDuplicateAlreadyPersisted_ShouldReject()
        {
            //Arrange
            var testAccountId = 1;
            var yesterday = DateTime.Now.AddDays(-1);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId)).Returns(new Account("Test", "User"));
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            mockMeterReadingRepository.Setup(r => r.GetReading(1, yesterday)).Returns(new MeterReading(testAccountId, yesterday, 1234));
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId, yesterday, 1234)
            };

            //Act
            var result = mapper.Map(data);

            //Assert
            mockMeterReadingRepository.Verify(r => r.GetReading(1, yesterday), Times.Once);
            Assert.Empty(result.ValidRecords);
            Assert.Single(result.InvalidRecords);
        }

        [Fact]
        public void Map_GivenNonExistentAccount_ShouldReject()
        {
            //Arrange
            var testAccountId = 1;
            var yesterday = DateTime.Now.AddDays(-1);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId)).Returns((Account?)null);
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId, yesterday, 1234)
            };
            //Act
            var result = mapper.Map(data);
            //Assert
            Assert.Empty(result.ValidRecords);
            Assert.Single(result.InvalidRecords);
        }

        [Fact]
        public void Map_GivenFutureDate_ShouldReject()
        {
            //Arrange
            var testAccountId = 1;
            var tomorrow = DateTime.Now.AddDays(1);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId)).Returns(new Account("Test", "User"));
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId, tomorrow, 1234)
            };
            //Act
            var result = mapper.Map(data);
            //Assert
            Assert.Empty(result.ValidRecords);
            Assert.Single(result.InvalidRecords);
        }

        [Fact]
        public void Map_GivenNegativeMeterReadValue_ShouldReject()
        {
            //Arrange
            var testAccountId = 1;
            var yesterday = DateTime.Now.AddDays(-1);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId)).Returns(new Account("Test", "User"));
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId, yesterday, -1)
            };
            //Act
            var result = mapper.Map(data);
            //Assert
            Assert.Empty(result.ValidRecords);
            Assert.Single(result.InvalidRecords);
        }

        [Fact]
        public void Map_GivenTooLargeMeterReadValue_ShouldReject()
        {
            //Arrange
            var testAccountId = 1;
            var yesterday = DateTime.Now.AddDays(-1);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId)).Returns(new Account("Test", "User"));
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId, yesterday, 100000)
            };
            //Act
            var result = mapper.Map(data);
            //Assert
            Assert.Empty(result.ValidRecords);
            Assert.Single(result.InvalidRecords);
        }

        [Fact]
        public void Map_GivenValidData_ShouldAccept()
        {
            //Arrange
            var testAccountId = 1;
            var yesterday = DateTime.Now.AddDays(-1);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId)).Returns(new Account("Test", "User"));
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId, yesterday, 1234)
            };
            //Act
            var result = mapper.Map(data);
            //Assert
            Assert.Single(result.ValidRecords);
            Assert.Empty(result.InvalidRecords);
        }

        [Fact]
        public void Map_GivenMultipleValidData_ShouldAcceptAll()
        {
            //Arrange
            var testAccountId1 = 1;
            var testAccountId2 = 2;
            var yesterday = DateTime.Now.AddDays(-1);
            var twoDaysAgo = DateTime.Now.AddDays(-2);
            var mockAccountRepository = new Mock<IAccountRepository>();
            mockAccountRepository.Setup(r => r.GetById(testAccountId1)).Returns(new Account("Test", "User"));
            mockAccountRepository.Setup(r => r.GetById(testAccountId2)).Returns(new Account("Test2", "User2"));
            var mockMeterReadingRepository = new Mock<IMeterReadingRepository>();
            var mapper = new MeterReadingMapper(mockAccountRepository.Object, mockMeterReadingRepository.Object);
            var data = new List<MeterReadingCsvRecord>()
            {
                new(testAccountId1, yesterday, 1234),
                new(testAccountId2, twoDaysAgo, 5678)
            };
            //Act
            var result = mapper.Map(data);
            //Assert
            Assert.Equal(2, result.ValidRecords.Count);
            Assert.Empty(result.InvalidRecords);
        }

    }
}
