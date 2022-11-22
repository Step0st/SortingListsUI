using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SaveData(ListObject database)
    {
        string path = Application.dataPath + "/" + database.name +  ".json";
        string json = JsonUtility.ToJson(database);
        File.WriteAllText(path, json);
    }

    public static ListObject LoadData(ListObject database)
    {
        string path = Application.dataPath + "/" + database.name +  ".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ListObject newDatabase = ScriptableObject.CreateInstance<ListObject>();
            JsonUtility.FromJsonOverwrite(json, newDatabase);
            return newDatabase;
        }
        else
        {
            return database;
        }
    }
}