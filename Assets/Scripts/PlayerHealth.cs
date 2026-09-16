using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 5;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color normalHealthColor, criticalHealthColor;
    [SerializeField] private AudioClip pickupSoundEffect;
    [SerializeField] private AudioClip damageSoundEffect;
    [SerializeField] private AudioClip deathSoundEffect;
    private int currentHealth;
    private AudioSource audioSource;
    private object current;

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(damageSoundEffect);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = spawnPosition.position;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        audioSource.PlayOneShot(deathSoundEffect);
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if(currentHealth <= 2)
        {
            fillImage.color = criticalHealthColor;
        }
        else
        {
            fillImage.color = normalHealthColor;
        }
    }

    public bool RestoreHealth(int healthToRestore)
    {
        if (currentHealth >= startingHealth)
        {
            return false;
        }
        currentHealth += healthToRestore;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(pickupSoundEffect);
        UpdateHealthBar();

        if(currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
        return true;
    }
}
