using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UiWeaponCard : MonoBehaviour
    {
        public RawImage image;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI trueDamageText;
        public TextMeshProUGUI amountSlotsText;
        public Button closeButton;
        public Button equipButton;
        private int _indexId;

        public void ReceiveData(WeaponsScriptableObject data, int indexId)
        {
            image.texture = data.image;
            titleText.text = data.name;
            damageText.text = data.DataDamage.damage.ToString();
            trueDamageText.text = data.DataDamage.trueDamage.ToString();
            amountSlotsText.text = data.slotsAmount.ToString();
            _indexId = indexId;
            closeButton.onClick.AddListener(CallEquipWeapon);
        }

        private void CallEquipWeapon()
        {
            Debug.Log("Tentei");
            UiManager.instance.EquipWeapon(_indexId);
        }
    }
}