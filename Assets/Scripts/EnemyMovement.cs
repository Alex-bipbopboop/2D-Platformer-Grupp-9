using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float bounciness = 100f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private AudioClip enemyDestroySoundEffect;

    //Knockback
    [SerializeField] private float knockbackForce = 100f;
    [SerializeField] private float upwardForce = 5f;
    private SpriteRenderer rend;
    private AudioSource audioSource;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (moveSpeed < 0)
        {
            rend.flipX = true;
        }
        if (moveSpeed > 0)
        {
            rend.flipX = false;
        }
    }


    void FixedUpdate()
    {
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock") || other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);

            if(other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(enemyDestroySoundEffect);

            Rigidbody2D rgbd = other.attachedRigidbody;

            if (rgbd != null)
            {
                rgbd.linearVelocity = new Vector2(rgbd.linearVelocityX, 0);
                rgbd.AddForce(new Vector2(0, bounciness));
            }
            
            Destroy(gameObject);
        }
    }
}
