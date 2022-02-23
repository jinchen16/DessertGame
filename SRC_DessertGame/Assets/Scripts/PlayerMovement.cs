using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    //Input system
    public PlayerAction playerInputAction;
    private Vector2 movementInput;

    //Animation
    private Animator anim;

    [SerializeField] private float movementSpeed = 0.0f;
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private bool IsRunning = false;
    [SerializeField] private bool IsWalking = false;
    [SerializeField] private bool IsMoving = false;


    private Vector3 inputDir;
    private Vector3 moveVec;
    private Quaternion currentRotation;

    //Test
    private float speed = 0.0f;
    private float accelation = 0.0f;
    private float deccelation = 0.5f;
    private int speedMesh;

    private void Awake()
    {
        playerInputAction = new PlayerAction();
        playerInputAction.PlayerControls.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        anim = GetComponent<Animator>();

        //Test
        speedMesh = Animator.StringToHash("Speed");

    }

    private void FixedUpdate()
    {
        float h = movementInput.x;
        float v = movementInput.y;

        Vector3 targetInput = new Vector3(h, 0, v);

        inputDir = Vector3.Lerp(inputDir, targetInput, Time.deltaTime * 10f);

        //Camera..
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;

        // The calculation of direction the player wants to head
        Vector3 desireDir = camForward * inputDir.z + camRight * inputDir.x;


        Move(desireDir);
        Turn(desireDir);


    }

    void Move(Vector3 desireDirection)
    {
        if (desireDirection != Vector3.zero && !IsRunning)
        {
            Walk(desireDirection);
        }
        else if (desireDirection != Vector3.zero && IsRunning)
        {
            Run(desireDirection);
        }
        else if (desireDirection == Vector3.zero && !IsRunning)
        {
            Idle();
        }

        moveVec.Set(desireDirection.x, 0f, desireDirection.z);
        moveVec = moveVec * movementSpeed * Time.deltaTime;
        transform.position += moveVec;

    }

    void Turn(Vector3 desireDirection)
    {
        // 
        if ((desireDirection.x > 0.1 || desireDirection.x < -0.1 )|| (desireDirection.z > 0.1 || desireDirection.z <-0.1))
        {
            currentRotation = Quaternion.LookRotation(desireDirection);
            transform.rotation = currentRotation;

        }
        else
        {
            transform.rotation = currentRotation;
        }
    }
    void Idle()
    {
        anim.SetFloat("Speed", 0);
        IsWalking = false;
        IsRunning = false;
    }
    void Walk(Vector3 desireDirection)
    {
        IsWalking = true;
        movementSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f);

    }

    void Run(Vector3 desireDirection)
    {

        movementSpeed = runSpeed;
        anim.SetFloat("Speed", 1.0f);

    }
    public void OnMove(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            IsMoving = true;
            Debug.Log("Move Move Move");
        }
        else
        {
            IsMoving = false;
            Debug.Log("Stop.....");
        }
    }
    public void OnRun(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            Debug.Log("Running");
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }
    }

    // Enable and Disable Input Ation
    private void OnEnable()
    {
        playerInputAction.Enable();

    }
    private void OnDisable()
    {
        playerInputAction.Disable();
    }
}
