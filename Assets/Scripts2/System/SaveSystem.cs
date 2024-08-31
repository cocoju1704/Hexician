using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem> {
    [SerializeField] string saveFileName;

    JsonSerializerSettings settings;
    string saveFilePath;
    protected override void Awake() {
        settings = new JsonSerializerSettings { 
            TypeNameHandling = TypeNameHandling.Auto, 
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore ,
            Converters = {  new Vector3IntDictionaryConverter() } 
        };

        saveFilePath = Path.Combine(Application.dataPath, saveFileName);
    }

    void Start() {
        Dictionary<int, int> dict = new Dictionary<int, int>();
    }

    void OnApplicationQuit() {
        // SaveFile();    
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha0)) {
            HasSaveFile();
        }    
    }

    public bool HasSaveFile() {
        return File.Exists(saveFilePath);
    }

    public bool SaveFile() {
        GameData gameData = new GameData();

        List<ISavable> savables = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>().ToList();
        foreach(ISavable savable in savables) {
            savable.Save(gameData);
        }

        string jsonFormat = JsonConvert.SerializeObject(gameData, Formatting.Indented, settings);
        
        File.WriteAllText(saveFilePath, jsonFormat);
        return true;
    }

    public bool LoadFile() {
        if(!File.Exists(saveFilePath)) {
            return false;
        }

        string jsonFormat = File.ReadAllText(saveFilePath);
        GameData gameData = JsonConvert.DeserializeObject<GameData>(jsonFormat, settings);

        List<ISavable> savables = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>().ToList();
        foreach(ISavable savable in savables) {
            savable.Load(gameData);
        }

        return true;
    }

    public bool DeleteFile() {
        if(!File.Exists(saveFilePath)) {
            return false;
        }

        File.Delete(saveFilePath);
        return true;
    }
}