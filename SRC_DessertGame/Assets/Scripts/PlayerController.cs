using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Input system
    public PlayerInput playerInputAction;
    private Vector2 rawInputMovement;

    private Animator anim;
    private Vector3 movement;


    // Player movement
    [Header("Movement Settings")]
    public Rigidbody playerRigidbody;

    public float movementX;
    public float movementY;

    [SerializeField] private float movementSpeed = 0.0f;
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 20.0f;
    //[SerializeField] private float turnSpeed = 0.1f;

    public bool IsRunning = false;

    //Vector2 dir = new Vector2();
    //private Vector3 movementDirection;
    //public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        //playerRigidbody.AddForce(movement * movementSpeed);

        Move();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMovement(InputAction.CallbackContext value) //(InputValue value)//
    {
        Vector2 movementVec = value.ReadValue<Vector2>();//Get<Vector2>();
        //movementX = movementVec.x;
        //movementY = movementVec.y;


        if (Vector2.zero != movementVec && !IsRunning)
        {
            Walk(movementVec);
            //movementX = movementVec.x;
            //movementY = movementVec.y;
            //movementSpeed = walkSpeed;
            //anim.SetFloat("Speed", 0.5f);
        }
        else if (Vector2.zero != movementVec && IsRunning)
        {
            Run(movementVec);
            //movementX = movementVec.x;
            //movementY = movementVec.y;
            //movementSpeed = runSpeed;
            //anim.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
        }
        else if (Vector2.zero == movementVec)
        {
            anim.SetFloat("Speed", 0);
        }
    }
    public void Move()
    {
        movement = new Vector3(movementX, 0.0f, movementY);
        playerRigidbody.AddForce(movement * movementSpeed);
    }
    public void Walk(Vector2 movementVec)
    {
        movementX = movementVec.x;
        movementY = movementVec.y;
        //rawInputMovement = new Vector3(movementVec.x, 0, movementVec.y);
        movementSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f);
    }

    public void Run(Vector2 movementVec)
    {
        movementX = movementVec.x;
        movementY = movementVec.y;
        //rawInputMovement = new Vector3(movementVec.x, 0, movementVec.y);
        movementSpeed = runSpeed;
        anim.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
    }
    public void Idle()
    {
        anim.SetFloat("Speed", 0);
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
    //public void OnDash(InputAction.CallbackContext value)
    //{
    //    if (value.started)
    //    {
    //        Debug.Log("Dash button!!");
    //    }
    //}
}
