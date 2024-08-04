using UnityEngine;
using Zenject;

public class BufferSceneController : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    private BufferCanvas _bufferCanvas;

    [Inject]
    private void Construct(SceneLoader sceneLoader, BufferCanvas bufferCanvas)
    {
        _sceneLoader = sceneLoader;
        _bufferCanvas = bufferCanvas;
    }

    private void Start()
    {
        StartAsyncLoadingNextScene();
    }
    private void StartAsyncLoadingNextScene()
    {
        StartCoroutine(_sceneLoader.LoadNextScene());
    }

    private void SetLoadingProgressToSlider(float value)
    {
        _bufferCanvas.SetSliderValue(value);
    }

    private void OnEnable()
    {
        //_sceneLoader.PrepearedToLoadingNextScene += StartAsyncLoadingNextScene;
        _sceneLoader.LoadingProgressChanched += SetLoadingProgressToSlider;
    }

    private void OnDisable()
    {
        //_sceneLoader.PrepearedToLoadingNextScene -= StartAsyncLoadingNextScene;
        _sceneLoader.LoadingProgressChanched -= SetLoadingProgressToSlider;
    }
}
