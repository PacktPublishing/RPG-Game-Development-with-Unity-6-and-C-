using System;
using Fight;
using InventorySystem;
using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class LifeController : MonoBehaviour, IReceiveDamage, IHaveHealth
    {
        public int MaxHealth { get; set; }
        [SerializeField] private DamageTaker[] damageTakers;
        [SerializeField] private int health = 100;
        public int Health 
        {
            get => health;
            set 
            {
                health = value;
            }
        }

        private IBlocker _blocker;

        public event Action HealthChanged;

        public void ReceiveDamage(int damage)
        {
            if(_blocker.IsBlocking)
            {
                Debug.Log("Damage Blocked");
                return;
            }

            health -= damage;
            HealthChanged?.Invoke();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public bool AddHealth(int health)
        {
            if(Health == MaxHealth)
            {
                return false;
            }

            Health += health;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            HealthChanged?.Invoke();
            return true;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            MaxHealth = health;
            foreach (var taker in damageTakers)
            {
                taker.RegisterDamageReceiver(this);
            }

            _blocker = GetComponent<IBlocker>();
        }
    }
}