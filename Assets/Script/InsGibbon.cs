using UnityEngine;
using UnityEngine.SceneManagement;

public class InsGibbon : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}