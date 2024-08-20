using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameLevelSelector : MonoBehaviour
{
    public UnityAction<GameLevelInfo, bool> GameLevelSelected;

    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private Button _previousLevel;
    [SerializeField] private Button _nextLevel;

    [SerializeField] private Image _levelIcon;
    [SerializeField] private TMP_Text _levelInfo;

    [SerializeField] private GameModeSelectingButton _enterZone;
    [SerializeField] private GameModeSelectingButton _endlessMode;

    [SerializeField] private GameLevelsSettings _gameLevelsSettings;

    private GameLevelInfo _currentLevel;
    private int _currentLevelIndex = 0;

    private void IncreaseCurrentLevelIndex()
    {
        _currentLevelIndex++;

        UpdateSelectorMenu();
    }

    private void DecreaseCurrentLevelIndex()
    {
        _currentLevelIndex--;

        UpdateSelectorMenu();
    }

    private void UpdateSelectorMenu()
    {
        UdpateSelectorButtonsInteractable();
        SetCurentLevel();
        SetCurrentInfo();
        UpdateStartGameButtons();
    }

    private void UdpateSelectorButtonsInteractable()
    {
        _previousLevel.interactable = _currentLevelIndex != 0;
        _nextLevel.interactable = _currentLevelIndex < (_gameLevelsSettings.LevelsCount - 1);
    }
    private void SetCurentLevel()
    {
        _currentLevel = _gameLevelsSettings.GetGameLevelInfo(_currentLevelIndex);
    }

    private void SetCurrentInfo()
    {
        _levelNumber.text = _currentLevel.LevelNumber.ToString();
        _levelIcon.sprite = _currentLevel.Icon;
        _levelInfo.text = _currentLevel.Info;
    }

    private void UpdateStartGameButtons()
    {
        var maxOpennedLvl = PlayerPrefs.GetInt("MaxOpennedLevel", 1);
        var currentLvl = _currentLevel.LevelNumber;

        if (maxOpennedLvl == currentLvl)
        {
            _enterZone.gameObject.SetActive(true);
            _endlessMode.gameObject.SetActive(false);
        }
        else if (maxOpennedLvl < currentLvl)
        {
            _enterZone.gameObject.SetActive(false);
            _endlessMode.gameObject.SetActive(false);
        }
        else
        {
            _enterZone.gameObject.SetActive(true);
            _endlessMode.gameObject.SetActive(true);
        }
    }

    private void SelectLevel(bool gameModeIsEndless)
    {
        GameLevelSelected?.Invoke(_currentLevel, gameModeIsEndless);
    }

    private void OnEnable()
    {
        _enterZone.GameModeSelected += SelectLevel;
        _endlessMode.GameModeSelected += SelectLevel;

        _previousLevel.onClick.AddListener(DecreaseCurrentLevelIndex);
        _nextLevel.onClick.AddListener(IncreaseCurrentLevelIndex);

        _currentLevelIndex = 0;
        UpdateSelectorMenu();
    }

    private void OnDisable()
    {
        _enterZone.GameModeSelected -= SelectLevel;
        _endlessMode.GameModeSelected -= SelectLevel;

        _previousLevel.onClick.RemoveListener(DecreaseCurrentLevelIndex);
        _nextLevel.onClick.RemoveListener(IncreaseCurrentLevelIndex);
    }
}
