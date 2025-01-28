using UnityEngine;
using TMPro;

public class ArmorStats : MonoBehaviour
{
    [SerializeField] private TMP_Text headTotal;
    [SerializeField] private TMP_Text torsoTotal;
    void Start() {
        ArmorManager.Instance.headSlot.contentChanged += UpdateStatsText;
        ArmorManager.Instance.torsoSlot.contentChanged += UpdateStatsText;
    }
    private void UpdateStatsText(Apparel apparel, BodyPart bodyPart) {
        if (bodyPart == BodyPart.Head) {
            if (apparel == null) {
                headTotal.text = "0";
            } else {
                headTotal.text = apparel.defense.ToString();
            }
        } else if (bodyPart == BodyPart.Torso) {
            if (apparel == null) {
                torsoTotal.text = "0";
            } else {
                torsoTotal.text = apparel.defense.ToString();
            }
        }
    }
    void OnDestroy() {
        ArmorManager.Instance.headSlot.contentChanged -= UpdateStatsText;
        ArmorManager.Instance.torsoSlot.contentChanged -= UpdateStatsText;
    }
}
