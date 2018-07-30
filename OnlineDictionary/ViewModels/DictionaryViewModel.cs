using OnlineDictionary.Models;
using System;
using System.Collections.Generic;

namespace OnlineDictionary.ViewModels
{
    public class DictionaryViewModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public bool? IsMyDictionary { get; set; }

        public IEnumerable<PhrasesPair> PhrasesPairs { get; set; }
    }
}