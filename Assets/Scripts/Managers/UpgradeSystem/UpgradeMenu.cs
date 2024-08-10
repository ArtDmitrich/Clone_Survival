using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeMenu : MonoBehaviour
{
    public UnityAction<Upgrade> UpgradeSelected;

    [SerializeField] private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();
     
    public void ActivateUpgradeButtons(List<Upgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Init(upgrades[i]);
        }
    }

    private void SelectUpdate(Upgrade upgrade)
    {
        UpgradeSelected?.Invoke(upgrade);

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].UpgradeSelected += SelectUpdate;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].UpgradeSelected -= SelectUpdate;
        }
    }
}
