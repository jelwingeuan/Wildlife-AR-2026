using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsTiger : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}