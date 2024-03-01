using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Массив префабов препятствий
    public Transform spawnPoint; // Точка спавна препятствий
    public float spawnInterval = 2f; // Интервал между спаунами
    public float spawnTimer = 0f; // Таймер для отслеживания интервала

    void Update()
    {
        // Увеличиваем таймер на время, прошедшее с прошлого кадра
        spawnTimer += Time.deltaTime;
        
        // Если прошло достаточно времени, чтобы заспавнить препятствие
        if (spawnTimer >= spawnInterval)
        {
            SpawnObstacle(); // Вызываем метод спавна препятствия
            spawnTimer = 0f; // Сбрасываем таймер
        }
    }

    void SpawnObstacle()
    {
        // Выбираем случайный префаб из массива
        GameObject randomObstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Создаем экземпляр префаба в точке спавна с его оригинальной ориентацией
        GameObject obstacleInstance = Instantiate(randomObstaclePrefab, spawnPoint.position, spawnPoint.rotation);

        // Если в префабе есть компонент Rigidbody, задаем ему начальную скорость
        Rigidbody obstacleRigidbody = obstacleInstance.GetComponent<Rigidbody>();
        if (obstacleRigidbody != null)
        {
            obstacleRigidbody.velocity = spawnPoint.forward * obstacleRigidbody.velocity.magnitude;
        }
    }
}
