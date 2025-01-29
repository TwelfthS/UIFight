using UnityEngine;

public class EnemyManager : CharacterManager
{
    [SerializeField] private int enemyDamage;
    private int currentTargetIndex = 0;
    [SerializeField] private BodyPart[] possibleTargets = { BodyPart.Head, BodyPart.Torso };
    public override void TakeDamage(int damageTaken, BodyPart bodyPart) {
        base.TakeDamage(damageTaken, bodyPart);
        base.DealDamage(enemyDamage, possibleTargets[currentTargetIndex]);
        SwitchTarget();
    }

    private void SwitchTarget()
    {
        currentTargetIndex = currentTargetIndex + 1 >= possibleTargets.Length ? 0 : currentTargetIndex + 1;
    }

    protected override void OnDeath() {
        LootManager.Instance.SpawnRandomLoot();
        HP = _maxHp;
    }
}
