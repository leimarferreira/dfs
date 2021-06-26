using Microsoft.EntityFrameworkCore;
using core.Persistence.Contexts;

namespace core_test.Helpers
{
    public class DbContextManager
    {
        public static AppDbContext DbContext { get; private set; }

        public static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("testdb")
                .Options;
            DbContext = new AppDbContext(options);
            return DbContext;
        }
    }
}