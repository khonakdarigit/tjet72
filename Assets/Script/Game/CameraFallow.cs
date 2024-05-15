using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField] Transform followTransform;

    [SerializeField] float smoothSpeed = 0.18f;

    public static bool isCameraFollow;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        isCameraFollow = true;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(followTransform.position.x + 5f, followTransform.position.y + 1f, this.transform.position.z);

        if (isCameraFollow) transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}
