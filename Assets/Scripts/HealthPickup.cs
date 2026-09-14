using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToRestore;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool hasRestoredHealth = other.gameObject.GetComponent<PlayerHealth>().RestoreHealth(healthToRestore);
            if (hasRestoredHealth)
            {
                Destroy(gameObject);
            }
        }
    }
}
