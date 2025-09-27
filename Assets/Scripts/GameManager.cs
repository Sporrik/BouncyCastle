using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;
    
    [SerializeField] private TextMeshProUGUI[] _scoreText;
    public int[] scoreArr = new int[2];

    [SerializeField] private TextMeshProUGUI[] _endText;

    private void Awake()
    {
        if (instance == null) instance = this;

        SpawnBall();
    }

    public void SpawnBall()
    {
        Instantiate(_ballPrefab, _ballSpawnPoint);
    }

    public void OnGoalScored(int teamID)
    {
        scoreArr[teamID]++;
        _scoreText[teamID].text = scoreArr[teamID].ToString();

        if (scoreArr[teamID] > 4)
        {
            _endText[teamID].gameObject.SetActive(true);
        }
        else
        {
            SpawnBall();
        }
    }
}
