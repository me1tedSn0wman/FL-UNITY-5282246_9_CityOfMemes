using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public interface ISaveable {
    public string OnSave();
    public void OnLoad(string data);
}

[Serializable]
public class SaveableObjectDef {
    public int id;
    public GameObject go;


    public SaveableObjectDef(int id, GameObject go) { 
        this.id= id;
        this.go = go;
    }

    public string Save() {
        if (go.GetComponent<ISaveable>()==null) Debug.Log("saveable is null");
        string data = id.ToString()+ "%" + go.GetComponent<ISaveable>().OnSave();
        return data;
    }

    public void Load(string data) {
        go.GetComponent<ISaveable>().OnLoad(data);
    }
}

public class SaveManager : MonoBehaviour
{
    public List<SaveableObjectDef> saveableObjectDefs;

    public string dataOverall;

    public void Save() {

        dataOverall = "";
        for (int i = 0; i < saveableObjectDefs.Count; i++) {
            dataOverall += saveableObjectDefs[i].Save() + "$";
        }
        YandexGame.savesData.savesDataStr = dataOverall;
        GameManager.Instance.SaveGameData();
    }

    public void Load() {
        if (!GameManager.dataIsLoaded) return;

        dataOverall = YandexGame.savesData.savesDataStr;
//        Debug.LogAssertion("Save: " + dataOverall);
        if (dataOverall.Length < 10) return;
        if (dataOverall.Equals("")) return;
        string[] lines = dataOverall.Split("$");
        for (int i = 0; i < lines.Length; i++) {
            int index = lines[i].IndexOf("%");
            if (index == -1) continue;

            int id = -1;
            Debug.Log(lines[i].Substring(0, index));
            Debug.Log(lines[i].Substring(index + 1));
            if (int.TryParse(lines[i].Substring(0, index), out id)) {

                TryFindObjectToLoad(id, lines[i].Substring(index + 1));
            }
        }
    }

    public void TryFindObjectToLoad(int id, string data) {
        for (int i = 0; i < saveableObjectDefs.Count; i++) {
            if (id == saveableObjectDefs[i].id) {
                saveableObjectDefs[i].Load(data);
                return;
            }
        }
    }


    public void GenerateListOfObjects() {
        int id = 0;
        saveableObjectDefs.Clear();
        GameObject[] gameObjs = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

        foreach (GameObject go in gameObjs) {
            if (go == null)
                continue;
            ISaveable saveable = go.GetComponent<ISaveable>();
            if(saveable==null)
                continue;

            saveableObjectDefs.Add(new SaveableObjectDef(
                id,
                go
                ));
            id++;
        }
    }
}
