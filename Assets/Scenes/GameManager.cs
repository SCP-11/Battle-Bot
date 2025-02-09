using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //a list of player game objects in the scene
    public List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        int dev_id = 0;
        InputDevice keyBoard = null;
        //assign devices to players while checking if the device suitable for the playerinput
        foreach (GameObject player in players)
        {
            PlayerInput playerInput = player.GetComponent<PlayerInput>();
            
            while (dev_id < InputSystem.devices.Count)
            {
                if (playerInput != null)
                {
                    InputDevice device = InputSystem.devices[dev_id];
                    if(device is Keyboard){
                        keyBoard = device;
                    }
                    if(playerInput.SwitchCurrentControlScheme(device) && (device is Gamepad|| device is Keyboard)){
                        Debug.Log("Device assigned to player input");
                        dev_id++;
                        break;
                    }else{
                        Debug.Log("Device not suitable for player input");
                    }
                }
                dev_id++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject player = players[i];
            if(player == null){
                RestartGame();
                continue;
            }
            Health health = player.GetComponent<Health>();
            if(health.health <= 0){
                RestartGame();
            }
        }   
    }

    public void RestartGame(){
        //restart the game
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
