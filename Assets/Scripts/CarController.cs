using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{    
    private WheelCollider RL_collider;
    private WheelCollider RR_collider;
    private WheelCollider FL_collider;
    private WheelCollider FR_collider;
    private Rigidbody self_rb;
    private bool isSelected = false;
    private bool isForward = true;


    private void Awake() {
        FL_collider = GameObject.Find("WheelsColliders/FrontLeftWheel").GetComponent<WheelCollider>();
        FR_collider = GameObject.Find("WheelsColliders/FrontRightWheel").GetComponent<WheelCollider>();
        RL_collider = GameObject.Find("WheelsColliders/RearLeftWheel").GetComponent<WheelCollider>();
        RR_collider = GameObject.Find("WheelsColliders/RearRightWheel").GetComponent<WheelCollider>();

        self_rb = GetComponent<Rigidbody>();
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            isSelected = !isSelected;
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            isForward = !isForward;
        }
    }


    private void FixedUpdate() {
        if (isSelected) {
            float move = Input.GetAxis("Vertical");
            float stAngle = Input.GetAxis("Horizontal");

            FL_collider.steerAngle = stAngle * 30f;
            FR_collider.steerAngle = stAngle * 30f;

            if (isForward) {
                if (move >= 0) {
                    RR_collider.motorTorque = move * 1000f;
                    RL_collider.motorTorque = move * 1000f;
                    FL_collider.brakeTorque = 0f;
                    FR_collider.brakeTorque = 0f;
                    RL_collider.brakeTorque = 0f;
                    RR_collider.brakeTorque = 0f;
                } else {
                    FL_collider.brakeTorque = 50000f;
                    FR_collider.brakeTorque = 50000f;
                    RL_collider.brakeTorque = 50000f;
                    RR_collider.brakeTorque = 50000f;
                }
            } else {
                if (move < 0) {
                    RR_collider.motorTorque = move * 1000f;
                    RL_collider.motorTorque = move * 1000f;
                    FL_collider.brakeTorque = 0f;
                    FR_collider.brakeTorque = 0f;
                    RL_collider.brakeTorque = 0f;
                    RR_collider.brakeTorque = 0f;
                } else {
                    FL_collider.brakeTorque = 50000f;
                    FR_collider.brakeTorque = 50000f;
                    RL_collider.brakeTorque = 50000f;
                    RR_collider.brakeTorque = 50000f;
                }
            }
        }
    }
}
