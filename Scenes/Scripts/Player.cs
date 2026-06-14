using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Varibles
    [SerializeField] InputActionReference Jump;
    Rigidbody2D rb2d;
    GameObject[] bounce;
    Animator animator;

    [SerializeField] float speed;
    [SerializeField] float Jumpforce;
    [SerializeField] float Flyingforce;
    [SerializeField] GameObject RestartScene;
    [SerializeField] float flyingGravity;
    [SerializeField] float Displace;
    [SerializeField] Transform Sprite;
    public float boostXforce;
    public float boostYforce;
    Vector2 boostforce;

    bool isjumping = false;
    bool isGrounded = true;
    bool Booost = false;
    bool alive = true;
    public bool isflying = false;
    public bool Flyingup = false;
    public bool isSpider = false;
    bool Flipped = false;
    float Gravityoriginal;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Jump.action.Enable();
        boostforce = new Vector2(boostXforce, boostYforce);
        bounce = GameObject.FindGameObjectsWithTag("bounce");
        animator = Sprite.GetComponent<Animator>();
        Gravityoriginal = rb2d.gravityScale;


    }

    // Update is called once per frame
    void Update()
    {
        if (alive == true)
        {
            if (isflying == true)
            {
                animator.SetBool("Jumping", false);
                HandleFlying();
            }
            else if (isSpider == true)
            {
                animator.SetBool("Jumping", false);

                if (Jump.action.WasPressedThisFrame() && Flipped == false)
                {
                    Spiderflip();
                    Flipped = true;
                }

            }
            else
            {
                rb2d.gravityScale = Gravityoriginal;


            }

            if (Jump.action.triggered && isGrounded == true && isflying == false && isSpider == false)
            {
                isjumping = true;
                animator.SetBool("Jumping", true);
                Debug.Log("Animator Jumping is true" );


            }
            else if (Jump.action.triggered && isGrounded == false && isflying == false && isSpider == false)
            {
                Booost = true;
                Debug.Log("Animator Jumping is true");

            }
        }
        else 
        { 
            gameObject.SetActive(false);
        }


    }
    void FixedUpdate()
    {

        rb2d.linearVelocity = new Vector3(speed, rb2d.linearVelocity.y);

        if (isjumping == true)
        {
            Jumping();

        }
        if (Flyingup == true)
        {
            FlyingUpLogic();
        }
        else if (Flyingup == false && isflying == true)
        {
            rb2d.linearVelocityY = flyingGravity;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
            Booost = false;
            Flipped = false;
            animator.SetBool("Jumping", false);

        }
        else if (collision.gameObject.layer == 6)
        {
            alive = false;
            RestartScene.SetActive(true);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up * Mathf.Sign(rb2d.gravityScale) * 10f);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("ExtraJump") && Booost == true)
        {
            ExtraJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GravityShift"))
        {
            GravityShift();
        }

    }



    public void GravityShift()
    {
        Gravityoriginal *= -1;
        rb2d.gravityScale = Gravityoriginal;

        BouncePad bouncescript;
        foreach (GameObject bouncepad in bounce)
        {
            bouncescript = bouncepad.GetComponent<BouncePad>();
            bouncescript.gravityswitch = true;
        }
    }
    public void ExtraJump()
    {
        rb2d.linearVelocity = new Vector3(rb2d.linearVelocityX, 0);
        rb2d.AddForce(boostforce, ForceMode2D.Impulse);

        Booost = false;
    }
    public void Jumping()
    {
        float gravityDirection = Mathf.Sign(rb2d.gravityScale);

        
        rb2d.linearVelocity = new Vector3(rb2d.linearVelocityX, 0);
        rb2d.AddForceY(gravityDirection * Jumpforce, ForceMode2D.Impulse);


        boostforce = new Vector2(boostXforce, gravityDirection * boostYforce);

        isGrounded = false;
        isjumping = false;
    }
    public void HandleFlying()
    {
        if (Jump.action.IsPressed())
        {
            Debug.Log("Flying up");
            Flyingup = true;
        }
        else if (Jump.action.WasReleasedThisFrame())
        {
            rb2d.gravityScale = 0;
            Debug.Log("Flying down");
            Flyingup = false;


        }
    }
    public void FlyingUpLogic()
    {
        rb2d.linearVelocityY = 0;
        rb2d.gravityScale = 0;
        rb2d.AddForceY(Flyingforce, ForceMode2D.Force);
    }
    public void Spiderflip()
    {
        RaycastHit2D hit = Physics2D.Raycast(
                            transform.position, 
                            Vector2.up * Mathf.Sign(rb2d.gravityScale),
                            10f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            rb2d.gravityScale *= -1;

            transform.position = new Vector3(
                transform.position.x,
                hit.point.y + Displace * Mathf.Sign(rb2d.gravityScale),
                transform.position.z
            );
        }
    }





}
