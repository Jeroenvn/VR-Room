using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BalloonGameManager : MonoBehaviour
{
    public UnityEvent<int> OnScoreChange = new();
    public UnityEvent OnGameEnd = new();

    [SerializeField] private GameObject[] balloonPrefabs;
    [SerializeField] private Transform balloonSpawnArea;

    private int score;
    [SerializeField] private int gameDurationSeconds = 10;
    [SerializeField] private float timeBetweenBalloons = 0.5f;

    private Coroutine BalloonSpawnCoroutine;
    private Coroutine GameTimerCoroutine;

    private List<GameObject> balloonInstances = new();

    public void StartGame()
    {
        SetScore(0);
        BalloonSpawnCoroutine = StartCoroutine("SpawnBalloons");
        GameTimerCoroutine = StartCoroutine("GameTimer");
    }

    private void SpawnBalloon()
    {
        Vector3 spawnPosition = GetRandomPointInArea(balloonSpawnArea);
        GameObject balloonPrefab = balloonPrefabs[Random.Range(0, balloonPrefabs.Length)];
        GameObject balloonInstance = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
        balloonInstances.Add(balloonInstance);
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

        int balloonsLeft = balloonInstances.Count;
        for (int i = 0; i < balloonsLeft; i++)
        {
            GameObject balloon = balloonInstances[0];
            balloonInstances.RemoveAt(0);
            Destroy(balloon);
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
