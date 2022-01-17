using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float maxDistance;
    [SerializeField] private float groundHeight;

    public float MaxDistance => maxDistance;

    private Vector3 groundHitPoint;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, -groundCheck.up, out hit, 50f))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                groundHitPoint = hit.point;
            }
        }

        transform.position = new Vector3(transform.position.x, groundHitPoint.y - groundHeight, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        if (Application.isPlaying)
            Gizmos.DrawSphere(groundHitPoint, 0.3f);
    }
}
