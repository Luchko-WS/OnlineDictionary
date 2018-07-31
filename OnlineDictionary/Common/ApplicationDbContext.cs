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

        public async Task<Dictionary> RemoveDictionary(Dictionary dictionary)
        {
            var res = this.Dictionaries.Remove(dictionary);
            var dictionaryPairs = await this.PhrasesPairs.Where(p => p.DictionaryId == res.Id).ToListAsync();
            foreach(var pair in dictionaryPairs)
            {
                await RemovePhrasesPair(pair);
            }
            return res;
        }

        #endregion

        #region PhrasesPairs

        public PhrasesPair CreatePhrasesPair(PhrasesPair phrasesPair)
        {
            return this.PhrasesPairs.Add(phrasesPair);
        }

        public Task<PhrasesPair> UpdatePhrasePair(PhrasesPair phrasesPair)
        {
            throw new NotImplementedException();
        }

        public async Task<PhrasesPair> RemovePhrasesPair(PhrasesPair phrasesPair)
        {
            var res = this.PhrasesPairs.Remove(phrasesPair);
            if (! await this.PhrasesPairs.AnyAsync(p => 
                            p.Id != res.Id &&
                            (p.FirstPhraseId == res.FirstPhraseId || 
                            p.SecondPhraseId == res.FirstPhraseId)))
            {
                var firstPhrase = await this.Phrases.FirstOrDefaultAsync(p => p.Id == res.FirstPhraseId);
                this.Phrases.Remove(firstPhrase);
            }
            if (! await this.PhrasesPairs.AnyAsync(p =>
                            p.Id != res.Id &&
                            (p.FirstPhraseId == res.SecondPhraseId || 
                            p.SecondPhraseId == res.SecondPhraseId)))
            {
                var secondPhrase = await this.Phrases.FirstOrDefaultAsync(p => p.Id == res.SecondPhraseId);
                this.Phrases.Remove(secondPhrase);
            }
            return res;
        }

        #endregion

        #region Phrases

        public Phrase CreatePhrase(Phrase phrase)
        {
            return this.Phrases.Add(phrase);
        }

        public Phrase RemovePhrase(Phrase phrase)
        {
            return this.Phrases.Remove(phrase);
        }

        #endregion

        public async Task SaveDbChangesAsync()
        {
            await this.SaveChangesAsync();
        }

        #endregion
    }
}