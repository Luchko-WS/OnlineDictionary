using System;
using System.Collections.Generic;

namespace OnlineDictionary.Models
{
    public class Dictionary : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FromLanguage { get; set; }

        public string ToLanguage { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public ICollection<PhrasesPair> Phrases { get; set; }
    }
}