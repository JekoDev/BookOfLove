using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRenderer : MonoBehaviour {

    public Item item;
    private SpriteRenderer _renderer;
    private bool isRender = false;
    private InputManager inp;
    private GameObject player;
    private Pause _paused;


    void Start () {
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        player = GameObject.Find("Model");
        _paused = GameObject.Find("Ingame Menu").GetComponent<Pause>();

        if (item.pickedUp == false)
        {
            _renderer = this.gameObject.AddComponent<SpriteRenderer>();
            _renderer.sprite = item.artwork;
            _renderer.sortingLayerName = "Objects";
            isRender = true;
        }
	}


    void Update()
    {
        if (item.pickedUp == true)
        {
            _paused.Message = false;
            Destroy(this.gameObject);
        }
    }

    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "cursor" && (inp.PointLeft || inp.Action) && Vector3.Distance(player.transform.position, this.gameObject.transform.position) < 3f)
        {
            item.pickedUp = true;
            player.GetComponent<Player>().AddItem(item);
            if (item == null) Debug.Log("....");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Vector3(3f, 3f, 3f));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Vector3(2.9f, 2.9f, 2.9f));
    }

}
