using System.Collections.Generic;
using _Developers.Vitor.Scripts.Weapons;
using UnityEngine;

namespace _Developers.Vitor.Scripts.UI
{
    public class UiWeaponInventory : MonoBehaviour
    {
        [SerializeField] private GameObject uiWeaponCardPrefab;
        public GameObject backButton;
        public void SetWeaponsData(List<WeaponsScriptableObject> list)
        {
            DeleteChildren();
            if (list.Count <= 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                var uiWeaponCard = Instantiate(uiWeaponCardPrefab, transform);
                uiWeaponCard.GetComponent<UiWeaponCard>().ReceiveData(list[i],i);
            }
        }

        private void DeleteChildren()
        {
            if (transform.childCount > 0)
            {
                var objects = transform.GetComponentsInChildren<UiWeaponCard>();
                foreach (var children in objects)
                {
                    Destroy(children.transform.gameObject);
                }
            }
        }
        public void Close()
        {
            DeleteChildren();
            gameObject.SetActive(false);
        }
    }
}