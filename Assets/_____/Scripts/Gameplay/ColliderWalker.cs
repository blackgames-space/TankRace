using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderWalker : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private Transform _rotator;
    [SerializeField] private Transform _hull;
    [SerializeField] private Transform _main;
    [SerializeField] private LayerMask _layerMask;

    private void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hit, _distance, _layerMask))
        {
            _rotator.forward = Vector3.Lerp(_rotator.forward, Vector3.Cross(hit.normal, -_main.right), 0.2f);
            _hull.position = Vector3.Lerp(_hull.position, new Vector3(_hull.position.x, hit.point.y, _hull.position.z) , 0.5f);
        }
        else
        {
            _rotator.up = Vector3.Lerp(_rotator.up, Vector3.Cross(Vector3.up, -_main.right), 0.1f);
            _hull.position += Vector3.down * 10f * Time.fixedDeltaTime;
        }
        _hull.Rotate(Vector3.right, _rotator.localEulerAngles.x - _hull.localEulerAngles.x);
    }
}
