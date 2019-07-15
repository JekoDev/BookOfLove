using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName ="BookOfLove/Item...")]
public class Item : ScriptableObject, ISerializationCallbackReceiver {

    public new string name;
    public Sprite artwork;
    public string description;
    public bool pickedUp;

    public float scaleScene = 1.0f;
    public float scaleInventory = 1.0f;
    public float XOffsetInv = 0.0f;
    public float YOffsetInv = 0.0f;


    public void OnBeforeSerialize()
    {
        pickedUp = false;
    }

    public void OnAfterDeserialize()
    {
    }
}

