using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg_Tip_Bone : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool activate = true;
    [SerializeField] private bool reset = false;

    private Vector3 targetLocation;
    private float lerp;
    private void Start()
    {
        targetLocation = target.transform.position;
    }

    private void Update()
    {
        if (activate)
        {
            Vector3 footPosition = Vector3.Lerp(target.transform.position, targetLocation, lerp);
            footPosition.y += Mathf.Sin((targetLocation - target.transform.position).magnitude * 0.1f * Mathf.PI) * 0.4f;
            target.transform.position = footPosition;
            lerp += Time.deltaTime;
        }

        if(reset)
        {
            targetLocation = target.transform.position;
            reset = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Target")
        {
            lerp = 0;
            Debug.Log("On Exit Collisions");
            targetLocation = other.transform.position;
        }
    }
}
