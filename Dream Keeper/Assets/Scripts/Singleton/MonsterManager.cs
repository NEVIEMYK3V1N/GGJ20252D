using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Dictionary<int, Queue<GameObject>> monsterQueues = new Dictionary<int, Queue<GameObject>>();

    // singleton
    public static MonsterManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public void AddMonster(GameObject monster)
    {
        var colers = monster.GetComponent<Monster>()._colors;
        var hash = GetEliminationCondition(colers);
        if (!monsterQueues.ContainsKey(hash))
        {
            monsterQueues.Add(hash, new Queue<GameObject>());
        }
        monsterQueues[hash].Enqueue(monster);

        //Debug.Log("try adding monster: " + monster.GetComponent<Monster>()._colors);
    }

    public bool HasMonster(Color[] colors)
    {
        //debug_printMonsters();

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

    //private
    public int GetEliminationCondition(Color[] colors)
    {
        colors = colors.Where(color => color != Color.EMPTY).ToArray();
        var colorStrings = colors.ToList().Select(color => color.ToString()).OrderBy(colorStr => colorStr).ToArray();
        string combinedColorString = string.Join(",", colorStrings);
        int hash = combinedColorString.GetHashCode();
        return hash;
    }

    public void ResetManager()
    {
        foreach(var pair in this.monsterQueues)
        {
            foreach (GameObject monster in pair.Value)
            {
                Destroy(monster);
            }
        }

        this.monsterQueues = new Dictionary<int, Queue<GameObject>>();
    }



    private void debug_printMonsters()
    {
        string str = "";
        foreach (var pair in this.monsterQueues)
        {
            str += pair.Key.ToString() + ": ";
            str += "\n";

            foreach (GameObject monster in pair.Value)
            {
                for (int j = 0; j < monster.GetComponent<Monster>()._colors.Count(); j++)
                {
                    str += monster.GetComponent<Monster>()._colors[j] + ", ";
                }

                str += "\n";
            }
        }

        Debug.Log(str);
    }
}
