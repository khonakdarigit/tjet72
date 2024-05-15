using System;
using UnityEngine;

public class Sito : Assets.Script.Singleton<Sito>
{
    private bool isGrounded;

    [SerializeField] Transform feetPos;
    [SerializeField] float CheckRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] int JumpForce;

    private bool facingRight = true;

    private Rigidbody2D rb;
    private Animator anima;

    [SerializeField] VariableJoystick variableJoystick;
    [SerializeField] float speed = 15;

    private float moveInput;

    internal void Run(bool value)
    {
        anima.SetBool("Run", value);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        JumAnimation();

    }

    float lastY = 0;

    bool JumpToDownRunning = false;

    private void JumAnimation()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, CheckRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //Debug.Log($"isGrounded : {isGrounded} IsJumping : {IsJumping} JumpToDownRunning : {JumpToDownRunning} lastY : {lastY} rb.velocity.y : {rb.velocity.y}");



        if (IsJumping && !JumpToDownRunning && lastY > transform.position.y)
        {

            anima.SetTrigger("JumpToDown");
            JumpToDownRunning = true;


        }

        if (IsJumping && isGrounded && JumpToDownRunning)
        {
            IsJumping = false;
            JumpToDownRunning = false;
            anima.SetTrigger("JumpLanding");
        }
        lastY = transform.position.y;
    }

    void LateUpdate()
    {
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        if (moveInput == 0)
        {
            Run(false);
        }
        else
        {
            Run(true);
        }
    }

    private void FixedUpdate()
    {
        moveInput =
     variableJoystick.Horizontal != 0 ?
     (variableJoystick.Horizontal > 0 ? 1 : -1) :
     Input.GetAxis("Horizontal");

        //var direction = Vector3.right * variableJoystick.Horizontal;

        //Debug.Log($"TargetX :{moveInput * speed} ");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        // transform.position += Vector3.right * moveInput * speed * Time.deltaTime;
        //transform.Translate(Vector3.right * moveInput * speed * Time.deltaTime);
    }

    void Flip()
    {
        Debug.Log($"facingRight : {facingRight} transform.localScale.X {transform.localScale.x}");
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;

        Scaler.Set(Scaler.x * -1f, Scaler.y, Scaler.z);
        rb.transform.localScale = Scaler;


        //if (moveInput > 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}
        //else if (moveInput < 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //}
    }

    bool IsJumping = false;

    internal void Jump()
    {

        isGrounded = Physics2D.OverlapCircle(feetPos.position, CheckRadius, whatIsGround);

        if (isGrounded)
        {
            IsJumping = true;

            rb.velocity = Vector2.up * (JumpForce /*+ Add*/);
            //Play(Audio_Jump);
            anima.SetTrigger("Jump");
        }

    }
}
