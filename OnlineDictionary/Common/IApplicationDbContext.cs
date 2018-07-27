using OnlineDictionary.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDictionary
{
    public interface IApplicationDbContext
    {
        IQueryable<Phrase> Phrases { get; }

        IQueryable<PhrasesPair> PhrasesPairs { get; }

        IQueryable<Dictionary> Dictionaries { get; }

        #region Dictionaries

        void CreateDictionary(Dictionary dictionary);

        void RemoveDictionary(Dictionary dictionary);
        
        #endregion

        Task SaveDbChangesAsync();
    }
}