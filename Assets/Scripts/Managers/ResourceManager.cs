using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    public UnityAction<float> PlayerHealed;
    public UnityAction<float> ManaRatioChanged;
    public UnityAction PlayersLevelUpped;
    public int EnemiesKilled
    {
        get { return _enemiesKilled; }
    }
    public int CurrentPlayerLevel
    {
        get { return _currentLevel; }
    }
    public int Gold
    {
        get { return _gold; }
    }
    public int CurrentMana
    {
        get { return (int)_currentMana; }
    }
    public int ManaToNextLevel
    {
        get { return (int)_manaToNextLevel; }
    }
    

    [SerializeField] private float _manaToNextLevel;

    [Tooltip("The parameter is set in percentage. For example: 0.3 = increase mana at the next level by 30%")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _increaseManaPerLevel;
    [SerializeField] private int _manaForKillingEnemy;
    [SerializeField] private int _enemiesKilled;

    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _currentMana;
    [SerializeField] private int _gold;

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
            PlayersLevelUpped?.Invoke();
        }

        ManaRatioChanged?.Invoke(_currentMana/_manaToNextLevel);
    }

    private void AddGold(float gold)
    {
        _gold += (int)gold;
    }

    private void AddHeal(float heal)
    {
        PlayerHealed?.Invoke(heal);
    }
}
