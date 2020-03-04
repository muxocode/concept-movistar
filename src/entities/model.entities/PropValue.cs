using System;

namespace entities
{
    public class PropValue: entities._base.EntityBase<Guid>
    {
        public object Value { get; set; }
        public Prop Property { get; set; }
    }
}