using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager saveManagerInstance { set; get; }
    public SaveState state;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        saveManagerInstance = this;
        Load();
    }

    //save saveState to player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", Serializer.Serialize(state));

    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = Serializer.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found");
        }
    }
}
