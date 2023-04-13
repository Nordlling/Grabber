using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelSaver
{

    public static SaveData Init()
    {
        SaveData saveData = new SaveData();
        Dictionary<int, LevelInfo> _levelMap = new Dictionary<int, LevelInfo>();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            _levelMap.Add(i, new LevelInfo());
        }

        saveData.LevelMap = _levelMap;
        saveData.SelectedLevel = 1;
        return saveData;
    }
    
    public static void SaveData(SaveData saveData)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/save.dat");
            bf.Serialize(file, saveData);
            file.Close();
            Debug.Log("Saved");
        } catch (System.Exception e)
        {
            Debug.LogError("Save data error: " + e.Message);
        }
    }
    

    public static SaveData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Taken");
            return saveData;
        }
        else
        {
            Debug.LogError("Save data not found. Created new");
            return Init();
        }
    }
}
