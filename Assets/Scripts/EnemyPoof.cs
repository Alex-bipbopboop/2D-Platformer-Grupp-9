using UnityEngine;

public class EnemyPoof : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    [SerializeField] private AudioClip enemyDestroySoundEffect;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, timeToDestroy);

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(enemyDestroySoundEffect);
    }
}
