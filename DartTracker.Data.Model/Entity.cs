using System;

namespace DartTracker.Data.Model
{
    public class Entity<T>
    {
        public T Value { get; set; }
        public Guid ID { get; set; }
        public DateTime CreatedUTC { get; set; }
        public DateTime ModifiedUTC { get; set; }
    }
}
