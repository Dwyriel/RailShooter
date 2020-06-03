using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerInputActions controls;
    [Tooltip("in ms^-1")] [SerializeField] float xSpeed = 6f, ySpeed = 6f;
    [Tooltip("in ms")] [SerializeField] float xPRange = 8f, xNRange = -8f, yPRange = 7f, yNRange = -6f; //(P)ositive and (N)egative movement range for x and y
    float xOffSet, yOffset, curXspeed, curYspeed;
    Vector2 direct;
    void Awake()
    {
        controls = new PlayerInputActions();
        controls.PlayerControls.Horizontal.performed += ctx => Movement(ctx.ReadValue<Vector2>());
        // the first part of the code shows the same path like in the Input Action Window, the scecond code part reads the input value
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShipMovement();
    }

    private void ShipMovement()
    {
        /*if (!Keyboard.current.anyKey.isPressed)
        {
            yOffset = xOffSet = 0;
        }*/
        curYspeed = Mathf.Clamp(transform.localPosition.y + yOffset, yNRange, yPRange);
        curXspeed = Mathf.Clamp(transform.localPosition.x + xOffSet, xNRange, xPRange);
        transform.localPosition = new Vector3(curXspeed, curYspeed, transform.localPosition.z);
    }

    void Movement(Vector2 direction)//gets input values 
    {
        direct = direction;
        Debug.Log("Axis" + direction);//only use the X axis, delete the .x to use all axis
        xOffSet = xSpeed * Time.deltaTime * direct.x;
        yOffset = ySpeed * Time.deltaTime * direct.y;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
