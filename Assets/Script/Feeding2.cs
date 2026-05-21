using UnityEngine;
using UnityEngine.SceneManagement;

public class Feeding2 : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}