using UnityEngine;
using UnityEngine.SceneManagement;

public class ARCam2 : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}