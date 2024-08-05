using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    public UnityAction PausePressed;
    public UnityAction ResumePressed;
    public UnityAction MainMenuPressed;

    [SerializeField] private Button _pause;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private GameObject _gameMenu;

    [SerializeField] private TMP_Text _gameEndTitle;

    [SerializeField] private Image _loadingBackground;

    public void CallGameplayEndMenu(bool isPlayerWin)
    {
        _gameMenu.SetActive(true);
        _resume.gameObject.SetActive(false);
        _pause.gameObject.SetActive(false);

        _gameEndTitle.gameObject.SetActive(true);
        _gameEndTitle.text = isPlayerWin ? "You Win!!!" : "GAME OVER.";
    }

    private void CallPause()
    {
        _gameMenu.SetActive(true);
        _resume.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
        _gameEndTitle.gameObject.SetActive(false);

        PausePressed?.Invoke();
    }

    private void ResumeGame()
    {
        _gameMenu.SetActive(false);
        _pause.gameObject.SetActive(true);

        ResumePressed?.Invoke();
    }

    private void BackToMainMenu()
    {
        _gameMenu.SetActive(false);
        _loadingBackground.gameObject.SetActive(true);

        MainMenuPressed?.Invoke();
    }

    private void OnEnable()
    {
        _loadingBackground.gameObject.SetActive(false);

        _pause.onClick.AddListener(CallPause);
        _resume.onClick.AddListener(ResumeGame);
        _mainMenu.onClick.AddListener(BackToMainMenu);
    }
    private void OnDisable()
    {
        _pause.onClick.RemoveListener(CallPause);
        _resume.onClick.RemoveListener(ResumeGame);
        _mainMenu.onClick.RemoveListener(BackToMainMenu);
    }
}
