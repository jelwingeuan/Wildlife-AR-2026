using UnityEngine;
using UnityEngine.SceneManagement;

public class InsTiger : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}