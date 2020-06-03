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
    [SerializeField] float posPitchFactor = -1.2f, ctrlPitchFactor = 10f, ctrlRollFactor = -10f, posYawFactor = -7f;
    float xOffSet, yOffset, pitch, yaw, roll;
    Vector2 direction;
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
    private void ShipRotation() //TODO fix animations to be more fluid, probably using Time.deltatime?
    {
        pitch = transform.localPosition.y * posPitchFactor - (direction.y * ctrlPitchFactor);
        yaw = transform.localPosition.x * posYawFactor;
        roll = direction.x * ctrlRollFactor;
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
