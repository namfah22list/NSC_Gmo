using UnityEngine;

public class InteractFocus : MonoBehaviour
{
    [Header("Player & Camera")]
    public Transform player;
    public Camera cam;

    [Header("Points")]
    public Transform focusPoint;
    public Transform cameraPoint;

    [Header("Settings")]
    public float moveSpeed = 4f;
    public float rotateSpeed = 6f;
    public float interactDistance = 3f;

    private bool isInteracting = false;
    private bool canInteract = false;

    void Update()
    {
        // ตรวจว่ากด E
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            Debug.Log("Pressed E");
            isInteracting = true;

            // ล็อคการเดิน (ถ้ามีสคริปต์เดิน)
            var move = player.GetComponent<MonoBehaviour>();
            if (move != null) move.enabled = false;
        }

        if (isInteracting)
        {
            MoveSmooth();
        }
    }

    void MoveSmooth()
    {
        // เคลื่อน player แบบสมูส
        player.position = Vector3.Lerp(player.position, focusPoint.position, Time.deltaTime * moveSpeed);

        Quaternion targetRot = Quaternion.LookRotation(focusPoint.forward);
        player.rotation = Quaternion.Slerp(player.rotation, targetRot, Time.deltaTime * rotateSpeed);

        // กล้องสมูส
        cam.transform.position = Vector3.Lerp(cam.transform.position, cameraPoint.position, Time.deltaTime * moveSpeed);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, cameraPoint.rotation, Time.deltaTime * rotateSpeed);

        // ถึงแล้ว
        if (Vector3.Distance(player.position, focusPoint.position) < 0.1f)
        {
            Debug.Log("Arrived!");
        }
    }

    // ตรวจระยะเข้า trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("Press E to interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}