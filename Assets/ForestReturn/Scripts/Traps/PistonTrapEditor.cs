using UnityEditor;
using UnityEngine;

namespace ForestReturn.Scripts.Traps
{
    [CustomEditor(typeof(PistonTrap))]
    public class PistonTrapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            PistonTrap pistonTrap = (PistonTrap)target;
            if (GUILayout.Button("Stop Moving"))
            {
                pistonTrap.StopAll();
            }

            if (GUILayout.Button("StartMoving"))
            {
                pistonTrap.StartMoving();
            }
        }
        
    }
}