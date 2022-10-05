using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
namespace ForestReturn.Scripts.PlayerAction.Inventory
{
    [CreateAssetMenu(fileName = "new Equipped Inventory", menuName = "Items/Equipped Inventory")]
    public class EquippedObject : ScriptableObject
    {
        // [ContextMenu("Save")]
        // public void Save()
        // {
        //     string saveData = JsonUtility.ToJson(this, true);
        //     BinaryFormatter bf = new BinaryFormatter();
        //     FileStream file = File.Create(string.Concat(Application.persistentDataPath, path));
        //     bf.Serialize(file,saveData);
        //     file.Close();
        //     
        //     
        //
        //     /*
        //      // This code save the data in a binary file
        //     IFormatter formatter = new BinaryFormatter();
        //     Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create,
        //         FileAccess.Write);
        //     formatter.Serialize(stream, Container);
        //     stream.Close();
        //     */
        // }

        // [ContextMenu("Load")]
        // public void Load()
        // {
        //     if (File.Exists(string.Concat(Application.persistentDataPath, path)))
        //     {
        //         BinaryFormatter bf = new BinaryFormatter();
        //         FileStream file = File.Open(string.Concat(Application.persistentDataPath, path), FileMode.Open);
        //         JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        //         file.Close();
        //
        //         
        //         
        //         /*
        //          // this code load the data from a binary file
        //         IFormatter formatter = new BinaryFormatter();
        //         Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open,
        //             FileAccess.Read);
        //         Container = (Inventory)formatter.Deserialize(stream);
        //         stream.Close();
        //         */
        //     }
        // }
    }
}