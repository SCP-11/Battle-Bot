using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float power = 1000;
    public float dmg = 1;
    private GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active){
        Debug.Log("target; " + target);
        if(target == null){
            return;
        }
        Rigidbody rb = target.GetComponent<Rigidbody>();
        Transform parent = target.transform.parent;
        while(rb == null){
            rb = parent.GetComponent<Rigidbody>();
            parent = parent.parent;   
        }
        if(rb){
            Health h = rb.gameObject.GetComponent<Health>();
            if(h != null){
                h.takeDamage(dmg);
            }
        }
        rb.AddForce((rb.gameObject.transform.position - transform.position).normalized * power);
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("trigger collider other: "+ other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            Debug.Log("target; " + target);
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }
}
