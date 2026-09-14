using UnityEngine;

public class StrawberryPickup : MonoBehaviour
{
    [SerializeField] private GameObject strawberryParticleSystem;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerQuest>().AddStrawberry();
            Instantiate(strawberryParticleSystem, transform.position, Quaternion.identity);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
