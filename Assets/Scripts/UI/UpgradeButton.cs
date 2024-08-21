using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public UnityAction<string> UpgradeSelected;

    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _title;
    private string _upgradeTitle;

    public void Init(string upgradeTitle)
    {
        _upgradeTitle = upgradeTitle;
        _title.text = _upgradeTitle;
    }

    private void SelectUpgrade()
    {
        UpgradeSelected?.Invoke(_upgradeTitle);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SelectUpgrade);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SelectUpgrade);
    }
}
