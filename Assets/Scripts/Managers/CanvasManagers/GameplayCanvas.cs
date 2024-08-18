using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class GameplayCanvas : MonoBehaviour
{
    public UnityAction Paused;
    public UnityAction Resumed;
    public UnityAction MainMenuPressed;

    [SerializeField] private Button _pause;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private GameObject _gameMenu;

    [SerializeField] private TMP_Text _menuTitle;

    [SerializeField] private ItemInfo _enemiesKilled;
    [SerializeField] private ItemInfo _level;
    [SerializeField] private ItemInfo _gold;
    [SerializeField] private ItemInfo _health;
    [SerializeField] private ItemInfo _healthPerSec;
    [SerializeField] private ItemInfo _mana;

    [SerializeField] private Slider _manaBar;
    [SerializeField] private Slider _healthBar;

    [SerializeField] private Image _loadingBackground;


    [SerializeField] private GameObject _joystick;
    [SerializeField] private StopwatchItem _stopwatch;

    [SerializeField] private UpgradeMenu _upgradeMenu;

    private GameplayManager _gameplayManager;
    private ResourceManager _resourceManager;
    private TimersManager _timersManager;
    private UpgradeSystem _upgradeSystem;

    private HealthComponent _playerHealthComponent;
    private CharacterStats _playerStats;

    [Inject]
    private void Construct(PlayerController playerController, GameplayManager gameplayManager, ResourceManager resourceManager, TimersManager timersManager, UpgradeSystem upgradeSystem)
    {
        _playerHealthComponent = playerController.HealthComponent;
        _playerStats = playerController.CharacterStats;

        _gameplayManager = gameplayManager;
        _resourceManager = resourceManager;
        _timersManager = timersManager;
        _upgradeSystem = upgradeSystem;
    }

    private void Start()
    {
        _gameMenu.gameObject.SetActive(false);
        _upgradeMenu.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(true);

        _timersManager.SetStopwatch(SetStopwatchTime);
    }

    private void SetManaBarValue(float value)
    {
        _manaBar.value = value;
    }

    private void SetHealthBarValue(float value)
    {
        _healthBar.value = value;
    }

    private void SetInfoValues(int enemiesKilled, int level, int gold, float currentHealth, float maxHealth, float healthPerSec, int currentMana, int manaToNextLevel)
    {
        _enemiesKilled.Value.text = enemiesKilled.ToString();
        _level.Value.text = level.ToString();
        _gold.Value.text = gold.ToString();
        _health.Value.text = $"{currentHealth:0}/{maxHealth:0}";
        _healthPerSec.Value.text = healthPerSec.ToString();
        _mana.Value.text = $"{currentMana}/{manaToNextLevel}";
    }

    private void CallGameplayEndMenu(bool isPlayerWin)
    {
        SetInfoValues(_resourceManager.EnemiesKilled, _resourceManager.CurrentPlayerLevel, _resourceManager.Gold,
            _playerStats.CurrentHealth, _playerStats.MaxHealth, _playerStats.HealthPerSec,
            _resourceManager.CurrentMana, _resourceManager.ManaToNextLevel);

        _gameMenu.SetActive(true);
        _resume.gameObject.SetActive(false);
        _pause.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(false);

        _menuTitle.text = isPlayerWin ? "You Win!!!" : "GAME OVER.";

        _health.gameObject.SetActive(false);
        _healthPerSec.gameObject.SetActive(false);
        _mana.gameObject.SetActive(false);

        _timersManager.RemoveAllStopwatches();
        Paused?.Invoke();
    }

    private void SetStopwatchTime(int minute,  int second)
    {
        _stopwatch.SetTime(minute, second);
    }

    private void CallUpgradeMenu()
    {
        _upgradeMenu.gameObject.SetActive(true);
        _joystick.gameObject.SetActive(false);

        Paused?.Invoke();
    }

    private void CallPause()
    {
        SetInfoValues(_resourceManager.EnemiesKilled, _resourceManager.CurrentPlayerLevel, _resourceManager.Gold,
            _playerStats.CurrentHealth, _playerStats.MaxHealth, _playerStats.HealthPerSec,
            _resourceManager.CurrentMana, _resourceManager.ManaToNextLevel);

        _gameMenu.SetActive(true);
        _pause.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(false);

        _menuTitle.text = "Menu";

        Paused?.Invoke();
    }

    private void ResumeGame()
    {
        _gameMenu.SetActive(false);
        _upgradeMenu.gameObject.SetActive(false);
        _pause.gameObject.SetActive(true);
        _joystick.gameObject.SetActive(true);

        Resumed?.Invoke();
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

        _gameplayManager.GameplayEnded += CallGameplayEndMenu;

        _upgradeSystem.UpgradingStarted += CallUpgradeMenu;
        _upgradeSystem.UpgradingFinished += ResumeGame;

        _resourceManager.ManaRatioChanged += SetManaBarValue;

        _playerHealthComponent.HealthRationChanged += SetHealthBarValue;
    }
    private void OnDisable()
    {
        _pause.onClick.RemoveListener(CallPause);
        _resume.onClick.RemoveListener(ResumeGame);
        _mainMenu.onClick.RemoveListener(BackToMainMenu);

        _gameplayManager.GameplayEnded -= CallGameplayEndMenu;

        _upgradeSystem.UpgradingStarted -= CallUpgradeMenu;
        _upgradeSystem.UpgradingFinished -= ResumeGame;

        _resourceManager.ManaRatioChanged -= SetManaBarValue;

        _playerHealthComponent.HealthRationChanged -= SetHealthBarValue;
    }
}
