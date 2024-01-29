using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public Rect Bounds = new Rect(0, 0, 10, 10);

    void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");        
        Vector3 position = transform.position + new Vector3(horizontalInput, 0, verticalInput) * MoveSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, Bounds.xMin, Bounds.xMax);
        position.z = Mathf.Clamp(position.z, Bounds.yMin, Bounds.yMax);
        transform.position = position;        
    }
}
