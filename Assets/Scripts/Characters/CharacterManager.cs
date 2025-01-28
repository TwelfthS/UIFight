using UnityEngine;
using System;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager opponent;
    public event Action<int, int> OnHPChanged;
    private int _hp = 100;
    protected int HP {
        get { return _hp; }
        set {
            _hp = value;
            OnHPChanged?.Invoke(_hp, _maxHp);
        }
    }
    protected int _maxHp = 100;

    void Awake() {
        HP = _maxHp;
        OnHPChanged += DeathCheck;
    }

    public virtual void TakeDamage(int damageTaken, BodyPart bodyPart) {
        HP = Mathf.Max(HP - damageTaken, 0);
    }

    public virtual void DealDamage(int damage, BodyPart bodyPart) {
        if (opponent != null) {
            opponent.TakeDamage(damage, bodyPart);            
        }
    }

    private void DeathCheck(int newHp, int maxHp) {
        if (newHp <= 0) {
            OnDeath();
        }
    }

    protected virtual void OnDeath() {
        Destroy(this.gameObject);
    }
}
