using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float MoveX;
    private float MoveY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetInput();
        anim.SetBool("Moving", MoveX != 0 || MoveY != 0);

        // Send movement to the Blend Tree
        anim.SetFloat("X", MoveX);
        anim.SetFloat("Y", MoveY);

        // Flip sprite when moving left/right
        if (MoveX > 0)
        {
            sr.flipX = false;
        }
        else if (MoveX < 0)
        {
            sr.flipX = true;
        }
        if (MoveX != 0 || MoveY != 0)
        {
            anim.SetFloat("LastMoveX", MoveX);
            anim.SetFloat("LastMoveY", MoveY);
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }

        Vector2 movement = new Vector2(MoveX, MoveY).normalized;
        rb.linearVelocity = movement * currentSpeed;
    }

    private void GetInput()
    {
        MoveX = Input.GetAxisRaw("Horizontal");
        MoveY = Input.GetAxisRaw("Vertical");
    }
}
