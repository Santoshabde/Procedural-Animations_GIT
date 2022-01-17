using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Foot
{
    Right,
    Left
}

public class LegIKController : MonoBehaviour
{
    [SerializeField] private Foot footType;
    [SerializeField] private RobotController robotController;
    [SerializeField] private Transform groundCheckIndicator;
    [SerializeField] private Transform initialTargetHolder;
    [SerializeField] private Transform target;

    private Vector3 targetPosition;
    private Vector3 groundHitPoint;

    public float distance;
    private void Start()
    {
        //Record the position of the Target!!
        targetPosition = initialTargetHolder.position;
    }

    private void Update()
    {
        Vector3 footPosition = Vector3.Lerp(target.position, targetPosition, 0.1f);
        footPosition.y += Mathf.Sin((targetPosition - target.position).magnitude * 0.01f * Mathf.PI) * 2;
        target.position = footPosition;

        //Ground check!!
        RaycastHit hit;
        Debug.DrawRay(groundCheckIndicator.position, -groundCheckIndicator.forward * 10f, Color.red);
        if (Physics.Raycast(groundCheckIndicator.position, -groundCheckIndicator.forward, out hit, 10f))
        {
            if(hit.transform.gameObject.tag == "Ground")
            {
                groundHitPoint = hit.point;
            }
        }

        float calculatedDistance;
        //Knowing which foot??
        if(footType == Foot.Left)
        {
            calculatedDistance = robotController.MaxDistance;
        }
        else
        {
            calculatedDistance = robotController.MaxDistance;
        }

        //Distance calculation!!
        distance = (groundHitPoint - target.position).magnitude;

        if (distance > calculatedDistance)
        {
            targetPosition = groundHitPoint;
        }
    }


    //Gizmos!!
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(initialTargetHolder.position, new Vector3(0.3f, 0.3f, 0.3f));
        }

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        if (Application.isPlaying)
        Gizmos.DrawSphere(groundHitPoint, 0.3f);
    }
}
