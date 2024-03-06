using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerAdvanced : MonoBehaviour
{
    public WheelColliders wheelColliders;
    public WheelMeshes meshes;
    public WheelParticles particles;
     Rigidbody carRB;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;

    public float motorPower;
    public float speed;
    public float brakePower;
    public float slipAngle;
    public AnimationCurve steeringCurve;
    // Start is called before the first frame update
    void Start()
    {
        carRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = carRB.velocity.magnitude;
        CheckInput();
        ApplyPower();
        ApplySteering();
        ApplyBrake();
        ApplyWheelsPosition();
    }

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
        slipAngle = Vector3.Angle(transform.forward, carRB.velocity-transform.forward);
        if (slipAngle < 120f)
        {
            if (gasInput < 0)
            {
                
                    brakeInput = Mathf.Abs(gasInput);
                    gasInput = 0;
                    
                
                
               
            }
            else
            {
                brakeInput = 0;
            }
           

        }
        else
        {
            brakeInput = 0;
        }

    }

    void ApplyBrake()
    {
        wheelColliders.FrontLeft.brakeTorque = brakeInput * brakePower * 0.7f;
        wheelColliders.FrontRight.brakeTorque = brakeInput * brakePower * 0.7f;
        wheelColliders.RearRight.brakeTorque = brakeInput * brakePower * 0.3f;
        wheelColliders.RearLeft.brakeTorque = brakeInput * brakePower * 0.3f;

    }

    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);   
        wheelColliders.FrontLeft.steerAngle = steeringAngle;
        wheelColliders.FrontRight.steerAngle = steeringAngle;   
    
    }

    void ApplyPower()
    { 
        wheelColliders.RearRight.motorTorque = (motorPower * gasInput)*4;
        wheelColliders.RearLeft.motorTorque = (motorPower * gasInput)*4;
    }

    void ApplyWheelsPosition()
    {
        UpdateWheel(wheelColliders.FrontLeft, meshes.FrontLeft);
        UpdateWheel(wheelColliders.RearLeft, meshes.RearLeft);
        UpdateWheel(wheelColliders.FrontRight, meshes.FrontRight);
        UpdateWheel(wheelColliders.RearRight, meshes.RearRight);
    }
    void UpdateWheel(WheelCollider col, MeshRenderer wheelmesh)
    {
        Quaternion quat;
        Vector3 position;
        col.GetWorldPose(out position, out quat);
        wheelmesh.transform.position = position;
        wheelmesh.transform.rotation = quat;
    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FrontRight;
    public WheelCollider FrontLeft;
    public WheelCollider RearRight;
    public WheelCollider RearLeft;
}

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FrontRight;
    public MeshRenderer FrontLeft;
    public MeshRenderer RearRight;
    public MeshRenderer RearLeft;
}

public class WheelParticles
{
    public GameObject FrontRight;
    public GameObject FrontLeft;
    public GameObject RearRight;
    public GameObject RearLeft;
}