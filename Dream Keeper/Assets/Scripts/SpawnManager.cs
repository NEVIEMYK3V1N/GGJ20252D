using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using System.IO;
using System;
using System.Linq;

public class MonsterObj
{
    public String _monsterName { get; set; }
    public Color[] _colors { get; set; }
    public int _score { get; set; }
    public String _spritePath { get; set; }
}

public class SpawnConfigObj
{
    public int _startPoint = 0;
    public int _endPoint = 0;
    public int _amountSpawning = 0;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public GameManager _gameManager;
    [SerializeField] public GameObject _monsterPrefabs; // TODO: unity,

    [SerializeField] public Transform[] _spawnPoints;

    [SerializeField] public String _availableMonstersFilePath;
    [SerializeField] public String _spawnLogicFilePath;

    [SerializeField] public float _spawnCd = 1;

    

    private List<SpawnConfigObj> _spawnLogics = new List<SpawnConfigObj>();
    private List<MonsterObj> _availableMonsters = new List<MonsterObj>();

    private bool _isSpawning = false;
    public float size = 5.0f;
    

    // load config files
    public void Start()
    {
        LoadAvaliableMonsters(_availableMonstersFilePath);
        loadSpawnLogic(_spawnLogicFilePath);
    }

    public void startSpawning()
    {
        this._isSpawning = true;
        StartCoroutine(continuousSpawnMonsters());
    }

    public void stopSpawning()
    {
        this._isSpawning = false;
    }

    private IEnumerator continuousSpawnMonsters()
    {
        while (_isSpawning)
        {
            yield return new WaitForSeconds(_spawnCd); // 等待timer秒
            int amount_to_spawn = -1;
            for (int i = 0; i < _spawnLogics.Count; i++)
            {
                if (this._gameManager.getScore() >= this._spawnLogics[i]._startPoint &&
                    this._gameManager.getScore() <= this._spawnLogics[i]._endPoint)
                {
                    amount_to_spawn = this._spawnLogics[i]._amountSpawning;
                    break;
                }
            }

            if (amount_to_spawn == -1)
            {
                amount_to_spawn = this._spawnLogics[this._spawnLogics.Count - 1]._amountSpawning;
            }
            if (amount_to_spawn <= 0)
            {
                amount_to_spawn = 1;
            }

            for (int i = 0; i < amount_to_spawn; i++)
            {
                SpawnNextMonster();
            }
        }
    }

    public GameObject SpawnNextMonster()
    {
        int randomIndex = UnityEngine.Random.Range(0, _availableMonsters.Count);
        var monsterObj = _availableMonsters[randomIndex];

        GameObject newMonster = Instantiate(_monsterPrefabs);

        // update all fields of the GameObject
        var image = newMonster.GetComponent<UnityEngine.UI.Image>();

        var sprite = LoadSpriteFromFile(monsterObj._spritePath);
        Debug.Log("sprite: " + sprite);
        var monster = newMonster.GetComponent<Monster>();
        if (monster != null)
        {
            monster.InitialMonster(monsterObj._monsterName, monsterObj._colors, monsterObj._score, sprite);
        }

        // set parents
        int spawn_point_idx = UnityEngine.Random.Range((int)0, (int)(this._spawnPoints.Count()));
        Transform spawn_point = this._spawnPoints[spawn_point_idx];
        newMonster.transform.SetParent(spawn_point);
        return newMonster;
    }







    private Sprite LoadSpriteFromFile(string filePath)
    {
        string fullPath = Path.Combine(Application.dataPath, filePath);
        Debug.Log("Application.dataPath: " + Application.dataPath);
        Debug.Log("fullPath: " + fullPath);
        // Read the file data
        byte[] fileData = File.ReadAllBytes(filePath);

        // Create a new Texture2D
        Texture2D texture = new Texture2D(213, 213); // Initialize with dummy width/height


        // Load the texture with the file data
        if (texture.LoadImage(fileData))
        {
            // Create a new Sprite from the texture
            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f) // Set pivot to center
            );

            return sprite;
        }
        else
        {
            Debug.LogError("Failed to load sprite from file: " + fullPath);
            return null;
        }
    }

    private Color[] ParseColors(string colorsStr)
    {
        List<Color> colors = new List<Color>();

        foreach (char colorChar in colorsStr)
        {
            switch (colorChar)
            {
                case 'r':
                    colors.Add(Color.Red);
                    break;
                case 'g':
                    colors.Add(Color.Green);
                    break;
                case 'b':
                    colors.Add(Color.Blue);
                    break;
                case 'y':
                    colors.Add(Color.Yellow);
                    break;
                default:
                    Debug.LogWarning("Unknown color character: " + colorChar);
                    break;
            }
        }

        return colors.ToArray();
    }






    private void LoadAvaliableMonsters(string filePath)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ",",
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            TrimOptions = TrimOptions.Trim,
            Mode = CsvMode.RFC4180,

        };

        // "rg" -> the monster should be destroyed by red + green bubble
        // monsterName, colors, score, spriteRenderer
        // 1,r,1,/Asserts/Images/Monster1.png
        // 2,rg,2,/Asserts/Images/Monster2.png
        using (var reader = File.OpenText(filePath))
        using (var csv = new CsvReader(reader, configuration))
        {
            while (csv.Read())
            {
                var monsterName = csv.GetField<string>(0);
                var colorsStr = csv.GetField<string>(1);
                var colors = ParseColors(colorsStr);
                var score = csv.GetField<int>(2);
                var spriteRenderer = csv.GetField<string>(3);

                Debug.Log("Monster: " + monsterName + " Colors: " + colorsStr + " Score: " + score + " Sprite: " + spriteRenderer);

                var monsterObj = new MonsterObj
                {
                    _monsterName = monsterName,
                    _colors = colors,
                    _score = score,
                    _spritePath = spriteRenderer
                };

                _availableMonsters.Add(monsterObj);
            }
        }
    }

    private void loadSpawnLogic(string filePath)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8,
            Delimiter = ",",
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            TrimOptions = TrimOptions.Trim,
            Mode = CsvMode.RFC4180,

        };

        // startScore, endScore, numberSpawning
        using (var reader = File.OpenText(filePath))
        using (var csv = new CsvReader(reader, configuration))
        {
            while (csv.Read())
            {
                int startPoints = Int32.Parse(csv.GetField<string>(0));
                int endPoints = Int32.Parse(csv.GetField<string>(1));
                int amountSpawning = Int32.Parse(csv.GetField<string>(2));

                SpawnConfigObj spawnConfigObj = new SpawnConfigObj
                {
                    _startPoint = startPoints,
                    _endPoint = endPoints,
                    _amountSpawning = amountSpawning
                };

                this._spawnLogics.Add(spawnConfigObj);
            }
        }
    }
}
