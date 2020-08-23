using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cod pt ca jucatorul sa fie urmarit de catre camera

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime); // smoothness
    }
}
