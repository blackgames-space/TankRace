using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWheel : MonoBehaviour
{
    public bool IsTouchingGround => _IsTouchingGrass;

    [SerializeField] private bool _Debugable;
    [SerializeField] private Rigidbody _myRigidbody;
    //[SerializeField] private Transform _collider;

    private bool _IsTouchingGrass;
    internal void ApplySuspension(float force, float distance, float wheelRadius)
    {
        if (Physics.Raycast(this.transform.position, -this.transform.up, out RaycastHit hit, distance))
        {
            //_collider.transform.position = hit.point + (this.transform.position - hit.point).normalized * wheelRadius;
            float magnitude = (this.transform.position - hit.point).magnitude;
            float forceMod = (distance - magnitude) / distance;
            if (_Debugable)
            {
                Debug.Log("magnitude: " + magnitude);
                Debug.Log("forceRaw: " + forceMod);
            }
            _IsTouchingGrass = true;
            _myRigidbody.AddForceAtPosition(this.transform.up * force * forceMod, this.transform.position);
        }
        else
        {
            _IsTouchingGrass = false;
        }
    }
}
