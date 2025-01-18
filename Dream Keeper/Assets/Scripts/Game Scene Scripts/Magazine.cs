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

    private List<Dictionary<Color, GameObject>> _availableSlotColors;
    private List<GameObject> _currentSlotColors;


    private void Awake()
    {
        this._availableSlotColors.Append(helper_addToDict(this._slot0Order, this._slot0Color));
        this._availableSlotColors.Append(helper_addToDict(this._slot1Order, this._slot1Color));
        this._availableSlotColors.Append(helper_addToDict(this._slot2Order, this._slot2Color));
        this._availableSlotColors.Append(helper_addToDict(this._slot3Order, this._slot3Color));
    }



    public void addToMagazine(int index, Color color)
    {
        GameObject colorObj = Instantiate(this._availableSlotColors[index][color]);
        this._currentSlotColors.Add(colorObj);
    }

    public void resetMagazine()
    {
        for (int i = 0; i < this._availableSlotColors.Count; i++)
        {
            Destroy(this._currentSlotColors[i]);
        }
        this._availableSlotColors.Clear();
    }




    private Dictionary<Color, GameObject> helper_addToDict(Color[] colors, GameObject[] objs)
    {
        Dictionary<Color, GameObject> ret = new Dictionary<Color, GameObject>();

        for (int i = 0; i < colors.Length; i++)
        {
            ret.Add(colors[i], objs[i]);
        }
        return ret;
    }
}
