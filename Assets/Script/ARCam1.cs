using UnityEngine;
using UnityEngine.SceneManagement;

public class ARCam1 : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}