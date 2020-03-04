using System;
using System.Collections.Generic;
using System.Text;

namespace entities._base
{
    public abstract class EntityBase<T> : IEntity
    {
        public T Id { get; set; }
        public object GetKey()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is IEntity oEntity)
            {
                return this.GetType().Equals(obj.GetType()) && this.GetKey().Equals(oEntity.GetKey());
            }

            return result;
        }
    }
}
