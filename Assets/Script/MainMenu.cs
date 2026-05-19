using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("START BUTTON CLICKED");
        SceneManager.LoadScene(1);
    }

    public void OpenInstruction()
    {
        Debug.Log("INSTRUCTION BUTTON CLICKED");
        SceneManager.LoadScene(2);
    }
}