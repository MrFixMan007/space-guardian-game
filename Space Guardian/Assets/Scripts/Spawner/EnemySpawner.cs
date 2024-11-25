﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnY = 4.2f; // Высота спавна
    private readonly float cellSize = 72f;

    public float spawnInterval = 5f; // Интервал между волнами врагов
    public float waveInterval = 20f; // Интервал между волнами (можно изменять)

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true) // Бесконечный цикл для спавна волн
        {
            SpawnEnemies(); // Спавним врагов
            yield return new WaitForSeconds(waveInterval); // Ждем перед следующим спавном волны
        }
    }

    void SpawnEnemies()
    {
        Camera mainCamera = Camera.main;

        // Ширина экрана в мировых координатах
        float screenWidthInWorld = 2f * mainCamera!.orthographicSize * mainCamera.aspect;

        // Размер клетки в мировых координатах
        float cellSizeInWorld = cellSize / Screen.width * screenWidthInWorld;

        // Левая граница экрана
        float minX = -screenWidthInWorld / 2f;

        // Количество клеток по ширине
        int numCells = Mathf.FloorToInt(screenWidthInWorld / cellSizeInWorld);

        for (int i = 0; i < numCells; i++)
        {
            // Центр текущей клетки
            float spawnX = minX + i * cellSizeInWorld + cellSizeInWorld / 2f;

            // шанс пропуска клетки
            if (Random.value > 0.35f)
            {
                // Выбор случайного врага
                GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                Instantiate(randomEnemy, new Vector3(spawnX, spawnY, 0), Quaternion.identity);
            }
        }
    }
}