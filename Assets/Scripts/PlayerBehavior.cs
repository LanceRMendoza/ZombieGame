using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject cam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform modelContainer;
    [SerializeField] private Text debugText;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Animator bodyAnimator;

    [Header("Designer Variables")]

    // Adds a slider on the range.
    [SerializeField] [Range(10,800)]private int walkSpeed;
    [SerializeField] [Range(10,800)]private int airSpeed;
    [SerializeField] [Range(0,1)]private float groundDrag;
    [SerializeField] [Range(0,1)]private float airDrag;
    [SerializeField] [Range(0,100)] private float jumpImpulse = 35;
    [SerializeField] [Range(0,100)] private float jumpForce = 7;

    private float CheckForGroundDistance = 0.5f;

    private bool onGround = false;

    // Input Variables
    PlayerInput inputs;
    private Vector2 movementInput;
    private float xVel;
    private float zVel;
    private bool jumpHold = false;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 airVelocity = Vector3.zero;

    bool isWalking = true;

    private float forwardAngle;
    private float faceAngle;

    // Audio
    public AudioSource audioPlayer;
    public AudioSource audioPlayer2;

    private void Awake(){
        inputs = new PlayerInput();
        // Taking the .Move PlayerInput, turning into a vector 2. ctx = context.
        // inputs. = Our input object, PlayerControls = Action mapping, Move = Action, performed = callback,
            // callbacks an even you can subscribe to += adds code that fires when the callback is triggered.
            // lambda operator => member pointing to an expression.
        inputs.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();


        //lerp = Linear interpolation, start + (b - a) * t
    }

    

    // Update is called once per frame
    void Update()
    {
        playJumpSound();

        // Collect Input
        xVel = movementInput.x;
        zVel = movementInput.y;
        //print($"Movement Input: {movementInput}");

            if (inputs.PlayerControls.Jump.WasPressedThisFrame()){
                JumpStart();
            }
            if (jumpHold && inputs.PlayerControls.Jump.WasReleasedThisFrame()){
                jumpHold = false;
                
            }
        if (jumpHold && inputs.PlayerControls.Jump.WasReleasedThisFrame()){
        jumpHold = false;
        }
    }

    private void FixedUpdate() {
        onGround = CheckForGround();

        if (onGround){
            isWalking = movementInput != Vector2.zero;
        }


        // Movement
        Vector3 cameraForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up);
        moveDirection = Vector3.Normalize(cameraForward * zVel + cam.transform.right * xVel);
        moveDirection *= new Vector2(xVel, zVel).magnitude;
        //moveDirection = new Vector3(xVel, 0 ,zVel);

        // On the Ground
        if (onGround){
            // Velocity Dampening
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(groundDrag, 1, groundDrag));

            if (isWalking){
            // Actual Move
            rb.AddForce(moveDirection * walkSpeed);

            // Actual Turning
            // Ternary Opeartor: Statement ? true value : false value
            forwardAngle = cameraForward.x > 0 ? Vector3.Angle(Vector3.forward, cameraForward) :  -Vector3.Angle(Vector3.forward, cameraForward);
            faceAngle = xVel > 0 ? Vector3.Angle(cameraForward,moveDirection) : -Vector3.Angle(cameraForward, moveDirection);

        // Actual Rotate
        modelContainer.localRotation = Quaternion.Euler(0, forwardAngle + faceAngle, 0);

        // Update Debug Canvas
        debugText.text = $"Pacing Angle: {Mathf.Round(forwardAngle)}\n" + $"Stick Angle: {Mathf.Round(faceAngle)}";
            }
        }

        // In the Air
        else{
            // Velocity Dampening
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(airDrag, 1, airDrag));

            if (jumpHold) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
            }
            
            // Velocity at launch
            airVelocity *= 0.95f;
            rb.AddForce(airVelocity);
                
            // Actual Move
            rb.AddForce(moveDirection * airSpeed);
        }

    }

    private void JumpStart(){
        if (onGround){
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            onGround = false;
            bodyAnimator.SetTrigger("Jump Trigger");
            jumpHold = true;
            airVelocity = moveDirection * walkSpeed;
        }
    }

    private RaycastHit CheckBelow(){
        RaycastHit hit;
        Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, out hit, 20, groundLayer, QueryTriggerInteraction.Ignore);
        return hit;
    }

    private bool CheckForGround(){
        RaycastHit rayTarget = CheckBelow();
        // Debug.DrawLine(transform.position + Vector3.up * 0.05f, transform.position + Vector3.down * CheckForGroundDistance, Color.cyan);
        if (rayTarget.distance < CheckForGroundDistance){
            return true;
        }
        return false;
    }

    private void OnEnable(){
        inputs.Enable();
    }

    private void OnDisable(){
        inputs.Disable();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            audioPlayer.Play();
    }
    public void playJumpSound(){
        if (Input.GetKeyDown(KeyCode.Space)){
            audioPlayer2.Play();
            
        }
    }
}

