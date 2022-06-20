using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UiWeaponCard : MonoBehaviour
    {
        public RawImage image;
        public TextMeshProUGUI weaponNameText;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI trueDamageText;
        public TextMeshProUGUI amountSlotsText;
        public Button closeButton;
        public Button equipButton;
        private int _indexId;

        public void ReceiveData(WeaponsScriptableObject data, int indexId, bool buttonEquip = true)
        {
            image.texture = data.image;
            weaponNameText.text = data.name;
            titleText.text = "Nova arma coletada";
            damageText.text = $"Dano: { (data.DataDamage.damage >= 0 ? '+' : '-' )} {data.DataDamage.damage.ToString()}" ;
            trueDamageText.text = $"Dano real: { (data.DataDamage.trueDamage >= 0 ? '+' : '-' )} {data.DataDamage.trueDamage.ToString()}";
            amountSlotsText.text = $"Quantidade de Slots: {data.slotsAmount.ToString()}";
            _indexId = indexId;
            if (buttonEquip)
            {
                equipButton.onClick.AddListener(CallEquipWeapon);
                equipButton.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(false);
            }
            else
            {
                equipButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
                closeButton.onClick.AddListener(Close);
            }
        }

        private void CallEquipWeapon()
        {
            UiManager.instance.EquipWeapon(_indexId);
            UiManager.instance.ShowWeaponsInventory();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}