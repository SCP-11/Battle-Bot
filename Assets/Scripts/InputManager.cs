using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    private List<InputDevice> assignedDevices = new List<InputDevice>();
    public void AssignDeviceToPlayer(InputDevice device)
    {
        if (assignedDevices.Contains(device))
        {
            return;
        }
        PlayerInput playerInput = PlayerInput.Instantiate(prefab, controlScheme: "Player", pairWithDevice: device);
    }
    private void OnDeviceAdded(InputDevice device)
    {
        Debug.Log($"Device added: {device.name}");
        AssignDeviceToPlayer(device);
    }
    void Start()
    {
        
    }
    public 
    // Update is called once per frame
    void Update()
    {
        
    }
}
