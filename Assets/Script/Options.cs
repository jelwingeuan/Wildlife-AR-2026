using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}