using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType WeaponType;
    public int WeaponLevel;

    protected float DamageValue
    {
        get { return _damageBase + _additionalDamage; }
    }

    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected float _coldownTime;
    [SerializeField] protected float _damageBase;

    protected bool _canAttack;
    protected float _additionalDamage;

    public void SetBaseDamage(float damage)
    {
        _damageBase = damage;
    }

    public virtual void SetAdditionalDamage(float damage)
    {
        _additionalDamage = damage;
    }

    protected IEnumerator AttackingColdown(float coldownTime)
    {
        _canAttack = false;

        yield return new WaitForSeconds(coldownTime);

        _canAttack = true;
    }

    protected void OnEnable()
    {
        _canAttack = true;
    }
}
