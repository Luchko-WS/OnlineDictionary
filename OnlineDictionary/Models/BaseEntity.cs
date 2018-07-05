using System;

namespace OnlineDictionary.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public string OwnerId { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}