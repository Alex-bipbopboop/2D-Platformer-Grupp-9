using UnityEngine;
using TMPro;
using System.Reflection.Metadata.Ecma335;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] private int strawberriesToCollect = 10;
    [SerializeField] private TMP_Text strawberryText;
    [SerializeField] private AudioClip pickupSoundEffect;
    private int strawberries = 0;
    private AudioSource audioSource;

    private void Start()
    {
        strawberryText.text = "" + strawberries;
        audioSource = GetComponent<AudioSource>();
    }

    public void AddStrawberry()
    {
        strawberries++;
        strawberryText.text = "" + strawberries;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(pickupSoundEffect);
    }

    public int GetStrawberries() { return strawberries;}

    public int GetStrawberriesToCollect() { return strawberriesToCollect;}
}
