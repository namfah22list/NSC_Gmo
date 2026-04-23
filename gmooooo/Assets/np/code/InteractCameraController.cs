using UnityEngine;

public class InteractCameraController : MonoBehaviour
{
    [Header("Smooth Settings")]
    public float smoothSpeed = 5f;        // ความเร็วในการ smooth
    public float rotationSmooth = 4f;

    private Transform targetObject;
    private bool isLocked = false;
    private float lockedDistance;
    private float lockedHeight;

    // เก็บ state เดิมของกล้อง
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool savedOriginal = false;

    void LateUpdate()
    {
        if (!isLocked || targetObject == null) return;

        // คำนวณตำแหน่งเป้าหมายของกล้อง
        Vector3 desiredPosition = targetObject.position
            - targetObject.forward * lockedDistance
            + Vector3.up * lockedHeight;

        // Smooth move
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            Time.deltaTime * smoothSpeed
        );

        // Smooth look at
        Quaternion desiredRotation = Quaternion.LookRotation(
            targetObject.position - transform.position
        );
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            Time.deltaTime * rotationSmooth
        );
    }

    public void LockCameraTo(Transform target, float distance, float height)
    {
        // บันทึกตำแหน่งเดิม
        if (!savedOriginal)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            savedOriginal = true;
        }

        targetObject = target;
        lockedDistance = distance;
        lockedHeight = height;
        isLocked = true;

        Debug.Log($"[Camera] ล็อคกล้องไปที่: {target.name}");
    }

    public void UnlockCamera()
    {
        isLocked = false;
        targetObject = null;
        savedOriginal = false;
        Debug.Log("[Camera] ปลดล็อคกล้อง");
    }

    public bool IsLocked() => isLocked;
}