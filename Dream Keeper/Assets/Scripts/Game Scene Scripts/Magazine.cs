using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] public GameObject[] _slot0Color;
    [SerializeField] public Color[] _slot0Order;
        
    [SerializeField] public GameObject[] _slot1Color;
    [SerializeField] public Color[] _slot1Order;

    [SerializeField] public GameObject[] _slot2Color;
    [SerializeField] public Color[] _slot2Order;

    [SerializeField] public GameObject[] _slot3Color;
    [SerializeField] public Color[] _slot3Order;

    private List<Dictionary<Color, GameObject>> _availableSlotColors = new List<Dictionary<Color, GameObject>>();
    private List<GameObject> _currentSlotColors = new List<GameObject>();

    private bool _initialized = false;
    private void loadAvailableColors()
    {
        this._availableSlotColors.Add(helper_addToDict(this._slot0Order, this._slot0Color));
        this._availableSlotColors.Add(helper_addToDict(this._slot1Order, this._slot1Color));
        this._availableSlotColors.Add(helper_addToDict(this._slot2Order, this._slot2Color));
        this._availableSlotColors.Add(helper_addToDict(this._slot3Order, this._slot3Color));
        //Debug.Log("loaded colors");
        //this.debug_availableColors();
    }

    public void addToMagazine(int index, Color color)
    {
        if (!this._initialized)
        {
            this.loadAvailableColors();
            this._initialized = true;
        }

        //Debug.Log("find color - index: " + index + ", color: " + color);

        GameObject colorObj = Instantiate((this._availableSlotColors[index])[color], this.transform);
        this._currentSlotColors.Add(colorObj);
    }

    public void resetMagazine()
    {
        for (int i = 0; i < this._currentSlotColors.Count; i++)
        {
            Destroy(this._currentSlotColors[i]);
        }
        
        this._currentSlotColors.Clear();
    }




    private Dictionary<Color, GameObject> helper_addToDict(Color[] colors, GameObject[] objs)
    {
        //Debug.Log("helper_addToDict");
        Dictionary<Color, GameObject> ret = new Dictionary<Color, GameObject>();

        for (int i = 0; i < colors.Length; i++)
        {
            ret[colors[i]] = objs[i];
        }

        //Debug.Log(ret.Count());
        return ret;
    }

    private void debug_availableColors()
    {
        string str = "";
        for (int i = 0; i < this._availableSlotColors.Count; i++)
        {
            str += "Box: " + i + "\n";
            foreach(var pair in this._availableSlotColors[i])
            {
                str += "Key: " + pair.Key + ", Value: " + pair.Value + "\n";
            }
            str += "\n";
        }
        Debug.Log(str);
    }
}
