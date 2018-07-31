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

        Dictionary CreateDictionary(Dictionary dictionary);

        Task<Dictionary> RemoveDictionary(Dictionary dictionary);

        #endregion

        #region PhrasesPairs

        PhrasesPair CreatePhrasesPair(PhrasesPair phrasesPair);

        Task<PhrasesPair> RemovePhrasesPair(PhrasesPair phrasesPair);

        #endregion

        #region  Phrases

        Phrase CreatePhrase(Phrase phrase);

        Phrase RemovePhrase(Phrase phrase);

        #endregion

        Task SaveDbChangesAsync();
    }
}