using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] ObstaclesSpawnPoint1; // Массив препятствий для первой точки спавна
    public Camera mainCamera; // Ссылка на основную камеру
    public Transform spawnPoint1; // Первая точка спавна
    public float spawnInterval; // Интервал между спаунами препятствий
    public float obstacleLifetime; // Время жизни препятствия
    public float interval;
    public Color[] colors; // Массив цветов для изменения

    private bool spawning = false; // Флаг, указывающий, идет ли процесс спавна в данный момент
    private List<GameObject> spawnedObstacles = new List<GameObject>(); // Список сгенерированных препятствий
    private List<GameObject> shuffledObstacles = new List<GameObject>(); // Перемешанный список препятствий

    private void Start()
    {
        if (!spawning) // Проверяем, не идет ли уже процесс спавна
        {
            StartCoroutine(SpawnObstacles());
        }
    }

    IEnumerator SpawnObstacles()
    {
        spawning = true; // Устанавливаем флаг, что процесс спавна идет
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (ObstaclesSpawnPoint1.Length == 0 || mainCamera == null || spawnPoint1 == null)
            {
                Debug.LogWarning("One or more spawn points or obstacle arrays are not assigned!");
                yield break;
            }

            List<GameObject> obstacleList = new List<GameObject>(ObstaclesSpawnPoint1);
            SpawnFromPoint(obstacleList, spawnPoint1);
            yield return new WaitForSeconds(interval + 0.1f); // Adding a small delay before spawning from the second point
        }
    }

    private void SpawnFromPoint(List<GameObject> obstacles, Transform spawnPoint)
    {
        // Получаем границы видимой области камеры в мировых координатах
        Vector2 minViewportBounds = new Vector2(0f, 0f);
        Vector2 maxViewportBounds = new Vector2(1f, 1f);
        Vector3 minWorldBounds = mainCamera.ViewportToWorldPoint(new Vector3(minViewportBounds.x, minViewportBounds.y, 0));
        Vector3 maxWorldBounds = mainCamera.ViewportToWorldPoint(new Vector3(maxViewportBounds.x, maxViewportBounds.y, 0));

        GameObject obstaclePrefab = obstacles[0]; // Берем первое препятствие из перемешанного списка

        // Генерируем случайную позицию в пределах видимой области камеры или в пределах выбранной точки спавна
        Vector3 randomPosition = spawnPoint.position != null ? spawnPoint.position : new Vector3(Random.Range(minWorldBounds.x, maxWorldBounds.x), Random.Range(minWorldBounds.y, maxWorldBounds.y), 0f);
        GameObject obstacle = Instantiate(obstaclePrefab, randomPosition, Quaternion.identity);

        // Применяем случайный цвет из массива цветов
        SpriteRenderer renderer = obstacle.GetComponent<SpriteRenderer>();
        if (renderer != null && colors.Length > 0)
        {
            // Генерируем случайный индекс цвета из массива
            int randomColorIndex = Random.Range(0, colors.Length);
            Color randomColor = colors[randomColorIndex];
            renderer.color = randomColor;
        }

        // Удаляем использованное препятствие из перемешанного списка
        obstacles.RemoveAt(0);

        // Добавляем сгенерированное препятствие в список
        spawnedObstacles.Add(obstacle);

        // Уничтожаем препятствие через заданное время
        Destroy(obstacle, obstacleLifetime);
    }
}
