using System;

namespace Fight
{
    public interface IHaveExhaustion
    {
        public float MaxPower { get; }
        public float Power { get; }
        public bool IsExhausted { get; }
        public event Action OnPowerChanged;
    }
}