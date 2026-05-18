using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ChooseCompanion");
    }

    public void OpenInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }
}