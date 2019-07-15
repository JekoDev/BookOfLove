using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour {

    public Item selectedItem;
    [HideInInspector]
    public List<Item> itemlist;

    [HideInInspector]
    public LevelFade lf;

    [HideInInspector]
    public Movement move;

    [HideInInspector]
    public Dictionary<string, string> save = new Dictionary<string, string>();

    private PostProcessTweak ppt;

    private void Start()
    {
        lf = GameObject.Find("LevelFade").GetComponent<LevelFade>();
        move = GameObject.Find("Character").GetComponent<Movement>();
        move.offsetStart = GameM.playerX;

        itemlist = new List<Item>(GameM.itemlist);
        selectedItem = GameM.selected;
        save = new Dictionary<string, string>(GameM.save);
        ppt = GameObject.Find("World").GetComponent<PostProcessTweak>();
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
        save[field] = value;
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

    public void DisableSpriteRenderer(bool State)
    {
        this.GetComponent<SpriteRenderer>().enabled = !State;
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

    public void SetBloom(float bloom)
    {
        
    }

    public void EnterFlashback()
    {
        Camera.main.GetComponent<PostProcessingBehaviour>().enabled = true;
    }

    public void LeaveFlashback()
    {
        Camera.main.GetComponent<PostProcessingBehaviour>().enabled = false;
    }

    public void StatueThing(int nr)
    {
        GameObject cache = GameObject.Find("Statue");

        if (cache != null)
        {
            cache.GetComponent<Animator>().SetInteger("Statue", nr);
            if (nr<3) nr++;
            SaveValue("StatueHealth", nr.ToString());
        }
        else {
            Debug.Log("DAFUQ?");
        }
    }
}
