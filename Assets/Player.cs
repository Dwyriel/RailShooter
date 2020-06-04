using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInputActions controls;
    [Tooltip("in Ms^-1")] [SerializeField] float xSpeed = 6f, ySpeed = 6f;
    [Tooltip("in Ms")] [SerializeField] float xPRange = 8f, xNRange = -8f, yPRange = 7f, yNRange = -6f; //(P)ositive and (N)egative movement range for x and y
    [SerializeField] float posPitchFactor = -1.2f, ctrlPitchFactor = 10f, ctrlRollFactor = 10f, posYawFactor = -7f;
    [Min(0f)] [SerializeField] float animationResetSpeed = 0.5f, animationSpeed = 10f;
    float xOffSet, yOffset, pitch, yaw, roll, pitchcalc;
    Vector2 direction;
    bool testbool1, testbool2;
    void Awake()
    {
        controls = new PlayerInputActions();
        // the first part of the code shows the same path like in the Input Action Window, the scecond code part reads the input value
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShipRotation();
        ShipMovement();
    }

    private void ShipRotation()
    {
        //y - working
        if (direction.y != 0f) { pitchcalc = Mathf.Clamp(pitchcalc + (direction.y * ctrlPitchFactor * Time.deltaTime * animationSpeed), -ctrlPitchFactor, ctrlPitchFactor); }
        else if (pitchcalc > 1f) { pitchcalc = Mathf.Clamp(pitchcalc - Mathf.MoveTowards(pitchcalc * Time.deltaTime, 0f, -animationResetSpeed), -ctrlPitchFactor, ctrlPitchFactor); }
        else if (pitchcalc < -1f) { pitchcalc = Mathf.Clamp(pitchcalc - Mathf.MoveTowards(pitchcalc * Time.deltaTime, 0f, -animationResetSpeed), -ctrlPitchFactor, ctrlPitchFactor); }
        else { pitchcalc = Mathf.Clamp(pitchcalc, -0f, 0f); }
        //x - working
        if (direction.x != 0f) { roll = Mathf.Clamp(roll - (direction.x * ctrlRollFactor * Time.deltaTime * animationSpeed), -ctrlRollFactor, ctrlRollFactor); }
        else if (roll < -1f) { roll = Mathf.Clamp(roll - Mathf.MoveTowards(roll * Time.deltaTime, 0f, -animationResetSpeed), -ctrlRollFactor, ctrlRollFactor); }
        else if (roll > 1f) { roll = Mathf.Clamp(roll - Mathf.MoveTowards(roll * Time.deltaTime, 0f, -animationResetSpeed), -ctrlRollFactor, ctrlRollFactor); }
        else { roll = Mathf.Clamp(roll, -0f, 0f); }
        pitch = transform.localPosition.y * posPitchFactor - pitchcalc;
        yaw = transform.localPosition.x * posYawFactor;
        /*previous code if fast rollback is needed: 
        pitch = transform.localPosition.y * posPitchFactor - (direction.y * ctrlPitchFactor);
        yaw = transform.localPosition.x * posYawFactor;
        roll = direction.x * ctrlRollFactor;
        */
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ShipMovement()
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x + (Time.deltaTime * xOffSet), xNRange, xPRange)
            , Mathf.Clamp(transform.localPosition.y + (Time.deltaTime * yOffset), yNRange, yPRange)
            , transform.localPosition.z);
    }

    private void OnEnable()
    {
        controls.PlayerControls.Horizontal.performed += Movement;
        controls.PlayerControls.Horizontal.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControls.Horizontal.performed -= Movement;
        controls.PlayerControls.Horizontal.Disable();
    }
    void Movement(InputAction.CallbackContext context)//gets input values 
    {
        direction = context.ReadValue<Vector2>();
        Debug.Log("Axis" + direction);
        xOffSet = xSpeed * direction.x;
        yOffset = ySpeed * direction.y;
    }
}
