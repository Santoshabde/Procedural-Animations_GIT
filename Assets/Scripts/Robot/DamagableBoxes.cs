using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableBoxes : MonoBehaviour, IDamagable
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject normalObj;
    [SerializeField] private GameObject destructionObj;

    public void OnDamage()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        normalObj.SetActive(false);
        destructionObj.SetActive(true);

        for (int i = 0; i < destructionObj.transform.childCount; i++)
        {
            Debug.Log(destructionObj.transform.GetChild(i).name);
            destructionObj.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * 10f, ForceMode.Impulse);
        }

        this.GetComponent<BoxCollider>().enabled = false;
    }
}
