using System;

namespace OnlineDictionary.Models
{
    public class PhrasesPair : BaseEntity
    {
        public Guid FirstPhraseId { get; set; }

        public Phrase FirstPhrase { get; set; }

        public Guid SecondPhraseId { get; set; }

        public Phrase SecondPhrase { get; set; }

        public Guid DictionaryId { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsConfirmed { get; set; }
    }
}