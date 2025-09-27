using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;
    
    [SerializeField] private TextMeshProUGUI[] _scoreText;
    public int[] scoreArr = new int[2];

    private void Awake()
    {
        if (instance == null) instance = this;

        SpawnBall();
    }

    private void SpawnBall()
    {
        Instantiate(_ballPrefab, _ballSpawnPoint);
    }

    public void OnGoalScored(int teamID)
    {
        scoreArr[teamID]++;
        _scoreText[teamID].text = scoreArr[teamID].ToString();

        SpawnBall();
    }
}
