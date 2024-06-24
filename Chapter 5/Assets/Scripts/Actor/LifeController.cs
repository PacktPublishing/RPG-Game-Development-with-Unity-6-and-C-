using Fight;
using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class LifeController : MonoBehaviour, IReceiveDamage, IHaveHealth
    {
        [SerializeField] private DamageTaker[] damageTakers;
        private IBlocker _blocker;

        public int Health { get; private set; }
        public void ReceiveDamage(int damage)
        {
            if(_blocker.IsBlocking)
            {
                Debug.Log("Damage Blocked");
                return;
            }

            Health -= damage;
            if (Health < 0)
            {
                Destroy(gameObject);
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Health = 100;
            foreach (var taker in damageTakers)
            {
                taker.RegisterDamageReceiver(this);
            }

            _blocker = GetComponent<IBlocker>();
        }
    }
}