using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RunCameraDolly : MonoBehaviour
{
    [SerializeField]
    public float TargetSpeed = 30f;

    [SerializeField, Min(0), Tooltip("Set to 0 for instant speed changes")]
    public float AccelerationRate = 1.5f;

    private CinemachineVirtualCamera Camera;
    private CinemachineTrackedDolly Dolly;
    private float CurrentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetupCamera();
    }

    // Update is called once per frame
    void Update()
    {
        MoveDolly();
    }


    private void SetupCamera()
    {
        Camera = GetComponent<CinemachineVirtualCamera>();
        Dolly = Camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        Dolly.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
    }


    private void MoveDolly()
    {
        CalculateSpeed();
        Dolly.m_PathPosition = Dolly.m_PathPosition + CurrentSpeed * Time.deltaTime;

        //If closed loop, make sure the pathposition will not overflow
        if (Dolly.m_Path.Looped && Dolly.m_PathPosition > Dolly.m_Path.PathLength)
        {
            Dolly.m_PathPosition = Dolly.m_PathPosition - Dolly.m_Path.PathLength;
        }

    }

    void CalculateSpeed()
    {

        // Set speed to target speed if difference is smaller than acceleration value, or acceleration is set to 0
        if (Math.Abs(CurrentSpeed - TargetSpeed) <= AccelerationRate | Math.Abs(AccelerationRate) < float.Epsilon)
        {
            CurrentSpeed = TargetSpeed;
            return;
        }

        // Accelerate
        else if (CurrentSpeed < TargetSpeed)
        {
            CurrentSpeed += AccelerationRate;
        }

        // Decelerate
        else
        {
            CurrentSpeed -= AccelerationRate;
        }
    }
}