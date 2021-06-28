using System;

namespace HooliCash.DTOs.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string TransactionType { get; set; }

        public int TransactionCount { get; set; }

        public string IconUrl { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
