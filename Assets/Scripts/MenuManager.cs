using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Transform player;
    public GameObject mainMenu;
    
    // Check if player is alive
    private void Update()
    {
        if (!player) {
            mainMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("RunnerScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}