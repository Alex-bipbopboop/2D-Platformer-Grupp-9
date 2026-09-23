using UnityEngine;

public class EnemyMovementFlying : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float bounciness = 100f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private GameObject enemyPoof;
    private GameObject waypoint;
    [SerializeField] private float distanceToAttack;
    private GameObject player;
    private Vector2 moveToPosition;
    private float distanceToPlayer;
    public Vector2 spawnPosition;

    //Knockback
    [SerializeField] private float knockbackForce = 100f;
    [SerializeField] private float upwardForce = 5f;
    private SpriteRenderer rend;
    private int enemyLayerDefeated;
    public PlayerHealth playerHealth;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        enemyLayerDefeated = LayerMask.NameToLayer("EnemyDefeated");
        waypoint = GameObject.FindGameObjectWithTag("EnemyMoveToPosition");
        spawnPosition = gameObject.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        moveToPosition = new Vector2(waypoint.transform.position.x, waypoint.transform.position.y);
        distanceToPlayer = Vector2.Distance(transform.position, moveToPosition);

        if (player.transform.position.x > gameObject.transform.position.x)
        {
            rend.flipX = true;
        }
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            rend.flipX = false;
        }

        if (player.GetComponent<PlayerHealth>().didRespawn == true)
        {
            GameObject.FindGameObjectWithTag("EnemyNest").GetComponent<Enemy>().EnemyRespawn();
        }
    }


    void FixedUpdate()
    {
        if (distanceToPlayer <= distanceToAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveToPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageGiven);

            if (other.transform.position.x > transform.position.x)
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
            Rigidbody2D rgbd = other.attachedRigidbody;

            if (rgbd != null)
            {
                rgbd.linearVelocity = new Vector2(rgbd.linearVelocityX, 0);
                rgbd.AddForce(new Vector2(0, bounciness));
                Instantiate(enemyPoof, transform.position, Quaternion.identity);
            }

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<AudioSource>().enabled = false;
            gameObject.layer = enemyLayerDefeated;
        }
    }
}
