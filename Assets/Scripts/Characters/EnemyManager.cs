using UnityEngine;

public class EnemyManager : CharacterManager
{
    public override void TakeDamage(int damageTaken) {
        base.TakeDamage(damageTaken);
        base.DealDamage();
    }
}
