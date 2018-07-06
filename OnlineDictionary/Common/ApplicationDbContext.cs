using Microsoft.AspNet.Identity.EntityFramework;
using OnlineDictionary.Models;
using System.Data.Entity;

namespace OnlineDictionary
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhrasesPair>()
                .HasRequired(c => c.FirstPhrase)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhrasesPair>()
                .HasRequired(c => c.SecondPhrase)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Phrase> Phrases { get; set; }

        public DbSet<PhrasesPair> PhrasesPairs { get; set; }

        public DbSet<Dictionary> Dictionaries { get; set; }
    }
}