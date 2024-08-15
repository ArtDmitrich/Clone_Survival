using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public UnityAction PlayPressed;

    [SerializeField] private Button _play;
    [SerializeField] private Button _backToMainMenu;
    [SerializeField] private Image _loadingBackground;

    [SerializeField] private MainMenuItem _levelSelectionMenu;

    private MainMenuItem _currentOpenedMenu;

    public void ActivateLoadingBackground()
    {
        _loadingBackground.gameObject.SetActive(true);

    }

    private void OpenLevelSelectionMenu()
    {
        OpenMenu(_levelSelectionMenu);
    }

    private void OpenMenu(MainMenuItem item)
    {
        _currentOpenedMenu = item;
        _currentOpenedMenu.gameObject.SetActive(true);
        _backToMainMenu.gameObject.SetActive(true);
    }

    private void CloseOpenedMenu()
    {
        _currentOpenedMenu.gameObject.SetActive(false);
        _backToMainMenu?.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _loadingBackground.gameObject.SetActive(false);
        _backToMainMenu.gameObject.SetActive(false);

        _play.onClick.AddListener(OpenLevelSelectionMenu);

        _backToMainMenu.onClick.AddListener(CloseOpenedMenu);
    }

    private void OnDisable()
    {
        _play.onClick.RemoveListener(OpenLevelSelectionMenu);

        _backToMainMenu.onClick.RemoveListener(CloseOpenedMenu);
    }
}
