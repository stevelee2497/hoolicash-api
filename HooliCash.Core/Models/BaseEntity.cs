using System;

namespace HooliCash.Core.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        protected BaseEntity()
        {
            Id = new Guid();
        }
    }
}
