using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region Singleton

    public static PlayerController instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    #region PrivateSteeringVars
    Rigidbody2D rb2d;
    [SerializeField] private bool isLookingRight = true;
    [SerializeField] private bool isGrounded = true;
    private Animator animator;
    public float maxMoveSpeed;
    public float jumpSpeed;
    #endregion
    #region GroundDetection
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsDeath;
    public LayerMask whatIsClimbable;
    #endregion
    #region Insanity
    public GameObject mask;
    public bool raiseInsanity = false;
    public float insanity = 0;
    public float maxInsanity = 10;
    public float insanityStep = 0.0001f;
    public CircleCollider2D viewCollider;
    #endregion
    #region Interaction

    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        viewCollider = mask.GetComponent<CircleCollider2D>();
        viewCollider.enabled = false;
    }

    void Update()
    {
        UpdateAnimation();
        UpdateMaskSize();
        /*
        if (Input.GetKeyDown(KeyCode.R) && isGrounded)
        {
            viewCollider.enabled = true;
            raiseInsanity = true;
        }
        */
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!isLookingRight)
            {
                Flip();
            }
            rb2d.velocity = new Vector2(maxMoveSpeed, rb2d.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isLookingRight)
            {
                Flip();
            }
            rb2d.velocity = new Vector2(-maxMoveSpeed, rb2d.velocity.y);
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isMoving", rb2d.velocity.x != 0);
        animator.SetBool("isDead", false);
        animator.SetBool("isTakingDamage", false);
    }

    private void UpdateMaskSize()
    {
        Transform transform = mask.GetComponent<Transform>();
        if (raiseInsanity)
        {
            insanity = transform.localScale.x + insanityStep;
            transform.localScale = new Vector3(insanity, insanity, transform.localScale.z);
            raiseInsanity = insanity < maxInsanity;
        }
        else if (!raiseInsanity && insanity > 0)
        {
            insanity = transform.localScale.x - insanityStep;
            transform.localScale = new Vector3(insanity, insanity, transform.localScale.z);
        }
        else
        {
            viewCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("is Grounded");
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    private void Flip()
    {
        isLookingRight = !isLookingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
