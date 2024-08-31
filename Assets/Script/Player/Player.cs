using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpForce, rotateSpeed;
    [SerializeField] SO_Stat stat;
    [SerializeField] HealthBar healthBar;


    Animator anim;
    Rigidbody rb;
    int inputX, inputZ;
    bool isOnGround;


    /* Basic methods ***********************************************************/
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        stat.PublicResetHealth();
    }
    private void Update()
    {
        Rotate();
    }
    private void LateUpdate()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
    }


    /* Other handlers ***********************************************************/
    private void AnimationLanding()
    {
        anim.SetTrigger("land");
    }


    /* Movement handlers ********************************************************/
    private void Move()
    {
        Vector3 xDir = Vector3.right * moveSpeed * inputX;
        Vector3 yDir = Vector3.up * rb.velocity.y;
        Vector3 zDir = Vector3.forward * moveSpeed * inputZ;
        
        rb.velocity = xDir + yDir + zDir;

        Vector2 inputAxis = new Vector2(inputX, inputZ);    
        bool isMoving = (inputAxis  != Vector2.zero);
        anim.SetBool("isMoving", isMoving);
    }
    private void Jump()
    {
        anim.ResetTrigger("land");  // Preventing trigger set before jump, causing animation to swap immediately when hit jump key
        anim.SetTrigger("jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void Rotate()
    {
        Vector3 inputAxis = new Vector3(inputX, 0, inputZ);
        if (inputAxis != Vector3.zero)
        {
            Quaternion rotateDir = Quaternion.LookRotation(inputAxis, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDir, rotateSpeed * Time.deltaTime);
        }
    }


    /* Input handlers *************************************************************/
    private void OnMove(InputValue a)
    { 
        inputZ = Mathf.RoundToInt(a.Get<Vector2>().y);
        inputX = Mathf.RoundToInt(a.Get<Vector2>().x);
    }
    private void OnJump()
    {
        if(isOnGround)
            Jump();
    }

    /* Public methods *************************************************************/
    public void PublicAdjustHealth(int value)
    {
        stat.health += value;
        healthBar.PublicAdjustHealth(value);
    }
    public void PublicSetIsGrounded(bool value)
    {
        isOnGround = value;
    }
    public void PublicTriggerLandingAnimation()
    {
        AnimationLanding();
    }
}