using System;
using System.Collections.Generic;

namespace OnlineDictionary.Models
{
    public class Dictionary : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public ICollection<PhrasesPair> PhrasesPairs { get; set; }
    }
}