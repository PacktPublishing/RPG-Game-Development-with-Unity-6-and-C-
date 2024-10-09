using System;

namespace Fight
{
    public interface IHaveHealth
    {
        public int MaxHealth { get; }
        public int Health { get; }
        public event Action HealthChanged;
        public bool AddHealth(int healthPoint);
    }
}