using UnityEngine;
using UnityEngine.SceneManagement;

public class iTiger : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}