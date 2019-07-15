using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSingleItem : MonoBehaviour {

    public Item item;
    private InputManager inp;
    private ItemMenu iMenu;
    Player player;
    private bool mouseIn;
    private Movement mov;

    void Start()
    {
        player = GameObject.Find("Model").GetComponent<Player>();
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        iMenu = GameObject.Find("ItemMenu").GetComponent<ItemMenu>();
        mov = GameObject.Find("Character").GetComponent<Movement>();
    }

    void Update()
    {
        if(mouseIn && inp.PointLeft)
        {
            player.selectedItem = this.item;
            iMenu.collapse = false;
            mov.BlockDialogueB = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "cursor") mouseIn = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "cursor")
        {
            mouseIn = false;
            mov.BlockDialogueB = false;
        }

    }

    private void OnDestroy()
    {
        mov.BlockDialogueB = false;
    }

}
