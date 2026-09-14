using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject textPopup;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            textPopup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textPopup.SetActive(false);
        }
    }
}
