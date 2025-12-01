using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void OnStartGame() 
    {
        SceneManager.LoadScene("Game");
    }
    public void OnCredits()
    {
        Debug.Log("Todo Add Credits.");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
