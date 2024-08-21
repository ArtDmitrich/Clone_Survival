using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeMenu : MonoBehaviour
{
    public UnityAction<string> UpgradeSelected;

    [SerializeField] private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();
     
    public void ActivateUpgradeButtons(List<string> upgradesTitles)
    {
        for (int i = 0; i < upgradesTitles.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Init(upgradesTitles[i]);
        }
    }

    private void SelectUpgrade(string upgradeTitle)
    {
        UpgradeSelected?.Invoke(upgradeTitle);

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].UpgradeSelected += SelectUpgrade;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].UpgradeSelected -= SelectUpgrade;
        }
    }
}
