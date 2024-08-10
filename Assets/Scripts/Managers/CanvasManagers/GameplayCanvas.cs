using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    public UnityAction PausePressed;
    public UnityAction ResumePressed;
    public UnityAction MainMenuPressed;
    public UnityAction<Upgrade> UpgradeSelected;

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

    [SerializeField] private UpgradeMenu _upgradeMenu;

    private void Start()
    {
        _gameMenu.gameObject.SetActive(false);
        _upgradeMenu.gameObject.SetActive(false);
    }

    public void SetManaBarValue(float value)
    {
        _manaBar.value = value;
    }
    public void SetHealthBarValue(float value)
    {
        _healthBar.value = value;
    }

    public void SetInfoValues(int enemiesKilled, int level, int gold, int currentHealth, int maxHealth, float healthPerSec, int currentMana, int manaToNextLevel)
    {
        _enemiesKilled.Value.text = enemiesKilled.ToString();
        _level.Value.text = level.ToString();
        _gold.Value.text = gold.ToString();
        _health.Value.text = $"{currentHealth}/{maxHealth}";
        _healthPerSec.Value.text = healthPerSec.ToString();
        _mana.Value.text = $"{currentMana}/{manaToNextLevel}";
    }

    public void CallGameplayEndMenu(bool isPlayerWin)
    {
        _gameMenu.SetActive(true);
        _resume.gameObject.SetActive(false);
        _pause.gameObject.SetActive(false);

        _menuTitle.text = isPlayerWin ? "You Win!!!" : "GAME OVER.";

        _health.gameObject.SetActive(false);
        _healthPerSec.gameObject.SetActive(false);
        _mana.gameObject.SetActive(false);
    }

    public void CallUpgradeMenu(List<Upgrade> upgrades)
    {
        _upgradeMenu.gameObject.SetActive(true);

        _upgradeMenu.ActivateUpgradeButtons(upgrades);
    }

    private void GetSelectedUpgrade(Upgrade upgrade)
    {
        UpgradeSelected?.Invoke(upgrade);
        _upgradeMenu.gameObject.SetActive(false);
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

        _upgradeMenu.UpgradeSelected += GetSelectedUpgrade;
    }
    private void OnDisable()
    {
        _pause.onClick.RemoveListener(CallPause);
        _resume.onClick.RemoveListener(ResumeGame);
        _mainMenu.onClick.RemoveListener(BackToMainMenu);

        _upgradeMenu.UpgradeSelected -= GetSelectedUpgrade;
    }
}
