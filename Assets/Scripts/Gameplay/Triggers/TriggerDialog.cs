//This code is used to trigger dialogues and even actions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TriggerDialog : MonoBehaviour {

    public enum triggerType
    {
        DIALOG_CLICKME,
        DIALOG_AUTOSTART,     
        DIALOG_DELETEME     
    }

    public triggerType trigger;
    private bool autostart = false;
    private bool autostarted = false;

    private InputManager inp;
    private Movement mov;
    private GameObject player;
    private Pause _paused;
    private Flowchart f;
    private Flowchart pf;
    private Item sel;
    private static float block;
    private ItemMenu iMenu;

    private bool triggered;
    private bool check = false;

    private bool moveToMe;
    private bool moveItem;

    private bool LeMeSetAWalkToPoint = true;
    public float WalkToPoint = 0f;
    public bool LookRightIfNotLeft = true;

    public Sprite HoverSprite;
    private bool Hovered;
    private SpriteRenderer ThisSRenderer;
    public float HoverSpriteScale = 1f;
    public float HoverOffsetY = 0f;
    public float HoverOffsetX = 0f;
    private GameObject CacheElement;

    public void Start()
    {
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        mov = GameObject.Find("Character").GetComponent<Movement>();
        player = GameObject.Find("Model");
        _paused = GameObject.Find("Ingame Menu").GetComponent<Pause>();
        iMenu = GameObject.Find("ItemMenu").GetComponent<ItemMenu>();
        f  = this.GetComponent<Flowchart>();
        pf = player.GetComponent<Flowchart>();
        block = Time.time;
        ThisSRenderer = this.GetComponent<SpriteRenderer>();
        if (trigger == triggerType.DIALOG_AUTOSTART) autostart = true;
    }

    public void trigger_Dialog(bool type)
    {
        _paused.Message = true;
        check = true;
        block = Time.time;
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
        if (triggered == true)
        {
            if (Hovered == false && HoverSprite != null)
            {
                GameObject CacheElement = new GameObject();
                CacheElement.transform.parent = this.transform;
                CacheElement.name = "HoverElement";
                CacheElement.AddComponent<SpriteRenderer>();
                SpriteRenderer HoverS = CacheElement.GetComponent<SpriteRenderer>();
                HoverS.sortingLayerName = ThisSRenderer.sortingLayerName;
                HoverS.sortingOrder = ThisSRenderer.sortingOrder + 100;
                HoverS.sprite = HoverSprite;
                HoverS.transform.position = new Vector3(this.transform.position.x + HoverOffsetX, this.transform.position.y + HoverOffsetY);
                HoverS.transform.localScale = new Vector3(HoverSpriteScale, HoverSpriteScale);
                Hovered = true;
            }
        }
        else
        {
            if (Hovered == true)
            {
                Destroy(GameObject.Find("HoverElement"));
                Hovered = false;
            }
        }


        if (check == true && f.GetExecutingBlocks().Count != 0) block = Time.time;

        if (Time.time - block > 0.5 && check == true)
        {
            check = false;
            _paused.Message = false;
        }


        if (autostart == false)
        {

            if (Time.time - block < 0.5) return;

            if (mov.blockMove == false && autostart == false && (inp.PointLeft || inp.Action || inp.PointRight) && triggered == true && (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > 2.0f || LeMeSetAWalkToPoint == true)){
                mov.blockMove = true;
                moveToMe = true;
                moveItem = (inp.PointRight) ? true : false;

                if (player.transform.position.x > this.transform.position.x + WalkToPoint)
                {
                    mov.MoveDir = true;
                    mov.OwnWalk = LeMeSetAWalkToPoint;
                    mov.TurnDir = LookRightIfNotLeft;
                    mov.moveTo = this.gameObject.transform.position.x + WalkToPoint;
                }
                else
                {
                    mov.MoveDir = false;
                    mov.OwnWalk = LeMeSetAWalkToPoint;
                    mov.TurnDir = LookRightIfNotLeft;
                    mov.moveTo = this.gameObject.transform.position.x + WalkToPoint;
                }  
            
            }

            if (moveToMe == true && mov.blockMove == false)
            {
                if (LeMeSetAWalkToPoint == false || mov.OwnWalk == false) { 
                    if (trigger == triggerType.DIALOG_DELETEME)
                    {
                        player.GetComponent<SpriteRenderer>().enabled = false;
                    }

                    trigger_Dialog(moveItem);
                    moveToMe = false;
                }
            }

        }

        if (autostart == true && trigger == triggerType.DIALOG_AUTOSTART && autostarted == false)
        {
            autostarted = true;
            trigger_Dialog(false);
        }
    }


    public void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.name == "cursor" && (trigger == triggerType.DIALOG_CLICKME && trigger == triggerType.DIALOG_DELETEME) && mov.BlockDialogue == false && mov.BlockDialogueB == false)
        {
            triggered = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "cursor" && (trigger == triggerType.DIALOG_CLICKME||trigger==triggerType.DIALOG_DELETEME))
        {
            triggered = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (LeMeSetAWalkToPoint == true) { 
            Gizmos.DrawWireSphere(new Vector3(this.transform.position.x + WalkToPoint, this.transform.position.y), 1f);
        }

        if(HoverSprite != null)
        {
            Vector3 cache = new Vector3(this.transform.position.x + HoverOffsetX, this.transform.position.y + HoverOffsetY);
            Gizmos.DrawWireCube(cache, new Vector3(HoverSprite.bounds.size.x * HoverSpriteScale, HoverSprite.bounds.size.y * HoverSpriteScale));
        }
    }

}
