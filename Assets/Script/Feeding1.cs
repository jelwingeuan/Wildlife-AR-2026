using UnityEngine;
using UnityEngine.SceneManagement;

public class Feeding1 : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}