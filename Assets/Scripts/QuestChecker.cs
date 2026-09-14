using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject panel, finishedText, unfinishedText;
    [SerializeField] private int levelIndex;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerQuest>().GetStrawberries() >= other.GetComponent<PlayerQuest>().GetStrawberriesToCollect())
            {
                panel.SetActive(true);
                finishedText.SetActive(true);
                anim.SetTrigger("Flag");
                Invoke(nameof(LoadNextLevel), 3.0f);
            }
            else
            {
                panel.SetActive(true);
                unfinishedText.SetActive(true);
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        panel.SetActive(false);
        finishedText.SetActive(false);
        unfinishedText.SetActive(false);
    }
}
