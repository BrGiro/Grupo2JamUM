using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public float laneTopY = 1f;
    public float laneBottomY = -1f;
    public float spawnX = 10f;
    public float despawnX = -10f;

    public GameObject obstaclePrefab;

    public float spawnInterval = 1.5f;
    public float minInterval = 0.5f;
    public float difficultyRate = 0.1f;

    private float timer;

    void Start()
    {
        StartCoroutine(IncreaseDifficulty());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {

        float y = Random.Range(0f, 2f) > 1f ? laneTopY : laneBottomY; //elije el lane

        GameObject obj = Instantiate(obstaclePrefab, new Vector2(spawnX, y), Quaternion.identity);
        var obstacle = obj.GetComponent<Obstacle>();
        obstacle.speed = CalculateSpeed();
        obstacle.Init(despawnX);
    }

    float CalculateSpeed()
    {
        return Mathf.Lerp(5f, 12f, 1f - (spawnInterval - 0.5f) / 1f);
    }

    IEnumerator IncreaseDifficulty()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            spawnInterval = Mathf.Max(minInterval, spawnInterval - difficultyRate);
        }
    }
}
