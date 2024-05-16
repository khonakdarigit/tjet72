using System;
using UnityEngine;

public class Sito : Assets.Script.Singleton<Sito>
{
    bool IsJumping = false;
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
    private float LastY;
    private bool _statCheckJumpAnimation;

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
        isGrounded = Physics2D.OverlapCircle(feetPos.position, CheckRadius, whatIsGround);
        JumAnimation();

    }



    private void JumAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (_statCheckJumpAnimation)
        {
            if (!isGrounded && LastY > transform.position.y)
            {
                //Debug.Log($"isGrounded : {isGrounded} >> JumpToDown");

                anima.SetTrigger("JumpToDown");

            }

        }
        if (isGrounded)
        {
            //Debug.Log($"isGrounded : {isGrounded} >> JumpLanding");

            anima.SetTrigger("JumpLanding");
            _statCheckJumpAnimation = false;
            anima.SetBool("OnGround", true);

        }

        LastY = transform.position.y;


        //if (isGrounded && _statCheckJumpAnimation)
        //{
        //    _statCheckJumpAnimation = false;
        //    LastY = 0;
        //}

        //if (_statCheckJumpAnimation)
        //{


        //}





    }

    public void StatCheckJumpAnimation()
    {
        if (!isGrounded)
        {
            //Debug.Log($"isGrounded : {isGrounded} >> StatCheckJumpAnimation");

            _statCheckJumpAnimation = true;
            anima.SetBool("OnGround", false);

        }
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
            if (!IsJumping)
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
        //Debug.Log($"facingRight : {facingRight} transform.localScale.X {transform.localScale.x}");
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


    internal void Jump()
    {

        isGrounded = Physics2D.OverlapCircle(feetPos.position, CheckRadius, whatIsGround);

        if (isGrounded)
        {
            rb.velocity = Vector2.up * (JumpForce /*+ Add*/);
            //Play(Audio_Jump);
            anima.SetTrigger("Jump");
        }

    }
}
