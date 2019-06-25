using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSingleItem : MonoBehaviour {

    public Item item;
    private InputManager inp;
    private ItemMenu iMenu;
    Player player;

    void Start()
    {
        player = GameObject.Find("Model").GetComponent<Player>();
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        iMenu = GameObject.Find("ItemMenu").GetComponent<ItemMenu>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "cursor" && inp.PointLeft)
        {
            player.selectedItem = this.item;
            iMenu.collapse = false;
        }
    }
}
