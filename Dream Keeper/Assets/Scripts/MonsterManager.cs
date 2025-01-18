using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Dictionary<int, Queue<GameObject>> monsterQueues = new Dictionary<int, Queue<GameObject>>();

    public GameObject testMonster;
    // public void Awake()
    // {
    //     // TODO: unity
    //     AddMonster(testMonster);
    // }
    public void AddMonster(GameObject monster)
    {
        var colers = monster.GetComponent<Monster>()._colors;
        var hash = GetEliminationCondition(colers);
        if (!monsterQueues.ContainsKey(hash))
        {
            monsterQueues.Add(hash, new Queue<GameObject>());
        }
        monsterQueues[hash].Enqueue(monster);
    }

    public bool HasMonster(Color[] colors)
    {
        var hash = GetEliminationCondition(colors);
        return monsterQueues.ContainsKey(hash) && monsterQueues[hash].Count > 0;
    }

    // return the score of the monster
    public int DestroyMonster(Color[] colors)
    {
        var hash = GetEliminationCondition(colors);
        var monster = monsterQueues[hash].Dequeue();
        var score = monster.GetComponent<Monster>()._score;
        Destroy(monster);
        Debug.Log("Destroying monster with color: " + string.Join(",", colors) );
        return score;
    }

    // TODO: unity, 找个测试按钮绑定，在monster timer时效内，点击，理论效果是monster消失，且得分
    public void Test_DestroyInTime()
    {
        Debug.Log("Click button to mock destroying monster in time");
        
        AddMonster(testMonster);
        var getScore = DestroyMonster(new Color[] { Color.Red });
        Debug.Log("Get score after destroying monster: " + getScore);
    }

    private int GetEliminationCondition(Color[] colors)
    {
        var colorStrings = colors.ToList().Select(color => color.ToString()).OrderBy(colorStr => colorStr).ToArray();
        string combinedColorString = string.Join(",", colorStrings);
        int hash = combinedColorString.GetHashCode();
        return hash;
    }
}
