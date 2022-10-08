using _Developers.Vitor.Scripts.Character;
using UnityEngine;

namespace Bagunca.Organizar
{
    public class TestePlayer : BaseCharacter
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("batata");
        }
    }
}
