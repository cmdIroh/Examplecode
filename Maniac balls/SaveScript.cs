using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveScript {


    public const string _binFolder = "/Bin/";
    public const string _replayFolder = "/Bin/Replays/";
    public const string _highscoreFolder = "/Bin/Highscores/";
    public const string _logFolder = "/Logs/";

    public static void Save<T>(string fileName, T data) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bf.Serialize(file, data);
        file.Close();
    }

    public static void Save<T>(string folder, string fileName, T data)
    {
        MakeDirectory(folder);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + folder + fileName);
        bf.Serialize(file, data);
        file.Close();
    }

    public static T Load<T>(string fileName) {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            Debug.Log("Loading Data");
            //Deserialize data to data variable
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            T data = (T)bf.Deserialize(file);
            file.Close();
            return data;
        }
        return default(T);
    }

    public static T Load<T>(string folder, string fileName) {
        if (Directory.Exists(Application.persistentDataPath + folder))
        if (File.Exists(Application.persistentDataPath + folder + fileName))
        {
            Debug.Log("Loading Data");
            //Deserialize data to data variable
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + folder + fileName, FileMode.Open);
            T data = (T)bf.Deserialize(file);
            file.Close();
            return data;
        }
        return default(T);

    }

    private static void MakeDirectory(string folder)
    {
        if (!Directory.Exists(Application.persistentDataPath + folder))
        {
            Directory.CreateDirectory(Application.persistentDataPath + folder);
        }
    }


}
