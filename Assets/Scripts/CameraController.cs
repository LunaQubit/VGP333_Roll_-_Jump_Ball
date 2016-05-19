using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothDampTime = 30f;

    private Vector3 currentVelocity;
    private Transform cachedTransform;
    private Vector3 offset;

    void Start()
    {
        cachedTransform = GetComponent<Transform>();

        offset = cachedTransform.position - player.position;
    }

    void LateUpdate()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();

        Vector3 targetPosition = player.position + offset;
        cachedTransform.position = Vector3.SmoothDamp( 
            cachedTransform.position, targetPosition + rb.velocity,
            ref currentVelocity, smoothDampTime * Time.deltaTime );
    }
}
