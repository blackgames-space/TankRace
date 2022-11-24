using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSuspension : MonoBehaviour
{
    [SerializeField] private bool _IsDebuggable;
    [SerializeField] private MyWheel[] _wheels;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _force;
    [SerializeField] private float _distance;
    [SerializeField] private float _wheelRadius;

    private float _rotationSpeed;

    public void FixedUpdate()
    {
        foreach (var wheel in _wheels)
        {
            wheel.ApplySuspension(_force, _distance, _wheelRadius);
        }

        bool isTouchingGround = false;
        foreach (var wheel in _wheels)
        {
            if (wheel.IsTouchingGround) isTouchingGround = true;
        }
        if (!isTouchingGround)
        {
            _rotationSpeed++;
            Vector3 eulers = this.transform.TransformVector(_rigidbody.transform.localEulerAngles);
            Vector3 eulersMod = new Vector3(
                (eulers.x > 180) ? 180 - eulers.x : eulers.x,
                (eulers.y > 180) ? 180 - eulers.y : eulers.y,
                (eulers.z > 180) ? 180 - eulers.z : eulers.z);
            _rigidbody.angularVelocity = -eulersMod * Mathf.Deg2Rad * _rotationSpeed * 0.8f;
            if (_IsDebuggable)
            {
                Debug.Log("eulersRaw = " + eulers);
                Debug.Log("eulersMod = " + eulersMod);
                Debug.Log("_rigidbody.angularVelocity = " + _rigidbody.angularVelocity);
            }
            _rigidbody.velocity += Vector3.down * 100 * Time.fixedDeltaTime;
        }
        else
        {
            _rotationSpeed = 2f;
        }


    }
}
