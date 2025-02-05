using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject meteorPrefab;
    public GameObject metalMeteorPrefab;
    public GameObject speedUpPrefab;
    public GameObject sizeUpPrefab;
    public GameObject moreWeaponsPrefab;
    public GameManager manager;
    public MeteorMover meteorMover;
    public MeteorMover metalMeteorMover;
    private float minSpawnDelay = 1f;
    private float maxSpawnDelay = 3f;
    public float spawnXLimit = 3.2f;

    float timeHasPassed = 0f;
    int bonusCount = 0;
    int difficultyCount = 0;
    void Start()
    {
        Spawn();
        MetalMeteorSpawn();
    }

    void Spawn()
    {
        float random = Random.Range(-spawnXLimit, spawnXLimit);
        Vector3 spawnPos = transform.position + new Vector3(random, 0f, 0f);

        float randomSize = Random.Range(1f, 3f);
        meteorPrefab.transform.localScale = new Vector3(randomSize, randomSize, 0);

        Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void MetalMeteorSpawn()
    {
        float random = Random.Range(-spawnXLimit, spawnXLimit);
        Vector3 spawnPos = transform.position + new Vector3(random, 0f, 0f);

        Instantiate(metalMeteorPrefab, spawnPos, Quaternion.identity);

        Invoke("MetalMeteorSpawn", 50f);
    }

    private void Update()
    {
        DifficultyAndPowerUpScale();
    }

    private void DifficultyAndPowerUpScale()
    {
        timeHasPassed += Time.deltaTime;
        float random = Random.Range(-spawnXLimit, spawnXLimit);
        Vector3 spawnPos = transform.position + new Vector3(random, 0f, 0f);

        //PowerUps
        if (System.Math.Round(timeHasPassed) == 30 && bonusCount == 0)
        {
            Instantiate(speedUpPrefab, spawnPos, Quaternion.identity);
            bonusCount++;
        }
        else if (System.Math.Round(timeHasPassed) == 80 && bonusCount == 1)
        {
            Instantiate(moreWeaponsPrefab, spawnPos, Quaternion.identity);
            bonusCount++;
        }
        else if (System.Math.Round(timeHasPassed) == 120 && bonusCount == 2)
        {
            Instantiate(moreWeaponsPrefab, spawnPos, Quaternion.identity);
            bonusCount++;
        }
        else if (System.Math.Round(timeHasPassed) == 180 && bonusCount == 3)
        {
            Instantiate(speedUpPrefab, spawnPos, Quaternion.identity);
            bonusCount++;
        }
        else if (System.Math.Round(timeHasPassed) == 240 && bonusCount == 4)
        {
            Instantiate(moreWeaponsPrefab, spawnPos, Quaternion.identity);
            bonusCount++;
        }

        //MeteorUpgrade
        if (System.Math.Round(timeHasPassed) == 30 && difficultyCount == 0)
        {
            meteorMover.SpeedUp();
            metalMeteorMover.SpeedUp();
            minSpawnDelay = 0f;
            maxSpawnDelay = 2f;
            difficultyCount++;
        }
        else if (System.Math.Round(timeHasPassed) == 100 && difficultyCount == 1)
        {
            maxSpawnDelay = 1f;
            meteorMover.SpeedUp();
            metalMeteorMover.SpeedUp();
            difficultyCount++;
        }
        else if (System.Math.Round(timeHasPassed) == 160 && difficultyCount == 2)
        {
            maxSpawnDelay = 0.5f;
            meteorMover.SpeedUp();
            metalMeteorMover.SpeedUp();
            difficultyCount++;
        }
        else if (System.Math.Round(timeHasPassed) == 220 && difficultyCount == 3)
        {
            maxSpawnDelay = 0.2f;
            meteorMover.SpeedUp();
            metalMeteorMover.SpeedUp();
            difficultyCount++;
        }
    }
}
