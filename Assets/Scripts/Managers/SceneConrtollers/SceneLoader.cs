using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public UnityAction<float> LoadingProgressChanched;

    private int _nextSceneId;
    public void Load(Scenes nextScene)
    {
        _nextSceneId = (int)nextScene;

        SceneManager.LoadScene((int)Scenes.Buffer);
    }

    public IEnumerator LoadNextScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_nextSceneId);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            LoadingProgressChanched?.Invoke(asyncOperation.progress * 100);
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
