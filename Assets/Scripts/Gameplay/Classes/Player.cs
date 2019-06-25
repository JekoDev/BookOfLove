using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Item selectedItem;
    [HideInInspector]
    public List<Item> itemlist;

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

    public void AddItem(Item item)
    {
        itemlist.Add(item);
        if (selectedItem == null) selectedItem = item;
    }

    public void RemoveItem(Item item)
    {
        itemlist.Remove(item);
        if (selectedItem == item) selectedItem = null;
    }
}
