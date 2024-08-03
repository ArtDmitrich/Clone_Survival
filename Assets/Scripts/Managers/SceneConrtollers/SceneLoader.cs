using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void Load(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
