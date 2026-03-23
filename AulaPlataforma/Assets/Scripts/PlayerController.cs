using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Simple Roll-a-Ball player controller using the new Input System.
/// Usage:
/// - Add this component to your ball GameObject that has a Rigidbody and SphereCollider.
/// - Add a PlayerInput component, set the Actions asset to your Input Actions (e.g. InputSystem_Actions),
///   and set Behavior to "Send Messages" so the engine calls OnMove(InputValue).
/// - The Move action should be a Vector2 (X = left/right, Y = forward/back) — this project already has Player/Move.
/// - Tweak moveSpeed, maxSpeed and ForceMode in the inspector to taste.
///
/// This script supports two input hookup styles:
/// 1) PlayerInput (Behavior = Send Messages) -> OnMove(InputValue)
/// 2) Generated wrapper or manual calls -> call SetMove(Vector2)
///
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Force multiplier applied each FixedUpdate from input vector.")]
    public float moveSpeed = 10f;

    [Tooltip("Maximum horizontal speed (XZ plane). Set to a reasonable value depending on mass/drag.)")]
    public float maxSpeed = 5f;

    [Tooltip("Which ForceMode to use when applying movement force.")]
    public ForceMode forceMode = ForceMode.Force;

    [Tooltip("If true, horizontal velocity (XZ) will be clamped to maxSpeed.")]
    public bool clampHorizontalVelocity = true;

    // Current movement input (x = left/right, y = forward/back)
    Vector2 moveInput = Vector2.zero;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("PlayerController requires a Rigidbody component.", this);
    }

    /// <summary>
    /// Called by PlayerInput (Send Messages) when the Move action changes.
    /// Signature must be exactly OnMove(InputValue).
    /// </summary>
    /// <param name="value"></param>
    public void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        SetMove(v);
    }

    /// <summary>
    /// Alternative: call this from generated input-action wrapper or other code.
    /// </summary>
    /// <param name="movement">X = left/right, Y = forward/back</param>
    public void SetMove(Vector2 movement)
    {
        moveInput = movement;
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        // Convert Vector2 input (x,z) to world-space force relative to the object's orientation
        Vector3 localInput = new Vector3(moveInput.x, 0f, moveInput.y);

        // If you want movement relative to camera, multiply by camera transform here.
        Vector3 force = localInput * moveSpeed;

        rb.AddForce(force, forceMode);

        if (clampHorizontalVelocity)
        {
            Vector3 vel = rb.linearVelocity;
            Vector3 horizontal = new Vector3(vel.x, 0f, vel.z);
            float mag = horizontal.magnitude;
            if (mag > maxSpeed)
            {
                Vector3 limited = horizontal.normalized * maxSpeed;
                rb.linearVelocity = new Vector3(limited.x, vel.y, limited.z);
            }
        }
    }

    // Optional debug: draw input vector in the editor
    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.cyan;
        Vector3 dir = new Vector3(moveInput.x, 0f, moveInput.y);
        Gizmos.DrawLine(transform.position, transform.position + dir);
    }
}

