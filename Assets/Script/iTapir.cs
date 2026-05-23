using UnityEngine;
using UnityEngine.SceneManagement;

public class iTapir : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}