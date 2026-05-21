using UnityEngine;
using UnityEngine.SceneManagement;

public class WildlifeARScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}