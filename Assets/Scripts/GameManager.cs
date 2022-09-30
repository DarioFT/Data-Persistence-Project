using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public int bestScore;
    public string bestPlayer;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameRank();
    }

    [System.Serializable]
    class HighScore
    {
        public int savedScore;
        public string savedPlayer;
    }

    public void SaveGameRank(string bestPlayer, int bestScore)
    {
        HighScore data = new HighScore();

        data.savedPlayer = bestPlayer;
        data.savedScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            Debug.Log("File found");
            string json = File.ReadAllText(path);
            HighScore data = JsonUtility.FromJson<HighScore>(json);
            bestPlayer = data.savedPlayer;
            bestScore = data.savedScore;
        }
    }

}
