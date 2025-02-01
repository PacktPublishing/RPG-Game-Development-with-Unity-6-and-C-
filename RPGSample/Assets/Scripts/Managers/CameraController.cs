using UnityEngine;

namespace RPGSample.Managers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform target;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.SetPositionAndRotation(target.position + offset, Quaternion.LookRotation(target.position - transform.position));
        }
    }
}
