    using Microsoft.EntityFrameworkCore;
    using Project.Models;

    namespace Project.Data
    {
        public class CandidateContext : DbContext
        {
            public CandidateContext(DbContextOptions<CandidateContext> options)
                : base(options)
            {
            }

            public DbSet<CandidateRegistration> Candidates { get; set; }
            public DbSet<AdminLogin> AdminLogins { get; set; }
            public DbSet<CareerRequest> Careers { get; set; }
            public DbSet<Contact> Contacts { get; set; }
             public DbSet<Quote> Quotes { get; set; }
            public DbSet<Category> Categories { get; set; }
          public DbSet<Product> Products { get; set; }


    }
}
