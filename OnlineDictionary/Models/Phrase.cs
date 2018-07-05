namespace OnlineDictionary.Models
{
    public class Phrase : BaseEntity
    {
        public string Text { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }
    }
}