using UnityEngine;
using UnityEngine.SceneManagement;

public class LarGibbonARScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}