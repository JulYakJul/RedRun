using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Массив префабов препятствий
    public Transform generationPoint; // Точка, где будут создаваться препятствия
    public float minTimeBetweenObstacles; // Минимальный интервал времени между созданием препятствий
    public float maxTimeBetweenObstacles; // Максимальный интервал времени между созданием препятствий
    public float obstacleSpeed; // Скорость движения препятствий влево
    public Canvas canvas; // Ссылка на Canvas, куда будут добавляться препятствия

    void Start()
    {
        // Создаем первое препятствие
        CreateObstacle();
    }

    void Update()
    {
        // Проверка, нужно ли создать новое препятствие
        if (Time.time >= nextObstacleTime)
        {
            CreateObstacle();
        }

        // Перемещаем существующие препятствия влево и уничтожаем их, если они больше не видны на экране
        MoveAndDestroyObstacles();
    }

    // Время следующего создания препятствия
    private float nextObstacleTime;

    // Создание препятствия
    void CreateObstacle()
    {
        // Выбираем случайное время для следующего создания препятствия
        nextObstacleTime = Time.time + Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles);

        // Выбираем случайный индекс из массива префабов препятствий
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        // Создаем препятствие в пространстве Canvas
        GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], generationPoint.position, Quaternion.identity, canvas.transform);
    }

    // Перемещение существующих препятствий влево и уничтожение их, если они больше не видны на экране
    void MoveAndDestroyObstacles()
    {
        // Получаем все препятствия в пространстве Canvas
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        // Двигаем каждое препятствие влево
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.Translate(Vector3.left * obstacleSpeed * Time.deltaTime);

            // Уничтожаем препятствие, если оно больше не видно на экране
            if (obstacle.transform.position.x < Camera.main.ViewportToWorldPoint(Vector3.zero).x)
            {
                Destroy(obstacle);
            }
        }
    }
}
