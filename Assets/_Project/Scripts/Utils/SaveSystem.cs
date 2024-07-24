using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData(Character  character)
    {
        CharacterData _data = new CharacterData(character);

        string json = JsonUtility.ToJson(_data);
        string path = Application.persistentDataPath + "/playerData.json";
        System.IO.File.WriteAllText(path,json);
    }

    public static CharacterData LoadData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            CharacterData loadedData = JsonUtility.FromJson<CharacterData>(json);


            return loadedData;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);


            return null;
        }
    }
}
