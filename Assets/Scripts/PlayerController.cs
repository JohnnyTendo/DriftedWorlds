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
    public bool isGrounded;
    int hor = 0;
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
    public GameObject[] masks;
    public bool raiseInsanity = false;
    public float insanity = 0;
    public float maxInsanity = 10;
    public float insanityStep = 0.0001f;
    #endregion
    #region Interaction

    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateMaskSize();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, whatIsGround);

        if (Input.GetKeyDown(KeyCode.R) && isGrounded)
        {
            raiseInsanity = true;
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hor = 1;
            rb2d.velocity = new Vector2(hor * maxMoveSpeed, rb2d.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            hor = -1;
            rb2d.velocity = new Vector2(hor * maxMoveSpeed, rb2d.velocity.y);
        }
    }

    private void UpdateMaskSize()
    {
        foreach (GameObject mask in masks)
        {
            Transform transform = mask.GetComponent<Transform>();
            if (raiseInsanity)
            {
                insanity = transform.localScale.x + insanityStep;
                transform.localScale = new Vector3(insanity, insanity, transform.localScale.z);
                raiseInsanity = insanity < maxInsanity;
            }
            if (!raiseInsanity && insanity > 0)
            {
                insanity = transform.localScale.x - insanityStep;
                transform.localScale = new Vector3(insanity, insanity, transform.localScale.z);
            }
        }
    }
}
