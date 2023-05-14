using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public int points;
    public int level;
    public Restart death;
    [SerializeField] private DataSaver data;

    private void Awake()
    {
        if (Time.realtimeSinceStartup <= 3f)
        {
            data.Score = 0;
            data.Level = 0;
        }
        print(Time.realtimeSinceStartup);
        points = data.Score;
        level = data.Level;
        print(level);
    }
    public void RestartGame()
    {
        level += 1;
        data.Level = level;
        data.Score = points;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
