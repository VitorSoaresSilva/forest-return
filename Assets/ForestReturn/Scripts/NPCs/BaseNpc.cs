using ForestReturn.Scripts.Interactable;
using UnityEngine;

namespace ForestReturn.Scripts.NPCs
{
    public interface IBaseNpc: IInteractable
    {
        public void InitOnLobby();
    }
}