using System.Collections.Generic;
using Artifacts;
using UnityEngine;

namespace UI
{
    public class UiArtifactInventory : MonoBehaviour
    {
        [SerializeField] private GameObject uiArtifactCardPrefab;
        public GameObject buttonBack;
        public void SetArtifactData(List<ArtifactsScriptableObject> list)
        {
            DeleteChildren();
            if (list.Count <= 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                var uiWeaponCard = Instantiate(uiArtifactCardPrefab, transform);
                uiWeaponCard.GetComponent<UIArtifactCard>().ReceiveData(list[i],i);
            }
        }
        private void DeleteChildren()
        {
            if (transform.childCount > 0)
            {
                var objects = transform.GetComponentsInChildren<UIArtifactCard>();
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
