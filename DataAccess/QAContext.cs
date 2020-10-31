using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class QAContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=QuestionsAnswers;Trusted_Connection=true");
        }
    }
}
