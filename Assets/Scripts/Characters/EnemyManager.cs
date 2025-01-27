using UnityEngine;

public class EnemyManager : CharacterManager
{
    [SerializeField] private int enemyDamage;
    public override void TakeDamage(int damageTaken) {
        base.TakeDamage(damageTaken);
        base.DealDamage(enemyDamage);
    }
}
