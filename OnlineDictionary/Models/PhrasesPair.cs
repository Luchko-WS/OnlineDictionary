using System;

namespace OnlineDictionary.Models
{
    public class PhrasesPair : BaseEntity
    {
        public Guid FirstId { get; set; }

        public Phrase First { get; set; }

        public Guid SecondId { get; set; }

        public Phrase Second { get; set; }

        public Guid DictionaryId { get; set; }

        public UserDictionary Dictionary { get; set; }

        public bool IsConfirmed { get; set; }
    }
}