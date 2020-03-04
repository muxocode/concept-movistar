using System;

namespace entities
{
    public class Prop: entities._base.EntityBase<Guid>
    {
        public string Name { get; set; }
        public int DefaultValue { get; set; }
    }
}