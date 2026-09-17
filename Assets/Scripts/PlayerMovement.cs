using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    private float moveDirection;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float walkSoundTimerTime = 0.4f;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float raycastDistance = 0.25f;
    [SerializeField] private LayerMask whatIsIce;               //samma funktion som whatIsGround
    [SerializeField] private float groundAcceleration = 6f;    //Acceleration tillagt för att kunna ändra på hastigheten som spelaren startar och stannar
    [SerializeField] private float iceAcceleration = 60f;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private ParticleSystem jumpParticleSystem;
    bool canMove = true;
    bool walkSoundTimer = false;
    private float playerPositionX;
    private float newPlayerPositionX;

    private AudioSource audioSource;
    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
        playerPositionX = transform.position.x;

        jump.action.started += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        newPlayerPositionX = transform.position.x;

        moveDirection = move.action.ReadValue<float>();

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.linearVelocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.linearVelocity.y);
        anim.SetBool("IsGrounded", CheckIsGrounded());

        if (moveDirection < 0f)
        {
            FlipSprite(true);

        }

        if (moveDirection > 0f)
        {
            FlipSprite(false);
        }

        while (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerRun") && !walkSoundTimer)
        {
            StartCoroutine(WalkSoundTimer());
        }
        playerPositionX = newPlayerPositionX;
    }

    private void FixedUpdate()
    {
        if(!canMove)
        {
            return;
        }
        //rgbd.linearVelocity = new Vector2(moveDirection * moveSpeed * Time.deltaTime, rgbd.linearVelocity.y);

        float targetSpeedX = moveDirection * moveSpeed;
        float accel = CheckIsOnIce() ? iceAcceleration : groundAcceleration; //if CheckIsOnIce is true accel uses iceAcceleration else uses groundAcceleration

        float newVelocityX = Mathf.MoveTowards(rgbd.linearVelocity.x, targetSpeedX, accel * Time.fixedDeltaTime);
        rgbd.linearVelocity = new Vector2(newVelocityX, rgbd.linearVelocity.y);

    }

    private void OnDisable()
    {
        jump.action.started -= Jump;
    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (CheckIsGrounded() == true || CheckIsOnIce() == true)
        {
            rgbd.AddForce(new Vector2(0, jumpForce));
            jumpParticleSystem.Play();
            int randomJumpSound = Random.Range(0, jumpSounds.Length);
            audioSource.PlayOneShot(jumpSounds[randomJumpSound]);
        }
    }

    private bool CheckIsGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, raycastDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, raycastDistance, whatIsGround);

        if (leftHit.collider != null && leftHit || rightHit.collider != null && rightHit)
        {
            return true;
        }
        else
        {
            return false;
        }
     
    }

    private bool CheckIsOnIce() //samma verktyg som CheckIsGrounded används för att detectera is
    { 
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, raycastDistance, whatIsIce);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, raycastDistance, whatIsIce);
        
        if (leftHit.collider != null && leftHit || rightHit.collider != null && rightHit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeKnockback(float backForce, float upwardsForce)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(backForce, upwardsForce));
        Invoke(nameof(CanMoveAgain), 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private IEnumerator WalkSoundTimer()
    {
        walkSoundTimer = true;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(walkSound);
        yield return new WaitForSeconds(walkSoundTimerTime);
        walkSoundTimer = false;
    }

}