//using Microsoft.EntityFrameworkCore;
//using System.Reflection.Emit;

//namespace NationalBank;

//public class BankDbContext : DbContext
//{
//    public DbSet<Manager> Managers => Set<Manager>();
//    public DbSet<Branch> Branches => Set<Branch>();
//    public DbSet<Customer> Customers => Set<Customer>();
//    public DbSet<Account> Accounts => Set<Account>();
//    public DbSet<CustomerAccount> CustomerAccounts => Set<CustomerAccount>();
//    public DbSet<Transaction> Transactions => Set<Transaction>();

//    protected override void OnConfiguring(
//        DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer(
//            "Server=.;Database=NationalBankDb;Trusted_Connection=True;TrustServerCertificate=True");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        // ===================== MANAGER =====================

//        modelBuilder.Entity<Manager>(entity =>
//        {
//            entity.HasKey(m => m.Id);

//            entity.Property(m => m.FullName)
//                .HasMaxLength(150)
//                .IsRequired();

//            entity.Property(m => m.Email)
//                .HasMaxLength(200)
//                .IsRequired();

//            entity.Property(m => m.PhoneNumber)
//                .HasMaxLength(20)
//                .IsRequired();

//            entity.Property(m => m.HireDate)
//                .IsRequired();

//            entity.HasIndex(m => m.Email)
//                .IsUnique();
//        });

//        // ===================== BRANCH =====================

//        modelBuilder.Entity<Branch>(entity =>
//        {
//            entity.HasKey(b => b.Code);

//            entity.Property(b => b.Code)
//                .HasMaxLength(10)
//                .IsRequired();

//            entity.Property(b => b.Name)
//                .HasMaxLength(100)
//                .IsRequired();

//            entity.Property(b => b.Address)
//                .HasMaxLength(250)
//                .IsRequired();

//            entity.Property(b => b.PhoneNumber)
//                .HasMaxLength(20)
//                .IsRequired();

//            entity.HasOne(b => b.Manager)
//                .WithOne(m => m.Branch)
//                .HasForeignKey<Branch>(b => b.ManagerId)
//                .IsRequired()
//                .OnDelete(DeleteBehavior.Restrict);
//        });

//        // ===================== CUSTOMER =====================

//        modelBuilder.Entity<Customer>(entity =>
//        {
//            entity.HasKey(c => c.Id);

//            entity.Property(c => c.FullName)
//                .HasMaxLength(150)
//                .IsRequired();

//            entity.Property(c => c.NationalId)
//                .HasMaxLength(20)
//                .IsRequired();

//            entity.Property(c => c.Email)
//                .HasMaxLength(200)
//                .IsRequired();

//            entity.Property(c => c.PhoneNumber)
//                .HasMaxLength(20)
//                .IsRequired();

//            entity.Property(c => c.Address)
//                .HasMaxLength(250)
//                .IsRequired();

//            entity.Property(c => c.DateOfBirth)
//                .IsRequired();

//            entity.Property(c => c.CustomerType)
//                .HasConversion<string>()
//                .HasMaxLength(20);

//            entity.HasIndex(c => c.NationalId)
//                .IsUnique();
//        });

//        // ===================== ACCOUNT =====================

//        modelBuilder.Entity<Account>(entity =>
//        {
//            entity.HasKey(a => a.AccountNumber);

//            entity.Property(a => a.AccountNumber)
//                .HasMaxLength(20)
//                .IsRequired();

//            entity.Property(a => a.CurrentBalance)
//                .HasPrecision(18, 2);

//            entity.Property(a => a.AccountType)
//                .HasConversion<string>()
//                .HasMaxLength(20);

//            entity.Property(a => a.OpeningDate)
//                .IsRequired();

//            entity.HasOne(a => a.Branch)
//                .WithMany(b => b.Accounts)
//                .HasForeignKey(a => a.BranchCode)
//                .IsRequired()
//                .OnDelete(DeleteBehavior.Restrict);
//        });

//        // ===================== CUSTOMER ACCOUNT =====================

//        modelBuilder.Entity<CustomerAccount>(entity =>
//        {
//            entity.HasKey(ca => new
//            {
//                ca.CustomerId,
//                ca.AccountNumber
//            });

//            entity.Property(ca => ca.OwnershipType)
//                .HasConversion<string>()
//                .HasMaxLength(20);

//            entity.Property(ca => ca.AccountStatus)
//                .HasConversion<string>()
//                .HasMaxLength(20);

//            entity.Property(ca => ca.OwnershipStartDate)
//                .IsRequired();

//            entity.HasOne(ca => ca.Customer)
//                .WithMany(c => c.CustomerAccounts)
//                .HasForeignKey(ca => ca.CustomerId)
//                .OnDelete(DeleteBehavior.Cascade);

//            entity.HasOne(ca => ca.Account)
//                .WithMany(a => a.CustomerAccounts)
//                .HasForeignKey(ca => ca.AccountNumber)
//                .OnDelete(DeleteBehavior.Cascade);
//        });

//        // ===================== TRANSACTION =====================

//        modelBuilder.Entity<Transaction>(entity =>
//        {
//            entity.HasKey(t => t.TransactionNumber);

//            entity.Property(t => t.Amount)
//                .HasPrecision(18, 2);

//            entity.Property(t => t.TransactionDate)
//                .IsRequired();

//            entity.Property(t => t.TransactionType)
//                .HasConversion<string>()
//                .HasMaxLength(20);

//            entity.Property(t => t.Note)
//                .HasMaxLength(250);

//            entity.HasOne(t => t.Account)
//                .WithMany(a => a.Transactions)
//                .HasForeignKey(t => t.AccountNumber)
//                .IsRequired()
//                .OnDelete(DeleteBehavior.Restrict);
//        });

//        // ===================== SEED DATA =====================

//        // Managers
//        modelBuilder.Entity<Manager>().HasData(
//            new Manager
//            {
//                Id = 1,
//                FullName = "Mostafa Adel",
//                Email = "mostafa.adel@nationalbank.com",
//                PhoneNumber = "01033333331",
//                HireDate = new DateTime(2016, 5, 10)
//            },
//            new Manager
//            {
//                Id = 2,
//                FullName = "Menna Samir",
//                Email = "menna.samir@nationalbank.com",
//                PhoneNumber = "01033333332",
//                HireDate = new DateTime(2019, 8, 20)
//            }
//        );

//        // Branches
//        modelBuilder.Entity<Branch>().HasData(
//            new Branch
//            {
//                Code = "GIZ-01",
//                Name = "Giza Main Branch",
//                Address = "12 Faisal Street, Giza",
//                PhoneNumber = "0233333331",
//                ManagerId = 1
//            },
//            new Branch
//            {
//                Code = "MAN-01",
//                Name = "Mansoura Branch",
//                Address = "8 El Gomhoria Street, Mansoura",
//                PhoneNumber = "0503333332",
//                ManagerId = 2
//            }
//        );

//        // Customers
//        modelBuilder.Entity<Customer>().HasData(
//            new Customer
//            {
//                Id = 1,
//                FullName = "Youssef Ibrahim",
//                NationalId = "29505011234561",
//                DateOfBirth = new DateTime(1995, 5, 1),
//                Email = "youssef.ibrahim@mail.com",
//                PhoneNumber = "01044444441",
//                Address = "Giza, Egypt",
//                CustomerType = CustomerType.Individual
//            },
//            new Customer
//            {
//                Id = 2,
//                FullName = "Delta Supplies",
//                NationalId = "30008011234562",
//                DateOfBirth = new DateTime(2000, 8, 1),
//                Email = "contact@deltasupplies.com",
//                PhoneNumber = "01044444442",
//                Address = "Mansoura, Egypt",
//                CustomerType = CustomerType.Business
//            }
//        );

//        // Accounts
//        modelBuilder.Entity<Account>().HasData(
//            new Account
//            {
//                AccountNumber = "4100-SAV",
//                CurrentBalance = 25750m,
//                AccountType = AccountType.Savings,
//                OpeningDate = new DateTime(2023, 2, 15),
//                BranchCode = "GIZ-01"
//            },
//            new Account
//            {
//                AccountNumber = "5200-BUS",
//                CurrentBalance = 185000m,
//                AccountType = AccountType.Business,
//                OpeningDate = new DateTime(2023, 7, 10),
//                BranchCode = "MAN-01"
//            }
//        );

//        // Customer Accounts
//        modelBuilder.Entity<CustomerAccount>().HasData(
//            new CustomerAccount
//            {
//                CustomerId = 1,
//                AccountNumber = "4100-SAV",
//                OwnershipType = OwnershipType.Primary,
//                OwnershipStartDate = new DateTime(2023, 2, 15),
//                AccountStatus = AccountStatus.Active
//            },
//            new CustomerAccount
//            {
//                CustomerId = 2,
//                AccountNumber = "5200-BUS",
//                OwnershipType = OwnershipType.Primary,
//                OwnershipStartDate = new DateTime(2023, 7, 10),
//                AccountStatus = AccountStatus.Active
//            }
//        );
//    }
//} 