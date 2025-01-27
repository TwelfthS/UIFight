using UnityEngine;
using System;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager opponent;
    public event Action<int, int> OnHPChanged;
    private int hp = 100;
    protected int HP {
        get { return hp; }
        set {
            hp = value;
            OnHPChanged?.Invoke(hp, maxHp);
        }
    }
    protected int maxHp = 100;

    void Awake() {
        HP = maxHp;
    }

    public virtual void TakeDamage(int damageTaken) {
        HP -= Mathf.Min(hp, damageTaken);
    }

    public virtual void DealDamage(int damage) {
        opponent.TakeDamage(damage);
    }
}
