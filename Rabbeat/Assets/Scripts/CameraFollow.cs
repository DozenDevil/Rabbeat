using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Player player;
    [SerializeField] private float smoothSpeed = 0.125f;

    private bool moveX;

    private void LateUpdate()
    {
        moveX = player.CameraMoveX;
        if (target.position.y > transform.position.y)
        {
            Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        if (moveX)
        {
            if (target.position.x > transform.position.x)
            {
                Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
            if (target.position.x < transform.position.x)
            {
                Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}
