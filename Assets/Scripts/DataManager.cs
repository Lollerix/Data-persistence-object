using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string playerName;
    public List<ScoreObj> leaderboard = new List<ScoreObj>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadLeaderboard();
    }

    public void SaveLeaderboard()
    {
        SaveData data = new SaveData();
        data.leaderboard = leaderboard;

        string json = JsonUtility.ToJson(data);


        File.WriteAllText(Application.persistentDataPath + "/leaderboard.json", json);
    }

    public void LoadLeaderboard()
    {
        string path = Application.persistentDataPath + "/leaderboard.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            leaderboard = data.leaderboard;
        }
    }
    public void addScore(string playerName, int score)
    {
        ScoreObj nScore = new ScoreObj(playerName, score);
        int x = SearchPrevious(nScore);
        if (x == -1 || leaderboard[x].score < nScore.score)
        {
            if (x >= 0)
            {
                leaderboard.RemoveAt(x);
            }
            leaderboard.Add(nScore);
            SortLeaderboard();
        }
    }
    private void SortLeaderboard()
    {
        leaderboard.Sort((x, y) => y.score.CompareTo(x.score));
    }
    private int SearchPrevious(ScoreObj prev)
    {
        for (int x = 0; x < leaderboard.Count; x++)
        {
            ScoreObj item = leaderboard[x];
            if (item.name.Equals(prev.name))
            {
                return x;
            }
        }
        return -1;
    }

    [System.Serializable]
    public class SaveData
    {

        public List<ScoreObj> leaderboard = new List<ScoreObj>();

    }
    [System.Serializable]
    public class ScoreObj
    {
        public string name;
        public int score;
        public ScoreObj(string nName, int nScore)
        {
            name = nName;
            score = nScore;
        }
    }


}
