using LibraryData.Functions.Librarians;
using LibraryData.Interface;
using LibraryData.Models;
using LibraryData.Services;
using LibraryData_Test.TestHelpers;
using Microsoft.Extensions.Logging;
using EntityType = LibraryData.Models.EbookRent;

namespace LibraryData_Test.Services
{
    public class StudentServiceTests
    {
        private readonly Mock<LibrarydbContext> _librarydbContext;
        private readonly StudentService _studentService;
        private readonly Mock<ILogger<StudentService>> _logger;
        private List<EntityType> rentedBooks = new();
        public StudentServiceTests()
        {
            _librarydbContext = new Mock<LibrarydbContext>();
            _logger = new Mock<ILogger<StudentService>>();
            _studentService = new StudentService(_librarydbContext.Object, _logger.Object);
        }

        [Fact]
        public async Task GetRentedBooks_By_Username()
        {
            //Arrange
            string username = "Sandy";
            var mockEbookRentDbSet = DbHelper.GetQueryableAsyncMockDbSet<EntityType>(rentedBooks);
            _librarydbContext.Setup(db => db.EbookRents).Returns(mockEbookRentDbSet);

            //Act
            var result = await _studentService.RentedBooks<EntityType>(username);

            //Assert
            result.Should().BeEquivalentTo(new List<EntityType>());
        }
    }
}
