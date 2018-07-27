using System;

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
    }
}