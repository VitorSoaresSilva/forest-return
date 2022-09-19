using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ForestReturn.Scripts.PlayerAction.Utilities;
using UnityEngine;
namespace ForestReturn.Scripts.PlayerAction
{
    [CreateAssetMenu(fileName = "new Game Data", menuName = "Items/GameData")]
    public class GameDataObject : ScriptableObject
    {
        public Enums.Scenes currentLevel;
        public string path;
        public DateTime LastSave;
        public Transform pointToSpawn;

        [ContextMenu("Save")]
        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, path));
            bf.Serialize(file, saveData);
            file.Close();
        }

        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, path)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, path), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
            }
            else
            {
                currentLevel = Enums.Scenes.Intro;
                pointToSpawn = null;
            }
        }
    }
}