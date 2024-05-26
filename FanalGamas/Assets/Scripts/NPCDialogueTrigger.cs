using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public GameObject dialogueUI; // อ้างอิงถึง UI ของการสนทนา

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // เปิด UI การสนทนา
            dialogueUI.SetActive(true);
            // อาจเพิ่มโค้ดเพื่อหยุดการเคลื่อนไหวของ player ขณะสนทนา
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ปิด UI การสนทนา
            dialogueUI.SetActive(false);
            // อาจเพิ่มโค้ดเพื่อคืนการเคลื่อนไหวของ player หลังจากการสนทนาเสร็จสิ้น
        }
    }
}
