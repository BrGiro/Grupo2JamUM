using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VientitosManager : MonoBehaviour
{
    public float laneTopY = 5f;
    public float laneBottomY = -5f;
    public float spawnX = 0f;
    public float spawnXOffset = 100f;
    public float despawnX = -100f;

    public GameObject obstaclePrefab;
    [SerializeField] personajePath3 player;

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
        spawnX = player.transform.position.x + 15;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {

        float y = Random.Range(laneBottomY, laneTopY);

        GameObject obj = Instantiate(obstaclePrefab, new Vector2(spawnX, y), Quaternion.identity);
        //Debug.Log("SpawnPoint = " +  obj.transform.position);
        var obstacle = obj.GetComponent<Vientitos>();
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
