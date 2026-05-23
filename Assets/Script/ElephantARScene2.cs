using UnityEngine;
using UnityEngine.SceneManagement;

public class ElephantARScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}