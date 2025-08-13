using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Playables;

public class PlayController : MonoBehaviour
{
    #region nessessary component
    private Rigidbody2D rb;
    public AudioSource m_AudioSource;
    public BoxCollider2D collider;
    #endregion

    #region 
    int moveDirection;
    int left = -1;
    int right = 1;
    #endregion


    public float moveSpeed;
    public float jumpImpact;

    [SerializeField]
    private int jumpIndex = 2;

    #region player input
    private bool jumpInput;
    private bool moveLeft;
    private bool moveRight;
    #endregion

    [SerializeField]
    private bool isGrounded = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb            = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        collider      = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGrounded = true;
            jumpIndex = 2;
        }
    }

    void handleInput()
    {
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        moveLeft = Input.GetKey(KeyCode.LeftArrow);
        moveRight = Input.GetKey(KeyCode.RightArrow);


        // rewriite this via transition to state
        if(moveLeft && !moveRight) moveDirection = left;
        else if(!moveLeft && moveRight) moveDirection = right;
        else moveDirection = 0;
    }

    void handleMovement()
    {
        rb.linearVelocityX = moveDirection * moveSpeed;
        
        if(jumpInput)
        {
            if (jumpIndex > 0)
            {
                rb.linearVelocityY = jumpImpact;
                jumpIndex--;
            }
        }
    }


    void handleSoundEffect()
    {

    }
    // Update is called once per frame
    void Update()
    {
        handleInput();

        handleMovement();

        handleSoundEffect();
    }
}
