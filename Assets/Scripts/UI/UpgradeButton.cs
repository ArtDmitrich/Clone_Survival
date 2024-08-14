using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public UnityAction<Upgrade> UpgradeSelected;

    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _title;
    private Upgrade _upgrade;

    public void Init(Upgrade upgrade)
    {
        _upgrade = upgrade;
        _title.text = _upgrade.Title;
    }

    private void SelectUpgrade()
    {
        UpgradeSelected?.Invoke(_upgrade);
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
