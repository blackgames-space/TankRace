using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCamera : MonoBehaviour
{
    public VirtualCameraController Camera => _camera;
    [SerializeField] private VirtualCameraController _camera;
    public void Rotate(Vector3 rotation)
    {
        
        this.transform.Rotate(new Vector3(rotation.x,0,0), Space.Self);
        this.transform.Rotate(new Vector3(0,rotation.y,0), Space.World);
    }
}
