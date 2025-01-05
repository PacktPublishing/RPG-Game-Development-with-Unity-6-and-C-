using UnityEngine;
using UnityEngine.InputSystem;

namespace RPGSample.Managers
{
    public class PlayerMovementManager : MonoBehaviour
    {            
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Transform rootTransform;
        [SerializeField] private Animator animator;
        [SerializeField] private float speedModifier;

        private int _blendXHash;
        private int _blendYHash;
        private Vector3 _cachedPoint;

        private int _blendRightRotationHash;
        private int _blendLeftRotationHash;

        private int _isMoving;


        private void Start()
        {
            _blendXHash = Animator.StringToHash("BlendX");
            _blendYHash = Animator.StringToHash("BlendY");
            _isMoving = Animator.StringToHash("IsMoving");
        }

        private void Update()
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit info,100,LayerMask.GetMask("Ground")))
            {
                _cachedPoint = info.point;
                var directionToPoint = info.point - transform.position;
                var forwardDirection = transform.forward;
                var dotProduct = Vector3.Dot(forwardDirection, directionToPoint);
                Debug.Log($"Dot Product {dotProduct}");

               rb.MoveRotation(Quaternion.LookRotation(directionToPoint));
            }
        }

        public void OnPlayerInput(Vector2 inputAxis)
        {
            animator.SetBool(_isMoving, inputAxis.sqrMagnitude > 0);
            animator.SetFloat(_blendYHash, inputAxis.y);
            animator.SetFloat(_blendXHash, inputAxis.x);

            rb.MovePosition(new Vector3(rootTransform.position.x + (inputAxis.x * speedModifier * Time.deltaTime), rootTransform.position.y, rootTransform.position.z + (inputAxis.y * speedModifier * Time.deltaTime * transform.forward.z)));   
        }

        public void OnPlayerInputJump()
        {

        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawCube(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()).origin, Vector3.one);
            Gizmos.DrawSphere(_cachedPoint, 0.5f);
        }
    }
}
