using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This script will handle more complicated data types. This class is needed because Json has issues dealing with dictionaries, which again, we will use
  to handle the more complicated data that we want to store, like which levers have been activated, which chest have been open, what items has the player gotten, etc*/
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue> , ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();//will store the id's we generate 
    [SerializeField] private List<TValue> values = new List<TValue>();// will store the values we store, more than likely booleans

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }



    public void OnAfterDeserialize()
    {
        this.Clear();
        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary but the amount of keys (" + keys.Count + ") does not match" +
                " the number of values (" + values.Count + ") which indicates that something went wrong.");
        }
        
        for(int i = 0; i< keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }

}
