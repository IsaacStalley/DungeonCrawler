using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int speed = 5;
    private float jumpHeight = 20;
    private float horizontalDirection;
    private Animator animator;
    public bool facingRight = false;

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
                print("Null");
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        PlayerMovement(horizontalDirection);
        WalkingAnimation(horizontalDirection);
    }

    private void PlayerMovement(float horizontalDirection)
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft(horizontalDirection);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight(horizontalDirection);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void WalkingAnimation(float horizontalDirection)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
    }

    void MoveLeft(float horizontalDirection)
    {
        transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
        if (horizontalDirection < 0 && facingRight)
            FlipPlayer();
    }

    void MoveRight(float horizontalDirection)
    {
        transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
        if (horizontalDirection > 0 && !facingRight)
            FlipPlayer();
    }
    
    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
