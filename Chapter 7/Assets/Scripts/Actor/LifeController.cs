using Fight;
using UnityEngine;

namespace BasicFiniteStateMachine
{
    public class LifeController : MonoBehaviour, IReceiveDamage, IHaveHealth
    {
        [SerializeField] private DamageTaker[] damageTakers;
        [SerializeField] private int health = 100;
        public int Health => health;
        private IBlocker _blocker;


        public void ReceiveDamage(int damage)
        {
            if(_blocker.IsBlocking)
            {
                Debug.Log("Damage Blocked");
                return;
            }

            health -= damage;
            if (health < 0)
            {
                Destroy(gameObject);
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (var taker in damageTakers)
            {
                taker.RegisterDamageReceiver(this);
            }

            _blocker = GetComponent<IBlocker>();
        }
    }
}