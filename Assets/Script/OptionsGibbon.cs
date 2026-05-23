using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsGibbon : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}