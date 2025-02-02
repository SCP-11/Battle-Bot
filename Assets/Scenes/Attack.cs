using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public Weapon weapon;
    public Transform hitpoint;
    private InputAction attackAction;
    private Vector3 weap_og_pos;
    void Start()
    {
        
        attackAction = GetComponent<PlayerInput>().actions.FindActionMap("Player").FindAction("Attack");
        weap_og_pos = weapon.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){
        if (attackAction.IsPressed())
        {
            weapon.transform.localPosition = hitpoint.localPosition;
            weapon.SetActive(true);
        }else{
            // weapon.SetActive(false);
            weapon.transform.localPosition = weap_og_pos;
        }
    }
}
