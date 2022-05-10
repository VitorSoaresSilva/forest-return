using UnityEditor;
using UnityEngine;

namespace ForestReturn.Objects.Props__to_fill_level_.Doors.Door1
{
    [CustomEditor(typeof(AnimationDoor))]
    public class AnimationDoorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            AnimationDoor animationDoor = (AnimationDoor)target;
            if (GUILayout.Button("Open Door"))
            {
                animationDoor.Open();
            }
            if(GUILayout.Button("Close Door"))
            {
                animationDoor.Close();
            }

        }
    }
}