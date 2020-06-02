using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerInputActions controls;
    Rigidbody rb;
    [Tooltip ("in ms^-1")][SerializeField] float xSpeed = 4f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        
    }

    void Movement(Vector2 direction)//gets input values 
    {
        Debug.Log("X Axis" + direction);//only use the X axis, delete the .x to use all axis
        float xOffSet = xSpeed * Time.deltaTime * direction.x;
        print(xOffSet);
        rb.AddRelativeForce(Vector3.up * xOffSet);
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
