using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isDead = false;
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
    public float maxInsanity = 12;
    public float insanityStep = 0.004f;
    public CircleCollider2D viewCollider;
    public GameObject messageBox;
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
        if (!isDead)
        {
            UpdateGroundCheck();
            UpdateAnimation();
            UpdateMaskSize();

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
        else
        {

            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
    }

    private void UpdateGroundCheck()
    {
        Collider2D[] deathColliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, whatIsDeath);
        for (int i = 0; i < deathColliders.Length; i++)
        {
            isDead = true;
            animator.SetBool("isDead", isDead);
            return;
        }

        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, whatIsGround);
        if (groundColliders.Length <= 0)
        {
            isGrounded = false;
            return;
        }
        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
    }

    private void UpdateAnimation()
    {
        animator.SetBool("isMoving", rb2d.velocity.x != 0);
        animator.SetBool("isJumping", !isGrounded);
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

    public void Die()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
    }

    private void Flip()
    {
        isLookingRight = !isLookingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void DamageEvent()
    {
        GameObject soundPlayer = GameObject.FindGameObjectWithTag("FX_Music");
        soundPlayer.GetComponent<SoundList>().CallSoundByName("damage");
    }

    public void DieEvent()
    {
        messageBox.SetActive(true);
    }
}
