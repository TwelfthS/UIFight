using UnityEngine;
using System;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterManager opponent;
    public event Action<int, int> OnHPChanged;
    private int hp = 100;
    public int HP {
        get { return hp; }
        set {
            hp = value;
            OnHPChanged?.Invoke(hp, maxHp);
        }
    }
    private int maxHp = 100;
    [SerializeField] protected int damage;

    public virtual void TakeDamage(int damageTaken) {
        HP -= Mathf.Min(hp, damageTaken);
    }

    public virtual void DealDamage() {
        opponent.TakeDamage(damage);
    }
}
