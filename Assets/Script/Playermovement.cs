using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed; // Játékos sebessége
    [SerializeField] private float jumpForce; // Ugrási erő
    private Rigidbody2D body; // Játékos Rigidbody komponense
    private Animator anim; // Játékos Animator komponense
    private bool isJumping = false; // Jelzi, hogy a játékos éppen ugrásban van-e
    private bool isFalling = false; // Jelzi, hogy a játékos éppen zuhan-e
    private Rigidbody2D rb; // Játékos Rigidbody komponense
    bool facingRight = true; // Jelzi, hogy a játékos jobbra néz-e

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 200f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLAyer;
    [SerializeField] private TrailRenderer tr;

    private bool hasJumped = false; // Jelzi, hogy a játékos már ugrált-e

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); // Rigidbody komponens inicializálása
        anim = GetComponent<Animator>(); // Animator komponens inicializálása
        rb = GetComponent<Rigidbody2D>(); // Rigidbody komponens inicializálása
    }

    private void Update()
    {
        // Játékos vízszintes sebességének beállítása az Input alapján
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        // Ugrás vezérlése
        if (Input.GetKey(KeyCode.W) && Mathf.Abs(body.velocity.y) < 0.01f)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            anim.SetBool("Jump", true); // Ugrás animáció bekapcsolása
        }
        else
        {
            anim.SetBool("Jump", false); // Ugrás animáció kikapcsolása
        }

        // Leesés vezérlése
        if (body.velocity.y < 0 && !isFalling)
        {
            isFalling = true;
            anim.SetBool("Fall", true); // Zuhanás animáció bekapcsolása
        }
        else if (body.velocity.y >= 0 && isFalling)
        {
            isFalling = false;
            anim.SetBool("Fall", false); // Zuhanás animáció kikapcsolása
        }

        // Játékos irányváltása
        if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            flip();
        }
        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            flip();
        }

        // Futás animáció vezérlése
        anim.SetBool("RUN", Input.GetAxis("Horizontal") != 0);

        // Játékos ugrása
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !hasJumped)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true; // Ugrás állapotának bekapcsolása
            hasJumped = true; // Ugrás történt
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (isDashing)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    // Földre érkezéskor végrehajtott műveletek
    public void OnLanding()
    {
        anim.SetBool("Jump", false); // Ugrás animáció kikapcsolása
        anim.SetBool("Fall", false); // Zuhanás animáció kikapcsolása
        isJumping = false; // Ugrás állapotának kikapcsolása
        hasJumped = false; // Az ugrás történt visszaállítása
    }

    // Játékos irányváltása
    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // Az irány beállítása a játékos jelenlegi irányának megfelelően
        float dashDirection = facingRight ? 1f : -1f;

        rb.velocity = new Vector2(dashDirection * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}