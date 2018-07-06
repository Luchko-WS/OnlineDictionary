using OnlineDictionary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDictionary
{
    public interface IApplicationDbContext
    {
        IQueryable<Phrase> Phrases { get; }

        IQueryable<PhrasesPair> PhrasesPairs { get; }

        IQueryable<Dictionary> Dictionaries { get; }

        void CreateDictionary(Dictionary newDictionary);

        Task SaveDbChangesAsync();
    }
}