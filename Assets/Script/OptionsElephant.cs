using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsElephant : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}