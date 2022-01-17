using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject targetPicked = null;
    [SerializeField] private GameObject tempMisile;
    [SerializeField] private List<GameObject> targetsInRange;

    [SerializeField] private Transform missileSpawnPoint;
    [SerializeField] private GameObject missileSpawnVfx;
    [SerializeField] private GameObject robotHead;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed;

    private float currentSpeed = 0;
    private void Start()
    {
        StartCoroutine(StartFunction());
    }

    private IEnumerator StartFunction()
    {
        yield return new WaitForSeconds(1f);
        currentSpeed = speed;
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * currentSpeed;

        if (targetPicked != null)
            robotHead.transform.rotation = Quaternion.Lerp(robotHead.transform.rotation, Quaternion.LookRotation((targetPicked.transform.position - robotHead.transform.position).normalized), Time.deltaTime * rotationSpeed);
  
        if (targetsInRange.Count != 0 && targetPicked == null)
        {
            targetPicked = targetsInRange[Random.Range(0, targetsInRange.Count)];
            StartCoroutine(Temp());        
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireMissile();
        }
    }

    private IEnumerator Temp()
    {
        yield return new WaitForSeconds(3f);
        FireMissile();
        targetsInRange.Remove(targetPicked);
        targetPicked = null;
    }

    private void FireMissile()
    {
        GameObject missile = Instantiate(tempMisile, missileSpawnPoint.position, missileSpawnPoint.transform.rotation);
        GameObject missileVFX = Instantiate(missileSpawnVfx);
        missileVFX.transform.SetParent(missileSpawnPoint);
        missileVFX.transform.localPosition = Vector3.zero;
        missileVFX.transform.localEulerAngles = Vector3.zero;

        missile.GetComponent<Missile>().SetTarget(targetPicked.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<IDamagable>() != null)
        {
            if (targetsInRange == null)
                targetsInRange = new List<GameObject>();

            targetsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<IDamagable>() != null)
        {
            if (targetsInRange != null)
                targetsInRange.Remove(other.gameObject);
        }
    }

}
