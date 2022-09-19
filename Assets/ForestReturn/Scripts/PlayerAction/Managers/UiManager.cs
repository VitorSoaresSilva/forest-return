using System;
using ForestReturn.Scripts.PlayerAction.UI;
using UnityEngine;
using Utilities;

namespace ForestReturn.Scripts.PlayerAction
{
    public class UiManager : Singleton<UiManager>
    {
        [SerializeField] private Animator _hurtAnimator;
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        [SerializeField] private DisplayInventory displayInventory;
        [SerializeField] private GameObject hud;
        public void PlayerHurt()
        {
            _hurtAnimator.SetTrigger(Hurt);
        }

        public void OpenCanvas(CanvasType canvasType)
        {
            switch (canvasType)
            {
                case CanvasType.Inventory:
                    hud.SetActive(false);
                    displayInventory.gameObject.SetActive(true);
                    break;
                case CanvasType.Hud:
                    hud.SetActive(true);
                    displayInventory.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }
    }



    public enum CanvasType
    {
        Inventory,
        Hud,
    }
}