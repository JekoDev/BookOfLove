//This code is used to trigger dialogues and even actions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TriggerDialog : MonoBehaviour {

    public enum triggerType
    {
        DIALOG_CLICKME,
        DIALOG_AUTOSTART        
    }

    public triggerType trigger;
    private bool autostart = true;

    private InputManager inp;
    private GameObject player;
    private Pause _paused;
    private Flowchart f;
    private Flowchart pf;
    private Item sel;
    private float block;
    private ItemMenu iMenu;

    private bool triggered;
    private bool check = false;


    public void Start()
    {
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        player = GameObject.Find("Model");
        _paused = GameObject.Find("Ingame Menu").GetComponent<Pause>();
        iMenu = GameObject.Find("ItemMenu").GetComponent<ItemMenu>();
        f  = this.GetComponent<Flowchart>();
        pf = player.GetComponent<Flowchart>();
        block = Time.time;
    }

    public void trigger_Dialog(bool type)
    {
        iMenu.collapse = false;
        if (type == true) {
            sel = player.GetComponent<Player>().selectedItem;
            if (sel == null)
            {
                if (f.HasBlock("noItem")){
                    f.ExecuteBlock("noItem");
                }else{
                    pf.ExecuteBlock("noItem");
                }
            }else{
                if (f.HasBlock(sel.name)){
                    f.ExecuteBlock(sel.name);
                }else{
                    if (f.HasBlock("wrongItem")){
                        f.ExecuteBlock("wrongItem");
                    }else{
                        pf.ExecuteBlock("wrongItem");
                    }
                }
            }
            
        }else{ 
            if (f && f.HasBlock("IN") == true){
                f.ExecuteBlock("IN");
            }
        }
    }
    
    public void Update()
    {
        if (f.IsActive() && f.HasExecutingBlocks())
        {
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > 3f)
            {
                f.StopAllBlocks();
            }
            check = true;
            block = Time.time;
            _paused.Message = true;
        }else { 
            if (check == true){
                check = false;
                _paused.Message = false;
                if (f.HasVariable("removeItem") && f.GetBooleanVariable("removeItem") == true)
                {
                    Player cache = player.GetComponent<Player>();
                    if (f.HasVariable("removeName") && cache.selectedItem.name == f.GetStringVariable("removeName")) { 
                        cache.RemoveItem(cache.selectedItem);
                    }
                }
            }
        }


        if (autostart == true && trigger == triggerType.DIALOG_AUTOSTART) {
            autostart = false;
            trigger_Dialog(false);
        }

        if (Time.time - block > 0.8f && check==false) {
            if (triggered == true && (inp.PointRight) && Vector3.Distance(player.transform.position, this.gameObject.transform.position) < 3f)
            {
                if (this.GetComponent<ItemRenderer>() == null) { 
                    trigger_Dialog(true);
                }else{
                    trigger_Dialog(false);
                }

            }

            if (triggered == true && (inp.PointLeft || inp.Action) && Vector3.Distance(player.transform.position, this.gameObject.transform.position) < 3f)
            {
                trigger_Dialog(false);
            }
        }
    }


    public void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name == "cursor" && trigger == triggerType.DIALOG_CLICKME)
        {
            triggered = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "cursor" && trigger == triggerType.DIALOG_CLICKME)
        {
            triggered = false;
        }
    }

}
