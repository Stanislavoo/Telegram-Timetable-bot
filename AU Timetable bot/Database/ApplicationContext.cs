using Microsoft.EntityFrameworkCore;
using Timetablebot.Exceptions;

namespace Timetablebot.Database
{
    public class ApplicationContext : DbContext
    {
        private static bool _isNotEmpty;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Button> Buttons { get; set; } = null!;
        public DbSet<ButtonsTranslation> ButtonsTranslation { get; set; } = null!;
        public DbSet<ButtonsOrder> ButtonsOrder { get; set; } = null!;
        public DbSet<Methodist> MethodistsData { get; set; } = null!;
        public DbSet<Headman> HeadmansData { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
            if(_isNotEmpty == false)
            {
                if (Buttons.FromSqlRaw($"SELECT * FROM Buttons WHERE id = 1").FirstOrDefault() == null)
                    throw new EmptyDatabaseTableException("Table \"Buttons\" is empty. Fill it for correсt work.");

                if (ButtonsTranslation.FromSqlRaw($"SELECT * FROM ButtonsTranslation WHERE id = 1").FirstOrDefault() == null)
                    throw new EmptyDatabaseTableException("Table \"ButtonsTranslation\" is empty. Fill it for correсt work.");

                if (ButtonsOrder.FromSqlRaw($"SELECT * FROM ButtonsOrder WHERE id = 1").FirstOrDefault() == null)
                    throw new EmptyDatabaseTableException("Table \"ButtonsOrder\" is empty. Fill it for correсt work.");

                if (MethodistsData.FromSqlRaw($"SELECT * FROM MethodistsData WHERE id = 1").FirstOrDefault() == null)
                    throw new EmptyDatabaseTableException("Table \"MethodistsData\" is empty. Fill it for correсt work.");     
                
                if (HeadmansData.FromSqlRaw($"SELECT * FROM HeadmansData  WHERE id = 1").FirstOrDefault() == null)
                    throw new EmptyDatabaseTableException("Table \"HeadmansData \" is empty. Fill it for correсt work.");
                _isNotEmpty = true;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"CONNECTION STRING");
        }
    }
}
