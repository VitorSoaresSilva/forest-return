using System;
using Artifacts;
using Attributes;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Weapons;
using Attribute = Attributes.Attribute;

namespace UI
{
    public class UiManager : Singleton<UiManager>
    {
        [System.Serializable]
        public class AttributesUI : IComparable
        {
            public AttributeType type;
            public TextMeshProUGUI text;
            public string suffix;
            public string prefix;
            public Slider slider;

            public void ChangeText(int currentValue, int maxValue)
            {
                if (text != null)
                {
                    // text.text = $"{prefix}{currentValue}{suffix}";
                    text.text = $"{currentValue}/{maxValue}";
                }
            }

            public void ChangeSlider(int currentValue, int maxValue)
            {
                if (slider != null)
                {
                    slider.value = (float)currentValue / (float)maxValue;
                }
            }

            public int CompareTo(object obj)
            {
                return (int) type - (int) ((AttributesUI) obj)!.type;
            }
        }
        public AttributesUI[] attributesMax;
        public AttributesUI[] attributesCurrent;
        private Animator _animator;

        [field: SerializeField] public UiWeaponInventory UiWeaponInventory { get; private set; }
        [field: SerializeField] public UiArtifactInventory UiArtifactInventory { get; private set; }
        [SerializeField] private GameObject blackSmithUIGameObject;

        [Header("Weapon Card")] 
        [SerializeField] private UiWeaponCard uiWeaponCard;
        [SerializeField] private UIArtifactCard uiArtifactCard;
        [SerializeField] private Camera camera;
        [SerializeField] private MainMenu _mainMenu;
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            Array.Sort(attributesMax);
            _animator = GetComponent<Animator>();
        }

        public void UpdateMaxValueAttribute(Attribute attribute)
        {
            if (attributesMax.Length > (int)attribute.Type)
            {
                attributesMax[(int) attribute.Type].ChangeText(attribute.CurrentValue,attribute.MaxValue);
            }
        }
        public void UpdateCurrentValueAttribute(Attribute attribute)
        {
            if (attributesCurrent.Length > (int)attribute.Type)
            {
                attributesCurrent[(int) attribute.Type].ChangeText(attribute.CurrentValue,attribute.MaxValue);
                attributesCurrent[(int) attribute.Type].ChangeSlider(attribute.CurrentValue,attribute.MaxValue);
            }
        }

        public void ShowCollectedWeapon(WeaponsScriptableObject weaponsScriptableObject)
        {
            uiWeaponCard.gameObject.SetActive(true);
            uiWeaponCard.ReceiveData(weaponsScriptableObject,0,false);
        }

        public void PlayerHurt()
        {
            _animator.SetTrigger(Hurt);
        }

        public void ShowArtifact(ArtifactsScriptableObject newArtifact)
        {
            uiArtifactCard.gameObject.SetActive(true);
            uiArtifactCard.ReceiveData(newArtifact,0,false);
            Debug.Log("Voce coletou o artefato " + newArtifact.artifactName);
        }

        public void ShowWeaponsInventory()
        {
            if (!UiWeaponInventory.gameObject.activeSelf)
            {
                UiWeaponInventory.backButton.SetActive(false);
                UiWeaponInventory.gameObject.SetActive(true);
            }
            UiWeaponInventory.SetWeaponsData(GameManager.instance.PlayerMain._weaponHolder.WeaponsInventory);
        }

        public void ShowWeaponsInventoryBlacksmith()
        {
            HideBlacksmith();
            if (!UiWeaponInventory.gameObject.activeSelf)
            {
                UiWeaponInventory.backButton.SetActive(true);
                UiWeaponInventory.gameObject.SetActive(true);
            }
            UiWeaponInventory.SetWeaponsData(GameManager.instance.PlayerMain._weaponHolder.WeaponsInventory);
        }

        public void ShowArtifactInventory()
        {
            if (!UiArtifactInventory.gameObject.activeSelf)
            {
                UiArtifactInventory.buttonBack.SetActive(false);
                UiArtifactInventory.gameObject.SetActive(true);
            }
            UiArtifactInventory.SetArtifactData(GameManager.instance.PlayerMain._weaponHolder.ArtifactsInventory);
        }
        public void ShowArtifactInventoryBlacksmith()
        {
            HideBlacksmith();
            if (!UiArtifactInventory.gameObject.activeSelf)
            {
                UiArtifactInventory.buttonBack.SetActive(true);
                UiArtifactInventory.gameObject.SetActive(true);
            }
            UiArtifactInventory.SetArtifactData(GameManager.instance.PlayerMain._weaponHolder.ArtifactsInventory);
        }

        public void ShowBlacksmith()
        {
            blackSmithUIGameObject.SetActive(true);
        }

        public void HideBlacksmith()
        {
            Debug.Log("hide");
            blackSmithUIGameObject.SetActive(false);
        }

        public void HideWeaponsInventory()
        {
            if (UiWeaponInventory.gameObject.activeSelf)
            {
                UiWeaponInventory.gameObject.SetActive(false);
            }
        }
        public void EquipWeapon(int index)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.PlayerMain._weaponHolder.EquipWeapon(index);
                
            }
        }
        public void EquipArtifact(int index)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.PlayerMain._weaponHolder.TryEquipArtifactFromInventory(index);
            }
        }

        public void HideMainMenu()
        {
            camera.gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(false);
        }
        public void ShowMainMenu()
        {
            camera.gameObject.SetActive(true);
            _mainMenu.gameObject.SetActive(true);
        }
    }
}