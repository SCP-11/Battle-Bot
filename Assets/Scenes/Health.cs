using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100;
    public TMPro.TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + Mathf.CeilToInt(health).ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateHealthUI(){
        healthText.text = "Health: " + Mathf.CeilToInt(health).ToString();
    }
    public void takeDamage(float damage){
        health -= damage;
        UpdateHealthUI();
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
