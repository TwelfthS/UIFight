using UnityEngine;
using System;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager opponent;
    public event Action<int, int> HPChanged;
    private int _hp = 100;
    protected int HP {
        get { return _hp; }
        set {
            _hp = value;
            HPChanged?.Invoke(_hp, _maxHp);
        }
    }
    protected int _maxHp = 100;

    void Awake() {
        HP = _maxHp;
        HPChanged += DeathCheck;
    }

    public virtual void TakeDamage(int damageTaken, BodyPart bodyPart) {
        HP = Mathf.Max(HP - damageTaken, 0);
    }

    public virtual void DealDamage(int damage, BodyPart bodyPart) {
        if (opponent != null) {
            opponent.TakeDamage(damage, bodyPart);            
        }
    }

    public int GetHP() {
        return HP;
    }

    public void LoadHP(int hp) {
        HP = hp;
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
