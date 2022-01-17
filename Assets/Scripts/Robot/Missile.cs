using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed = 1;

    private Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target; 
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;

        if(target != null)
        transform.forward = Vector3.Lerp(transform.forward, (target.position - transform.position).normalized, Time.deltaTime * turnSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<IDamagable>() != null)
        {
            other.transform.GetComponent<IDamagable>().OnDamage();
            Destroy(this.gameObject);
        }
    }
}
