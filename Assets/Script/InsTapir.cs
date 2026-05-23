using UnityEngine;
using UnityEngine.SceneManagement;

public class InsTapir : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}