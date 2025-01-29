using UnityEngine;
using TMPro;

public class ArmorStats : MonoBehaviour
{
    [SerializeField] private TMP_Text headTotal;
    [SerializeField] private TMP_Text torsoTotal;
    void Start() {
        ArmorManager.Instance.headSlot.contentChanged += UpdateStatsText;
        ArmorManager.Instance.torsoSlot.contentChanged += UpdateStatsText;
        UpdateStatsText(BodyPart.Head);
        UpdateStatsText(BodyPart.Torso);
    }
    private void UpdateStatsText(BodyPart bodyPart) {
        Apparel apparel = ArmorManager.Instance.GetApparel(bodyPart);
        TMP_Text textByBodyPart = GetTextByBodyPart(bodyPart);
        if (textByBodyPart) {
            textByBodyPart.text = apparel == null ? "0" : apparel.defense.ToString();
        }
        
    }
    private TMP_Text GetTextByBodyPart(BodyPart bodyPart) {
        TMP_Text result = null;
        if (bodyPart == BodyPart.Head) {
            result = headTotal;
        } else if (bodyPart == BodyPart.Torso) {
            result = torsoTotal;
        }
        return result;
    }
    void OnDestroy() {
        ArmorManager.Instance.headSlot.contentChanged -= UpdateStatsText;
        ArmorManager.Instance.torsoSlot.contentChanged -= UpdateStatsText;
    }
}
