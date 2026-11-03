using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }

        public void MarkUpdated()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}
