using UnityEngine;

public class EnemyManager : CharacterManager
{
    [SerializeField] private int enemyDamage;
    private int currentIndex = 0;
    private BodyPart[] possibleTargets = { BodyPart.Head, BodyPart.Torso };
    public override void TakeDamage(int damageTaken, BodyPart bodyPart) {
        base.TakeDamage(damageTaken, bodyPart);
        base.DealDamage(enemyDamage, possibleTargets[currentIndex]);
        SwitchTarget();
    }

    private void SwitchTarget()
    {
        currentIndex = currentIndex + 1 >= possibleTargets.Length ? 0 : currentIndex + 1;
    }

    protected override void OnDeath() {
        LootManager.Instance.SpawnRandomLoot();
        base.OnDeath();
    }
}
