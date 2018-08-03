using System;

namespace OnlineDictionary.ViewModels
{
    public class PhrasesPairViewModel
    {
        public Guid? Id { get; set; }

        public Guid? FirstPhraseId { get; set; }

        public PhraseViewModel FirstPhrase { get; set; }

        public Guid? SecondPhraseId { get; set; }

        public PhraseViewModel SecondPhrase { get; set; }

        public Guid DictionaryId { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsConfirmed { get; set; }

        public string OwnerId { get; set; }
    }
}