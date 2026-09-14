using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private GameObject creditsPanel;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
}
