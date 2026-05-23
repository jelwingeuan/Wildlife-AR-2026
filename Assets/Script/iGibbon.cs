using UnityEngine;
using UnityEngine.SceneManagement;

public class iGibbon : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}