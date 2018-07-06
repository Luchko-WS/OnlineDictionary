using System;

namespace OnlineDictionary.ViewModels
{
    public class DictionaryViewModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string FromLanguage { get; set; }

        public string ToLanguage { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastChangeDate { get; set; }
    }
}