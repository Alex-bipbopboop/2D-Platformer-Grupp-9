using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToRestore;
    [SerializeField] private GameObject pineappleParticleSystem;
    private int enemyLayerDefeated;

    private void Start()
    {
        enemyLayerDefeated = LayerMask.NameToLayer("EnemyDefeated");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool hasRestoredHealth = other.gameObject.GetComponent<PlayerHealth>().RestoreHealth(healthToRestore);
            if (hasRestoredHealth)
            {
                Instantiate(pineappleParticleSystem, transform.position, Quaternion.identity);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.layer = enemyLayerDefeated;
            }
        }
    }
}
