using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public void Load(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
