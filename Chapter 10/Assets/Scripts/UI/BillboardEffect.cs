using UnityEngine;

public class BillboardEffect : MonoBehaviour
{
    private Transform _targetCameraTransform;

    void Start()
    {
        _targetCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(_targetCameraTransform);
    }
}
