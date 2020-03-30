using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model
{
    public abstract class EntityBase
    {
        public virtual Guid ID { get; set; } = Guid.NewGuid();
    }
}
