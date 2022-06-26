using UnityEngine;

namespace ForestReturn.Scripts.NaoSei
{
    [CreateAssetMenu(fileName = "configLobby", menuName = "ScriptableObject/ConfigLobby", order = 0)]
    public class ConfigLobby : ScriptableObject
    {
        public bool blacksmithSaved = false;

        public GameObject blackSmithPrefab;
        public Vector3 blackSmithPosition = new Vector3(-30.27f, -0.59f,-34.28f );
        // UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-30.279998779296876,"y":-0.5999999046325684,"z":-34.28999710083008},"rotation":{"x":0.0,"y":0.5314153432846069,"z":0.0,"w":0.8471114635467529},"scale":{"x":1.0,"y":1.0,"z":1.0}}
    }
}