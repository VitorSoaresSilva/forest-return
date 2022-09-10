using UnityEngine;
using Utilities;

namespace ForestReturn.Scripts.PlayerAction
{
    public class UiManager : Singleton<UiManager>
    {
        [SerializeField] private Animator _hurtAnimator;
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        public void PlayerHurt()
        {
            _hurtAnimator.SetTrigger(Hurt);
        }
    }
}