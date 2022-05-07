using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour, IDataPersistence
{
    public void LoadData(GameData data)
    {
        this.KeyCount = data.keyCount;
        keyText.text = KeyCount.ToString(); //Sets initial UI key count
    }
    public void SaveData(GameData data)
    {
        data.keyCount = this.KeyCount;
    }

    public static KeyManager instance;
    public UnityEngine.UI.Text keyText;

    public int KeyCount = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        keyText.text = KeyCount.ToString(); //Sets initial UI key count
    }

    public void AddKey()
    {
        KeyCount += 1;
        keyText.text = KeyCount.ToString(); //update UI
    }

    public void SubtractKey()
    {
        if (KeyCount >= 1)
        {
            KeyCount -= 1;
            keyText.text = KeyCount.ToString(); //update UI
        }
        else
            Debug.Log("Key count is at 0");

    }
}
