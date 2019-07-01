using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Item selectedItem;
    [HideInInspector]
    public List<Item> itemlist;

    [HideInInspector]
    public LevelFade lf;

    private void Start()
    {
        lf = GameObject.Find("LevelFade").GetComponent<LevelFade>();
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
        lf.FadeLevel(id);
    }
}
