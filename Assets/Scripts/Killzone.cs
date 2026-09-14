using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = spawnPosition.position;
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
