using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Item selectedItem;
    [HideInInspector]
    public List<Item> itemlist;

    [HideInInspector]
    public LevelFade lf;

    [HideInInspector]
    public Movement move;

    [HideInInspector]
    public Dictionary<string, string> save;

    private void Start()
    {
        lf = GameObject.Find("LevelFade").GetComponent<LevelFade>();
        move = GameObject.Find("Character").GetComponent<Movement>();
        move.offsetStart = GameM.playerX;

        itemlist = new List<Item>(GameM.itemlist);
        selectedItem = GameM.selected;
        save = new Dictionary<string, string>(GameM.save);
    }

    // Update is called once per frame
    void Update () {
		if (selectedItem == null && itemlist.Count != null)
        {
            foreach(Item i in itemlist)
            {
                selectedItem = i;
                break;
            }
        }
	}

    public void SaveValue(string field, string value)
    {
        save.Add(field, value);
    }

    public string LoadValue(string field)
    {
        string mapValue;
        if (save.TryGetValue(field, out mapValue))
        {
            return mapValue;
        }
        return "";
    }



    public void RemoveItem(Item item)
    {
        itemlist.Remove(item);
        if (selectedItem == item) selectedItem = null;
    }

    public void AddItem(Item item)
    {
        if (!HasItem(item)) { 
            itemlist.Add(item);
        }
    }

    public bool HasItem(Item item)
    {
        foreach (Item i in itemlist)
        {
            if (i.name == item.name) return true;
        }
        return false;
    }


    public void ChangeLevel(int id, float position)
    {
        GameM.playerX = position;
        GameM.itemlist = itemlist;
        GameM.selected = selectedItem;
        GameM.save = save;

        lf.FadeLevel(id);
    }
}
