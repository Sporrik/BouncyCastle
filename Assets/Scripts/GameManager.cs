using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;

    private void Start()
    {
        SpawnBall();  
    }
    private void SpawnBall()
    {
        Instantiate(_ballPrefab, _ballSpawnPoint);
    }
}
