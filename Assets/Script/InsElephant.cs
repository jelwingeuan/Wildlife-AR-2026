using UnityEngine;
using UnityEngine.SceneManagement;

public class InsElephant : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}