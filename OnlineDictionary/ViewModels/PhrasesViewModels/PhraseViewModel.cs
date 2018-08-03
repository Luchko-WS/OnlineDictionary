using System;

namespace OnlineDictionary.ViewModels
{
    public class PhraseViewModel
    {
        public Guid? Id { get; set; }

        public string Text { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }
    }
}