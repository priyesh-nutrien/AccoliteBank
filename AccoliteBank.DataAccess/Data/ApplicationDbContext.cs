using Microsoft.EntityFrameworkCore;
using AccoliteBank.Models;

namespace AccoliteBank.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType>()
                .HasData(
                    new AccountType
                    {
                        Id = 1,
                        Type = "Checking"
                    },
                    new AccountType
                    {
                        Id = 2,
                        Type = "Saving"
                    }
                );

            modelBuilder.Entity<User>()
              .HasData(
                  new User
                  {
                      Id = 1,
                      UserName = "User1",
                      CreatedDate = DateTime.Now
                  },
                  new User
                  {
                      Id = 2,
                      UserName = "User2",
                      CreatedDate = DateTime.Now
                  },
                  new User
                  {
                      Id = 3,
                      UserName = "User3",
                      CreatedDate = DateTime.Now
                  },
                  new User
                  {
                      Id = 4,
                      UserName = "User4",
                      CreatedDate = DateTime.Now
                  }
              );

            modelBuilder.Entity<UserAccount>(
                  entity =>
                  {
                      entity.HasOne(d => d.User)
                          .WithMany(p => p.UserAccounts)
                          .HasForeignKey("UserId");

                      entity.HasOne(d => d.AccountType)
                         .WithMany(p => p.UserAccounts)
                         .HasForeignKey("AccountTypeId");
                  });

            modelBuilder.Entity<UserAccount>()
               .HasData(
                    new UserAccount
                    {
                        Id = 1,
                        AccountTypeId = 1,
                        UserId = 1,
                        Name = "Personal 1",
                        CreatedDate = DateTime.Now,
                        Balance = 10000.67m
                    },
                    new UserAccount
                    {
                        Id = 2,
                        AccountTypeId = 1,
                        UserId = 1,
                        Name = "Personal 2",
                        CreatedDate = DateTime.Now,
                        Balance = 122.44m
                    },
                    new UserAccount
                    {
                        Id = 3,
                        AccountTypeId = 1,
                        UserId = 1,
                        Name = "Personal 3",
                        CreatedDate = DateTime.Now,
                        Balance = 500.99m
                    },
                    new UserAccount
                    {
                        Id = 4,
                        AccountTypeId = 2,
                        UserId = 1,
                        Name = "Saving 2",
                        CreatedDate = DateTime.Now,
                        Balance = 600.28m
                    },
                    new UserAccount
                    {
                        Id = 5,
                        AccountTypeId = 1,
                        UserId = 2,
                        Name = "Personal 1",
                        CreatedDate = DateTime.Now,
                        Balance = 15000.62m
                    },
                    new UserAccount
                    {
                        Id = 6,
                        AccountTypeId = 1,
                        UserId = 2,
                        Name = "Personal 2",
                        CreatedDate = DateTime.Now,
                        Balance = 1500.42m
                    },
                    new UserAccount
                    {
                        Id = 7,
                        AccountTypeId = 2,
                        UserId = 2,
                        Name = "Saving 1",
                        CreatedDate = DateTime.Now,
                        Balance = 5500.32m
                    },
                    new UserAccount
                    {
                        Id = 8,
                        AccountTypeId = 2,
                        UserId = 2,
                        Name = "Saving 2",
                        CreatedDate = DateTime.Now,
                        Balance = 5600.23m
                    }
               );
        }

        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
