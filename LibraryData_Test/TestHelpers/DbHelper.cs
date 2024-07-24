using LibraryData.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LibraryData_Test.TestHelpers
{
    public class DbHelper
    {
        public static Mock<LibrarydbContext> CreateMockDbContext<T>(List<T>? input = null) where T : class
        {
            var options = new DbContextOptionsBuilder<LibrarydbContext>()
                .UseInMemoryDatabase(databaseName: "librarydb")
                .Options;

            var mockedDbContext = new Mock<LibrarydbContext>(options);

            var mockDatabaseFacade = new Mock<DatabaseFacade>(mockedDbContext.Object);
            mockedDbContext.SetupGet(x => x.Database).Returns(mockDatabaseFacade.Object);

            var dbSet = GetQueryableAsyncMockDbSet(input ?? new List<T>());
            mockedDbContext.Setup(r => r.Set<T>()).Returns(dbSet);

            mockedDbContext.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => dbSet.Count());

            return mockedDbContext;
        }

        public static DbSet<T> GetQueryableAsyncMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            mockSet.Setup(m => m.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .Returns<T, CancellationToken>((entity, token) =>
                {
                    sourceList.Add(entity);
                    return new ValueTask<EntityEntry<T>>(Task.FromResult((EntityEntry<T>)new Mock<EntityEntry<T>>().Object));
                });

            return mockSet.Object;
        }
    }
}
