using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    public UnityAction<float> PlayerHealed;

    [SerializeField] private float _manaToNextLevel;

    [Tooltip("The parameter is set in percentage. For example: 0.3 = increase mana at the next level by 30%")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _increaseManaPerLevel;
    [SerializeField] private int _manaForKillingEnemy;
    [SerializeField] private int _enemiesKilled;

    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _currentMana;
    [SerializeField] private float _gold;

    public void PlayerPickUpItem(PickUpItem pickUpItem)
    {
        var attributes = pickUpItem.Attributes;

        if (attributes.Gold != 0)
        {
            AddGold(attributes.Gold);
        }

        if (attributes.Heal != 0)
        {
            AddHeal(attributes.Heal);
        }

        if (attributes.Mana != 0)
        {
            AddMana(attributes.Mana);
        }
    }

    public void PlayerKilledEnemy()
    {
        _enemiesKilled++;
        AddMana(_manaForKillingEnemy);
    }

    private void AddMana(float mana)
    {
        _currentMana += mana;

        if (_currentMana >= _manaToNextLevel)
        {
            _currentLevel++;
            _currentMana = 0;
            _manaToNextLevel *= (1 + _increaseManaPerLevel);

            Debug.Log($"LEVEL UP! Current level: {_currentLevel}");
        }
    }

    private void AddGold(float gold)
    {
        _gold += gold;
    }

    private void AddHeal(float heal)
    {
        PlayerHealed?.Invoke(heal);
    }
}
