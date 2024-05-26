using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // Transform ของตัวละครที่จะให้กล้องติดตาม
    [SerializeField] private Vector3 offset;    // ระยะห่างระหว่างกล้องและตัวละคร
    [SerializeField] private float smoothSpeed = 0.125f; // ความเร็วในการตาม

    private void LateUpdate()
    {
        // คำนวณตำแหน่งที่ต้องการของกล้อง
        Vector3 desiredPosition = player.position + offset;
        
        // เคลื่อนที่กล้องไปยังตำแหน่งที่ต้องการด้วยความเร็วที่กำหนด
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        // ตั้งค่าตำแหน่งของกล้อง
        transform.position = smoothedPosition;

        // ตั้งค่าให้กล้องหันไปทางตัวละคร
        transform.LookAt(player);
    }
}
