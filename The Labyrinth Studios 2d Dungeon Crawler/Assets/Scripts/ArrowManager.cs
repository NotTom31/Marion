using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public void LoadData(GameData data)
    {
        this.arrowCount = data.arrowCount;
        arrowText.text = arrowCount.ToString(); //Sets initial UI key count
    }
    public void SaveData(GameData data)
    {
        data.arrowCount = this.arrowCount;
    }

    public static ArrowManager instance;
    public UnityEngine.UI.Text arrowText;

    public int arrowCount = 0;

    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        arrowText.text = arrowCount.ToString(); //Sets initial UI key count
    }

    public void AddArrow()
    {
        arrowCount += 10;
        arrowText.text = arrowCount.ToString(); //update UI
    }

    public void SubtractArrow()
    {
        if (arrowCount >= 1)
        {
            arrowCount -= 1;
            arrowText.text = arrowCount.ToString(); //update UI
        }
        else
            Debug.Log("Key count is at 0");

    }
}
