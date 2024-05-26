using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float mouseSensitivity = 100f;

    private Transform parent;

    private void Start()
    {
        // Get the parent transform
        parent = transform.parent;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        // Get mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Rotate the parent object around its up axis based on mouse input
        parent.Rotate(Vector3.up, mouseX);
    }
}
