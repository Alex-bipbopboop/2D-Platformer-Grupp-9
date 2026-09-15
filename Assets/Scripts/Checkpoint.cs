using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //[SerializeField] private Killzone killzone;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.SetSpawnPosition(transform);
            }
        } 
        
    }
}
