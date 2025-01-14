using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
// using Vector3 = UnityEngine.Vector3;
using UnityEngine.InputSystem;
// using Vector2 = UnityEngine.Vector2;
public class Car : MonoBehaviour
{
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public Transform wheelFLtrans;
    public Transform wheelFRtrans;
    public Transform wheelRLtrans;
    public Transform wheelRRtrans;
    public float maxSteer = 30;
    public float motorForce = 50;
    public float brakeForce = 100;
    public float maxSpeed = 100;
    public float maxBrake = 100;
    public GameObject tail;
    public GameObject target;
    
    private InputAction driveAction;
    public InputActionAsset inputActions;
    private Vector2 moveInput;

    private pre_left_torque = 0;
    private pro_right_torque = 0;
    // Start is called before the first frame update
    void Awake()
    {   
        driveAction = inputActions.FindActionMap("Player").FindAction("Drive");

    }

    private  void OnEnable()
    {
        driveAction.Enable();
    }

    private void OnDisable()
    {
        driveAction.Disable();
    }
    // Update is called once per frame
    void Update()
    {
    }
    void UpdateWheelModel(){
        Quaternion rot;
        Vector3 pos;
        wheelFL.GetWorldPose(out pos, out rot);
        wheelFLtrans.position = pos;
        wheelFLtrans.rotation = rot;
        wheelFR.GetWorldPose(out pos, out rot);
        wheelFRtrans.position = pos;
        wheelFRtrans.rotation = rot;
        wheelRL.GetWorldPose(out pos, out rot);
        wheelRLtrans.position = pos;
        wheelRLtrans.rotation = rot;
        wheelRR.GetWorldPose(out pos, out rot);
        wheelRRtrans.position = pos;
        wheelRRtrans.rotation = rot;
    }


    void FixedUpdate(){
        moveInput = driveAction.ReadValue<Vector2>();
        float moveInput_magnitude = moveInput.magnitude;
        Debug.Log(moveInput);
        float force = Mathf.Sqrt(Mathf.Pow(moveInput.x, 2) + Mathf.Pow(moveInput.y, 2)) * motorForce;
        if(moveInput.x == 0 && moveInput.y == 0){
            // wheelFL.brakeTorque = maxBrake;
            // wheelFR.brakeTorque = maxBrake;
            // wheelRL.brakeTorque = maxBrake;
            // wheelRR.brakeTorque = maxBrake;
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            return;
        }

        float left_accel = 0;
        float right_accel = 0;
        bool top_left = moveInput.x <= 0 && moveInput.y >= 0;
        bool top_right = moveInput.x > 0 && moveInput.y >= 0;
        bool bottom_left = moveInput.x <= 0 && moveInput.y < 0;
        bool bottom_right = moveInput.x > 0 && moveInput.y < 0;
        if(bottom_left){
            left_accel = -moveInput_magnitude * motorForce;
        }else if(top_right){
            left_accel = moveInput_magnitude * motorForce;
            
        }else{
            if(top_left){
                left_accel = (moveInput.y * 2 - 1) * motorForce;
            }else if(bottom_right){
                left_accel = (moveInput.y * 2 + 1) * motorForce;
            }
        }

        if(top_left){
            right_accel = moveInput_magnitude * motorForce;
        }else if(bottom_right){
            right_accel = -moveInput_magnitude * motorForce;
        }
        else{
            if(top_right){
                right_accel = (moveInput.y * 2 - 1) * motorForce;
            }else if(bottom_left){
                right_accel = (moveInput.y * 2 + 1) * motorForce;
            }
        }

        Debug.Log("Left: " + left_accel + " Right: " + right_accel);

        
        // wheelFL.brakeTorque = brakeForce;
        // wheelFR.brakeTorque = brakeForce;
        // wheelRL.brakeTorque = brakeForce;
        // wheelRR.brakeTorque = brakeForce;
        if(left_accel < pre_left_accel){
        wheelFL.brakeTorque = brakeForce;
        wheelRL.brakeTorque = brakeForce;}
        else{
        wheelFL.brakeTorque = 0;
        wheelRL.brakeTorque = 0;

        }
        if(right_accel < pre_right_torque){
        wheelFR.brakeTorque = brakeForce;
        wheelRR.brakeTorque = brakeForce;
        }
        else{
        wheelFR.brakeTorque = 0;
        wheelRR.brakeTorque = 0;

        }
        Debug.Log("Brake torque is " + wheelFL.brakeTorque );
        wheelRL.motorTorque = left_accel;
        wheelFL.motorTorque = left_accel;
        wheelRR.motorTorque = right_accel;
        wheelFR.motorTorque = right_accel;

        pre_left_torque = left_accel;
        pre_right_torque = right_accel;

        UpdateWheelModel();
    }
    // public void Drive(InputAction.CallbackContext context)
    // {
    //     moveInput = context.ReadValue<Vector2>();
    //     Debug.Log(moveInput);
    // }
    // private void OnEnable()
    // {
    //     inputActions.Player.Enable();
    //     inputActions.Player.Move.performed += OnMove;
    //     inputActions.Player.Move.canceled += OnMove;
    // }

    // private void OnDisable()
    // {
    //     inputActions.Player.Move.performed -= OnMove;
    //     inputActions.Player.Move.canceled -= OnMove;
    //     inputActions.Player.Disable();
    // }

    // private void OnMove(InputAction.CallbackContext context)
    // {
    //     moveInput = context.ReadValue<Vector2>();
    // }


}
