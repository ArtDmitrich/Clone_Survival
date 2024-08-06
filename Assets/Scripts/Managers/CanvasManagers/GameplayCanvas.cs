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

    [SerializeField] private TMP_Text _menuTitle;

    [SerializeField] private TMP_Text _enemiesKilledValue;
    [SerializeField] private TMP_Text _levelValue;
    [SerializeField] private TMP_Text _goldValue;
    [SerializeField] private TMP_Text _healthValue;
    [SerializeField] private TMP_Text _healthPerSecValue;
    [SerializeField] private TMP_Text _manaValue;

    [SerializeField] private Slider _manaBar;
    [SerializeField] private Slider _healthBar;

    [SerializeField] private Image _loadingBackground;

    public void SetManaBarValue(float value)
    {
        _manaBar.value = value;
    }
    public void SetHealthBarValue(float value)
    {
        _healthBar.value = value;
    }

    public void SetInfoValues(int enemiesKilled, int level, int gold, int currentHealth, int maxHealth, int healthPerSec, int currentMana, int manaToNextLevel)
    {
        _enemiesKilledValue.text = enemiesKilled.ToString();
        _levelValue.text = level.ToString();
        _goldValue.text = gold.ToString();
        _healthValue.text = $"{currentHealth}/{maxHealth}";
        _healthPerSecValue.text = healthPerSec.ToString();
        _manaValue.text = $"{currentMana}/{manaToNextLevel}";
    }

    public void CallGameplayEndMenu(bool isPlayerWin)
    {
        _gameMenu.SetActive(true);
        _resume.gameObject.SetActive(false);
        _pause.gameObject.SetActive(false);

        _menuTitle.text = isPlayerWin ? "You Win!!!" : "GAME OVER.";
    }

    private void CallPause()
    {
        _gameMenu.SetActive(true);
        _resume.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);

        _menuTitle.text = "Menu";

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
