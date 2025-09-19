using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    [Header("Eye Settings")]
    public Transform player;          // The player to follow
    public Transform leftEye;         // Left eye bone
    public Transform rightEye;        // Right eye bone

    [Header("Rotation Settings")]
    public float maxEyeAngle = 30f;   // Max degrees eyes can rotate
    public float eyeSpeed = 5f;       // How quickly eyes move
    public bool allowY = true;        // Allow up/down movement?
    public float followDistance = 2f; // Only follow if within this distance

    private Quaternion leftEyeInitial;
    private Quaternion rightEyeInitial;

    void Start()
    {
        if (leftEye != null) leftEyeInitial = leftEye.localRotation;
        if (rightEye != null) rightEyeInitial = rightEye.localRotation;
    }

    void LateUpdate()
    {
        if (player == null || leftEye == null || rightEye == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > followDistance)
        {
            // Reset to neutral if player is too far
            leftEye.localRotation = Quaternion.Slerp(leftEye.localRotation, leftEyeInitial, Time.deltaTime * eyeSpeed);
            rightEye.localRotation = Quaternion.Slerp(rightEye.localRotation, rightEyeInitial, Time.deltaTime * eyeSpeed);
            return;
        }

        // Direction from head center (transform) to player (shared for both eyes)
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        if (!allowY) dirToPlayer.y = 0; // Flatten vertical axis if disabled
        dirToPlayer.Normalize();

        // Calculate look rotation
        Quaternion lookRotation = Quaternion.LookRotation(dirToPlayer, Vector3.up);

        // Convert to local rotation
        Quaternion localRotation = Quaternion.Inverse(transform.rotation) * lookRotation;

        // Clamp rotation
        localRotation = ClampRotation(leftEyeInitial, localRotation, maxEyeAngle);

        // Apply same rotation to both eyes
        leftEye.localRotation = Quaternion.Slerp(leftEye.localRotation, localRotation, Time.deltaTime * eyeSpeed);
        rightEye.localRotation = Quaternion.Slerp(rightEye.localRotation, localRotation, Time.deltaTime * eyeSpeed);
    }

    private Quaternion ClampRotation(Quaternion initial, Quaternion target, float maxAngle)
    {
        float angle = Quaternion.Angle(initial, target);
        if (angle > maxAngle)
        {
            target = Quaternion.Slerp(initial, target, maxAngle / angle);
        }
        return target;
    }
}