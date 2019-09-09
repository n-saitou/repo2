using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.ValueObjects
{
    public sealed class AreaId : ValueObject<AreaId>
    {
        public AreaId(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public string DisplayVaule
        {
            get
            {
                return Value.ToString().PadLeft(4, '0');
            }
        }


        protected override bool EqulsCore(AreaId other)
        {
            return this.Value == other.Value;
        }
    }
}
