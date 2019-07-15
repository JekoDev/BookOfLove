//This Code is used for storing information between scenes

using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class GameM
{
    private static float PlayerX;
    private static List<Item> Itemlist;
    private static Item Selected;
    private static Dictionary<string, string> Save = new Dictionary<string, string>();
    private static bool Skipintro;
    private static int Level;

    public static SaveLoad saved = new SaveLoad();

    public static Dictionary<string, string> save
    {
        get{
            return Save;
        }
        set{
            Save = new Dictionary<string, string>(value);
        }
    }

    public static bool skipintro
    {
        get
        {
            return Skipintro;
        }
        set
        {
            Skipintro = value;
        }
    }

    public static int level
    {
        get
        {
            return Level;
        }
        set
        {
            Level = value;
        }
    }

    public static float playerX
    {
        get
        {
            return PlayerX;
        }
        set
        {
            PlayerX = value;
        }
    }

    public static Item selected
    {
        get
        {
            return Selected;
        }
        set
        {
            Selected = value;
        }
    }

    public static List<Item> itemlist
    {
        get
        {
            return Itemlist;
        }
        set
        {
            Itemlist = value;
        }
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            saved = (SaveLoad)bf.Deserialize(file);

            Itemlist = saved.Itemlist;
            Selected = saved.Selected;
            PlayerX  = saved.PlayerX;
            Save     = new Dictionary<string, string>(saved.Save);
            Level    = saved.Level;

            file.Close();

            SceneManager.LoadScene(Level);
        }
    }

    public static bool IsLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            return true;
        }
        else {
            return false;
        }
    }

    public static void SaveGame()
    {
        SaveLoad cache = new SaveLoad();
        cache.Itemlist = Itemlist;
        cache.Selected = Selected;
        cache.PlayerX  = PlayerX;
        cache.Level    = Level;
        cache.Save     = new Dictionary<string, string>(Save);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, cache);
        file.Close();
    }
}