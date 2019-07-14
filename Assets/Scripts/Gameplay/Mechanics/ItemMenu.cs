using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour {
    private Vector3 offset;
    Player player;
    SpriteRenderer selected;
    private InputManager inp;
    private bool collapsed;
    public bool collapse;
    GameObject itemContainer;
    GameObject itemBanner;
    private float offsetItem;

    // Use this for initialization
    void Start () {
        offset = this.transform.position - Camera.main.transform.position;
        player = GameObject.Find("Model").GetComponent<Player>();
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        selected = GameObject.Find("SelectedItem").GetComponent<SpriteRenderer>();
        itemContainer = GameObject.Find("AllItems");
        itemBanner = GameObject.Find("ItemBanner");
    }

    void Update()
    {
        if (player.selectedItem != null)
        {
            if (selected.sprite != player.selectedItem.artwork)
            {
                selected.sprite = player.selectedItem.artwork;
            }
            
        }else{
            if (selected.sprite != null) selected.sprite = null;
        }

        if (collapse == true && collapsed == false)
        {
            offsetItem = 0;
            foreach (Item i in player.itemlist)
            {
                offsetItem += 1.5f;
                GameObject cache = new GameObject("_item");
                cache.transform.parent = itemContainer.transform;
                cache.AddComponent<Rigidbody2D>();
                cache.AddComponent<BoxCollider2D>();
                cache.AddComponent<SpriteRenderer>();
                cache.GetComponent<SpriteRenderer>().sprite = i.artwork;
                cache.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                cache.transform.position = itemContainer.transform.position;
                cache.transform.localScale = itemContainer.transform.localScale;
                cache.transform.position += new Vector3(offsetItem, 0f);
                cache.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                cache.AddComponent<MenuSingleItem>();
                cache.GetComponent<MenuSingleItem>().item = i;
            }
            collapsed = true;
            itemBanner.GetComponent<Animator>().SetBool("Enable", true);
        }

        if (collapse == false && collapsed == true)
        {
            collapsed = false;
            int childs = itemContainer.transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.Destroy(itemContainer.transform.GetChild(i).gameObject);
            }
            itemBanner.GetComponent<Animator>().SetBool("Enable", false);
        }

    }

    void LateUpdate () {
        this.transform.position = Camera.main.transform.position + offset;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "cursor" && (inp.PointLeft || inp.Action))
        {
            collapse = !collapse;
        }
    }
}
