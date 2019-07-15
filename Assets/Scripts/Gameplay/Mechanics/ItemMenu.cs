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
    private bool mouseIn;
    private Movement mov;
    private bool wait;
    private float timer;

    public AudioSource open;
    public AudioSource close;

    // Use this for initialization
    void Start () {
        offset = this.transform.position - Camera.main.transform.position;
        player = GameObject.Find("Model").GetComponent<Player>();
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        selected = GameObject.Find("SelectedItem").GetComponent<SpriteRenderer>();
        itemContainer = GameObject.Find("AllItems");
        itemBanner = GameObject.Find("ItemBanner");
        mov = GameObject.Find("Character").GetComponent<Movement>();
    }

    void Update()
    {
        if (mouseIn == true)
        {
            if (inp.PointLeft || inp.Action) collapse = !collapse;
            mov.BlockDialogue = true;
        }
        else {
            mov.BlockDialogue = false;
        }


        if (player.selectedItem != null)
        {
            if (selected.sprite != player.selectedItem.artwork)
            {
                selected.sprite = player.selectedItem.artwork;
                selected.transform.localScale *= player.selectedItem.scaleInventory;
                selected.transform.position += new Vector3(player.selectedItem.XOffsetInv, player.selectedItem.YOffsetInv);
            }
            
        }else{
            if (selected.sprite != null) selected.sprite = null;
        }

        if (collapse == true && collapsed == false)
        {
            if (wait == false)
            {
                wait = true;
                timer = Time.time;
            }

            if (Time.time - timer > 0.5f) { 
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
                    cache.transform.position = itemContainer.transform.position + new Vector3(i.XOffsetInv, i.YOffsetInv);
                    cache.transform.localScale = itemContainer.transform.localScale * i.scaleInventory;
                    cache.transform.position += new Vector3(offsetItem, 0f);
                    cache.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    cache.AddComponent<MenuSingleItem>();
                    cache.GetComponent<MenuSingleItem>().item = i;
                }
                collapsed = true;
                itemBanner.GetComponent<Animator>().SetBool("Enable", true);
                if (open != null) open.Play();
            }
        }

        if (collapse == false && collapsed == true)
        {
            collapsed = false;
            wait = false;
            int childs = itemContainer.transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.Destroy(itemContainer.transform.GetChild(i).gameObject);
            }
            itemBanner.GetComponent<Animator>().SetBool("Enable", false);
            if (close != null) close.Play();
        }

    }

    void LateUpdate () {
        this.transform.position = Camera.main.transform.position + offset;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "cursor") mouseIn = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "cursor") mouseIn = false;
    }
}
