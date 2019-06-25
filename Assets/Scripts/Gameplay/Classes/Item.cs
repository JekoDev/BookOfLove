using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName ="BookOfLove/Item...")]
public class Item : ScriptableObject, ISerializationCallbackReceiver {

    public new string name;
    public Sprite artwork;
    public string description;
    public bool pickedUp;


    public void OnBeforeSerialize()
    {
        pickedUp = false;
    }

    public void OnAfterDeserialize()
    {
    }
}

