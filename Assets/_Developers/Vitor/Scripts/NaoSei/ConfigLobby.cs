using UnityEngine;

namespace _Developers.Vitor.Scripts.NaoSei
{
    [CreateAssetMenu(fileName = "configLobby", menuName = "ScriptableObject/ConfigLobby", order = 0)]
    public class ConfigLobby : ScriptableObject
    {
        public bool blacksmithSaved = false;

        public GameObject blackSmithPrefab;
        public Vector3 blackSmithPosition = new Vector3(-30.27f, -0.59f,-34.28f );
        public bool PlayCutsceneAnimation = true;
    }
}