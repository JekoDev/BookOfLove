using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad{

    public float PlayerX;
    public List<Item> Itemlist;
    public Item Selected;
    public Dictionary<string, string> Save = new Dictionary<string, string>();
    public bool Skipintro;
    public int  Level;

}
