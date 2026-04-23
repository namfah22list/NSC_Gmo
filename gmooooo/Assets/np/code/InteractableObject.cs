using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 3f;
    public Transform cameraTarget; // ตำแหน่งที่กล้องจะมอง (ใส่ Empty GameObject)
    public float cameraDistance = 2f;
    public float cameraHeight = 1.5f;

    [Header("UI")]
    public string interactHint = "กด [E] เพื่อตรวจสอบ";

    private bool playerInRange = false;
    private Transform player;
    private InteractCameraController camController;

    void Start()
    {
        // ถ้าไม่ได้กำหนด cameraTarget ให้ใช้ตัวเอง
        if (cameraTarget == null)
            cameraTarget = transform;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (camController != null)
        {
            camController.LockCameraTo(cameraTarget, cameraDistance, cameraHeight);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.transform;
            camController = other.GetComponentInChildren<InteractCameraController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;

            if (camController != null)
                camController.UnlockCamera();
        }
    }

    // วาด Gizmo แสดง range ใน Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    // Getter
    public bool IsPlayerInRange() => playerInRange;
    public string GetHint() => interactHint;
}