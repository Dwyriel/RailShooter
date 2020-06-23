using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    public int HP = 20;
    [Header("Movement Speed")]
    [Tooltip("in Ms^-1")] [SerializeField] float ySpeed = 18f;
    [Tooltip("in Ms^-1")] [SerializeField] float xSpeed = 20f;
    [Header("Camera Based Positioning")] //(P)ositive and (N)egative movement range for x and y
    [Tooltip("in Ms")] [SerializeField] float yPRange = 8f;
    [Tooltip("in Ms")] [SerializeField] float yNRange = -7f;
    [Tooltip("in Ms")] [SerializeField] float xPRange = 10f;
    [Tooltip("in Ms")] [SerializeField] float xNRange = -10f;
    [Header("Animations")]
    [SerializeField] float posPitchFactor = -1.5f;
    [SerializeField] float ctrlPitchFactor = 12.5f;
    [SerializeField] float ctrlRollFactor = 16f;
    [SerializeField] float posYawFactor = 2.3f;
    [Min(0f)] [SerializeField] float animationResetSpeed = 0.5f;
    [Min(0f)] [SerializeField] float animationSpeed = 8f;
    [Header("Lazers Particles")]
    [SerializeField] Transform[] parent;
    [SerializeField] GameObject[] bullets;
    [SerializeField] GameObject[] rfBullets;
    float xOffSet, yOffset, pitch, yaw, roll, pitchcalc, shootInput;
    bool playerHasControl = true, ableToMove = true;
    Vector2 direction;
    PlayerInputActions controls;

    void Awake()
    {
        controls = new PlayerInputActions();
    }

    void Start()
    {

    }

    void Update()
    {
        if (ableToMove)
        {
            ShipRotation();
            ShipMovement();
        }
    }

    private void ShipRotation()
    {
        //y - working
        if (direction.y != 0f && playerHasControl) { pitchcalc = Mathf.Clamp(pitchcalc + (direction.y * ctrlPitchFactor * Time.deltaTime * animationSpeed), -ctrlPitchFactor, ctrlPitchFactor); }
        else if (pitchcalc > 1f) { pitchcalc = Mathf.Clamp(pitchcalc - Mathf.MoveTowards(pitchcalc * Time.deltaTime, 0f, -animationResetSpeed), -ctrlPitchFactor, ctrlPitchFactor); }
        else if (pitchcalc < -1f) { pitchcalc = Mathf.Clamp(pitchcalc - Mathf.MoveTowards(pitchcalc * Time.deltaTime, 0f, -animationResetSpeed), -ctrlPitchFactor, ctrlPitchFactor); }
        else { pitchcalc = Mathf.Clamp(pitchcalc, -0f, 0f); }
        //x - working
        if (direction.x != 0f && playerHasControl) { roll = Mathf.Clamp(roll - (direction.x * ctrlRollFactor * Time.deltaTime * animationSpeed), -ctrlRollFactor, ctrlRollFactor); }
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
        if (playerHasControl)
        {
            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x + (Time.deltaTime * xOffSet), xNRange, xPRange)
                , Mathf.Clamp(transform.localPosition.y + (Time.deltaTime * yOffset), yNRange, yPRange)
                , transform.localPosition.z);
        }
    }

    private void OnEnable() //really messy, but working the way I wanted
    {
        controls.PlayerControls.Horizontal.performed += Movement;
        controls.PlayerControls.Horizontal.Enable();

        controls.PlayerControls.Shooting.started += Shooting;
        controls.PlayerControls.Shooting.Enable();

        controls.PlayerControls.RapidFire.performed += RapidFire;
        controls.PlayerControls.RapidFire.canceled += RapidFire;
        controls.PlayerControls.RapidFire.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerControls.Horizontal.performed -= Movement;
        controls.PlayerControls.Horizontal.Disable();

        controls.PlayerControls.Shooting.started -= Shooting;
        controls.PlayerControls.Shooting.Disable();

        controls.PlayerControls.RapidFire.performed -= RapidFire;
        controls.PlayerControls.RapidFire.canceled -= RapidFire;
        controls.PlayerControls.RapidFire.Disable();
    }

    void Movement(InputAction.CallbackContext context)//gets input values 
    {
        //Debug.Log("Axis" + direction);
        direction = context.ReadValue<Vector2>();
        xOffSet = xSpeed * direction.x;
        yOffset = ySpeed * direction.y;
    }

    private void Shooting(InputAction.CallbackContext context)
    {
        shootInput = context.ReadValue<float>();
        if (shootInput > .1f)
        {
            int iteration = 0;
            foreach (GameObject bul in bullets)
            {
                GameObject firedBullet = Instantiate(bul, parent[iteration].position, parent[iteration].rotation);
                firedBullet.transform.parent = parent[iteration];
                iteration++;
            }
        }
    }

    void RapidFire(InputAction.CallbackContext context)
    {
        
        float rfInput = context.ReadValue<float>();
        if (rfInput > 0.5f)
        {
            
            foreach (GameObject bul in rfBullets)
            {
                bul.GetComponent<ParticleSystem>().Play();
            }
        } else
        {
            foreach (GameObject bul in rfBullets)
            {
                bul.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    private void PlayerDeath() // called by string 
    {
        playerHasControl = false;
        Invoke("DisableShipMovement", 1f);
    }

    private void DisableShipMovement() //called by string
    {
        ableToMove = false;
    }
}
