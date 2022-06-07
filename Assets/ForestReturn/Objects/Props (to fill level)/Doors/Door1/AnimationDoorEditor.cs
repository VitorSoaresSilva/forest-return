using FMOD.Studio;
using FMODUnity;
using UnityEditor;
using UnityEngine;

namespace ForestReturn.Objects.Props__to_fill_level_.Doors.Door1
{
    [CustomEditor(typeof(Door))]
    public class AnimationDoorEditor : Editor
    {
        

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Door door = (Door)target;
            if (GUILayout.Button("Open Door"))
            {
                door.Open();
                EventInstance doorOpen = RuntimeManager.CreateInstance(door.EventPath);
                RuntimeManager.AttachInstanceToGameObject(doorOpen,door.transform);
                // doorOpen.setParameterByName();
                doorOpen.start();
                doorOpen.release();
            }
            if(GUILayout.Button("Close Door"))
            {
                door.Close();
            }
    
        }
    }
}