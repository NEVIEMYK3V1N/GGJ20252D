using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Dictionary<int, Queue<GameObject>> monsterQueues = new Dictionary<int, Queue<GameObject>>();


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

    public void DestroyMonster(Color[] colors)
    {
        var hash = GetEliminationCondition(colors);
        var monster = monsterQueues[hash].Dequeue();
        Destroy(monster);
    }

    private int GetEliminationCondition(Color[] colors)
    {
        var colorStrings = colors.ToList().Select(color => color.ToString()).OrderBy(colorStr => colorStr).ToArray();
        string combinedColorString = string.Join(",", colorStrings);
        int hash = combinedColorString.GetHashCode();
        return hash;
    }

}
