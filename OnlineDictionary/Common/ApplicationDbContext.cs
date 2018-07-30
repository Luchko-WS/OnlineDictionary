using Microsoft.AspNet.Identity.EntityFramework;
using OnlineDictionary.Models;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDictionary
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Database.Log = (log) => Debug.WriteLine(log);
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

        #region interface implementation
        IQueryable<Phrase> IApplicationDbContext.Phrases => this.Phrases;

        IQueryable<PhrasesPair> IApplicationDbContext.PhrasesPairs => this.PhrasesPairs;

        IQueryable<Dictionary> IApplicationDbContext.Dictionaries => this.Dictionaries;


        #region Dictionaries

        public Dictionary CreateDictionary(Dictionary dictionary)
        {
            return this.Dictionaries.Add(dictionary);
        }

        public Dictionary RemoveDictionary(Dictionary dictionary)
        {
            return this.Dictionaries.Remove(dictionary);
        }

        #endregion

        #region PhrasesPairs

        public PhrasesPair CreatePhrasePair(PhrasesPair phrasesPair)
        {
            return this.PhrasesPairs.Add(phrasesPair);
        }

        #endregion

        #region Phrases

        public Phrase CreatePhrase(Phrase phrase)
        {
            return this.Phrases.Add(phrase);
        }

        #endregion

        public async Task SaveDbChangesAsync()
        {
            await this.SaveChangesAsync();
        }

        #endregion
    }
}