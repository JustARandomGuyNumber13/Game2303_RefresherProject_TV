using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpForce;
    [SerializeField] private GameObject groundCheckObject;


    Animator anim;
    Rigidbody rb;
    int inputX, inputZ;
    bool isOnGround, isFlying;


    /* Basic methods ***********************************************************/
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
    }
    private void LateUpdate()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
        GroundCheck();
        AnimationLanding();
    }


    /* Other handlers ***********************************************************/
    private void AnimationLanding()
    {
        if (isFlying && isOnGround && rb.velocity.y < 0)    // velocity.y == 0 will cause a bug where player will lands at the same time as jumped
        {
            isFlying = false;
            anim.SetTrigger("land");
        }
    }
    private void GroundCheck()
    {
        Vector3 center = groundCheckObject.transform.position;
        Vector3 size = groundCheckObject.transform.localScale;

        Collider[] hitColliders = Physics.OverlapBox(center, size / 2, Quaternion.identity, LayerMask.GetMask("ground"));
        isOnGround = hitColliders.Length > 0;
    }


    /* Movement handlers ********************************************************/
    private void Move()
    {
        anim.SetInteger("inputX", inputX);
        anim.SetInteger("inputZ", inputZ);

        Vector3 xDir = transform.right * moveSpeed * inputX;
        Vector3 yDir = transform.up * rb.velocity.y;
        Vector3 zDir = transform.forward * moveSpeed * inputZ;

        rb.velocity = xDir + yDir + zDir;
    }
    private void Jump()
    {
        isFlying = true;
        anim.SetTrigger("jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
}