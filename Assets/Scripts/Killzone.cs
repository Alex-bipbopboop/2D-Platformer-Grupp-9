using UnityEngine;

public class Killzone : MonoBehaviour
{
    //[SerializeField] private Transform spawnPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().Respawn();
        }
    }
}
