using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Button _pause;
    [SerializeField] private Button _resume;
    [SerializeField] private GameObject _gameMenu;

    private void CallPause()
    {
        //GameController.Instance.TransitionToState(GameController.Instance.PauseState);
        _gameMenu.SetActive(true);
        _pause.gameObject.SetActive(false);
    }

    private void ResumeGame()
    {
        //GameController.Instance.TransitionToState(GameController.Instance.GameplayState);
        _gameMenu.SetActive(false);
        _pause.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _pause.onClick.AddListener(CallPause);
        _resume.onClick.AddListener(ResumeGame);
    }
    private void OnDisable()
    {
        _pause.onClick.RemoveListener(CallPause);
        _resume.onClick.RemoveListener(ResumeGame);
    }
}
