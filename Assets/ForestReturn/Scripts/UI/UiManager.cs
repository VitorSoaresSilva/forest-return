using System;
using Artifacts;
using Attributes;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;
using Weapons;
using Attribute = Attributes.Attribute;

namespace UI
{
    public class UiManager : PersistentSingleton<UiManager>
    {
        [System.Serializable]
        public class AttributesUI : IComparable
        {
            public AttributeType type;
            public TextMeshProUGUI text;
            public string suffix;
            public string prefix;
            public Slider slider;
            [SerializeField] private RawImage iconsLife100;
            [SerializeField] private RawImage iconsLife20;
            [SerializeField] private Sprite backgroundLife100;
            [SerializeField] private Sprite backgroundLife20;
            [SerializeField] private Sprite fillLife100;
            [SerializeField] private Sprite fillLife20;
            [SerializeField] private Image ImageBG;
            [SerializeField] private Image ImageFill;
            

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
                    if (type == AttributeType.Health && iconsLife100 != null && iconsLife20 != null)
                    {
                        if (currentValue >= (maxValue * 0.4f))
                        {
                            iconsLife100.gameObject.SetActive(true);
                            iconsLife20.gameObject.SetActive(false);
                        }
                        else
                        {
                            iconsLife100.gameObject.SetActive(false);
                            iconsLife20.gameObject.SetActive(true);
                        }
                    }
                    if (type == AttributeType.Health && backgroundLife100 != null && fillLife100 != null && backgroundLife20 != null && fillLife20 != null)
                    {
                        if (currentValue >= (maxValue * 0.4f))
                        {
                            ImageBG.sprite = backgroundLife100;
                            ImageFill.sprite = fillLife100;
                        }
                        else
                        {
                            ImageBG.sprite = backgroundLife20;
                            ImageFill.sprite = fillLife20;
                        }
                    }
                }
            }

            public int CompareTo(object obj)
            {
                return (int) type - (int) ((AttributesUI) obj)!.type;
            }
        }
        public AttributesUI[] attributesMax;
        public AttributesUI[] attributesCurrent;
        private Animator _hurtAnimator;

        [field: SerializeField] public UiWeaponInventory UiWeaponInventory { get; private set; }
        [field: SerializeField] public UiArtifactInventory UiArtifactInventory { get; private set; }
        [field: SerializeField] public UiDeathPanel UiDeathPanel { get; private set; }
        [field: SerializeField] public GameObject hurtPanel { get; private set; }
        [field: SerializeField] public GameObject hudPanel { get; private set; }
        [field: SerializeField] public UiMenu menuLobby { get; private set; }
        [field: SerializeField] public UiMenu menuLevels { get; private set; }
        // [field: SerializeField] public MainMenu MainMenu { get; private set; }
        [SerializeField] private GameObject blackSmithUIGameObject;

        [Header("Weapon Card")] 
        [SerializeField] private UiWeaponCard uiWeaponCard;
        [SerializeField] private UIArtifactCard uiArtifactCard;
        [FormerlySerializedAs("camera")] [SerializeField] private Camera _camera;
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        
        protected override void Awake()
        {
            base.Awake();
            Array.Sort(attributesMax);
            Array.Sort(attributesCurrent);
            _hurtAnimator = hurtPanel.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        }

        private void GameManagerOnOnGameStateChanged(GameState obj)
        {
            switch (obj)
            {
                case GameState.None:
                    break;
                case GameState.MainMenu:
                    break;
                case GameState.Lobby:
                case GameState.LobbySemCutscene:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Level01:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
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
            _hurtAnimator.SetTrigger(Hurt);
        }

        public void ShowArtifact(ArtifactsScriptableObject newArtifact)
        {
            uiArtifactCard.gameObject.SetActive(true);
            uiArtifactCard.ReceiveData(newArtifact,0,false);
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

        public void ShowDeathPanel()
        {
            UiDeathPanel.gameObject.SetActive(true);
            hurtPanel.gameObject.SetActive(false);
            hudPanel.SetActive(false);

        }

        public void HideAllPanel()
        {
            UiDeathPanel.gameObject.SetActive(false);
            hurtPanel.gameObject.SetActive(false);
            UiWeaponInventory.gameObject.SetActive(false);
            UiArtifactInventory.gameObject.SetActive(false);
            hudPanel.SetActive(false);
        }
        


        public void EndAnimationStart()
        {
            hudPanel.SetActive(true);
            hurtPanel.SetActive(true);
            GameManager.instance.PlayerMain.gameObject.SetActive(true);
        }
    }
}