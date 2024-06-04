using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset; // The offset distance between the camera and the target
    public float smoothSpeed = 0.125f; // The speed of the camera's smooth follow

    void LateUpdate()
    {
        if (LoadCharacter.activeCharacter != null)
        {
            Vector3 desiredPosition = LoadCharacter.activeCharacter.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(LoadCharacter.activeCharacter.transform);
        }
    }
}
