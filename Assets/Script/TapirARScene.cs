using UnityEngine;
using UnityEngine.SceneManagement;

public class TapirARScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}