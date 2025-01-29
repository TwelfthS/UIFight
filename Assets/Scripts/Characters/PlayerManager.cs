using UnityEngine;

public class PlayerManager : CharacterManager
{
    void Start() {
        PlayerEventManager.Instance.itemUsed += OnItemUsed;
    }
    public override void TakeDamage(int damageTaken, BodyPart bodyPart) {
        int damageReduced = ArmorManager.Instance.GetDefense(bodyPart);
        damageTaken = Mathf.Max(damageTaken - damageReduced, 0);
        base.TakeDamage(damageTaken, bodyPart);
    }

    private void OnItemUsed(Item item) {
        if (item is Medical medicalItem) {
            HP = Mathf.Min(HP + medicalItem.healing, _maxHp);
        }
    }

    protected override void OnDeath() {
        Debug.Log("Game over");
        GameManager.Instance.GameOver();
        base.OnDeath();
    }

    void OnDestroy() {
        PlayerEventManager.Instance.itemUsed -= OnItemUsed;
    }

}
