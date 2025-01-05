using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BalloonGameManager : MonoBehaviour
{
    public UnityEvent<int> OnScoreChange = new();
    public UnityEvent OnGameEnd = new();

    [SerializeField] private GameObject balloonPrefab;
    [SerializeField] private Transform balloonSpawnArea;

    private int score;
    private int gameDurationSeconds = 10;
    private float timeBetweenBalloons = 0.5f;

    private Coroutine BalloonSpawnCoroutine;
    private Coroutine GameTimerCoroutine;

    public void StartGame()
    {
        SetScore(0);
        BalloonSpawnCoroutine = StartCoroutine("SpawnBalloons");
        GameTimerCoroutine = StartCoroutine("GameTimer");
    }

    private void SpawnBalloon()
    {
        Vector3 spawnPosition = GetRandomPointInArea(balloonSpawnArea);
        GameObject balloonInstance = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
        if (balloonInstance.TryGetComponent(out Balloon balloon))
        {
            balloon.BalloonPoppedEvent.AddListener(OnBalloonPopped);
        }
    }

    private Vector3 GetRandomPointInArea(Transform area)
    {
        return area.position + 0.5f * new Vector3(
            Random.Range(-area.lossyScale.x, area.lossyScale.x),
            Random.Range(-area.lossyScale.y, area.lossyScale.y),
            Random.Range(-area.lossyScale.z, area.lossyScale.z));
    }

    private void OnBalloonPopped()
    {
        AddToScore(5);
    }

    private void AddToScore(int amount)
    {
        score += amount;
        OnScoreChange.Invoke(score);
    }

    private void SetScore(int amount)
    {
        score = amount;
        OnScoreChange.Invoke(score);
    }

    public void EndGame()
    {
        if (BalloonSpawnCoroutine != null)
        {
            StopCoroutine(BalloonSpawnCoroutine);
            BalloonSpawnCoroutine = null;
        }

        if (GameTimerCoroutine != null)
        {
            StopCoroutine(GameTimerCoroutine);
            GameTimerCoroutine = null;
        }

        OnGameEnd.Invoke();
    }

    private IEnumerator GameTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameDurationSeconds);
            EndGame();
            break;
        }
    }

    private IEnumerator SpawnBalloons()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenBalloons);
            SpawnBalloon();
        }
    }
}
