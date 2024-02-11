using System.Collections;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstaclePrefabs1; // Array of obstacle prefabs for the first point
    public GameObject[] obstaclePrefabs2; // Array of obstacle prefabs for the second point
    public GameObject[] obstaclePrefabs3; // Array of obstacle prefabs for the third point
    public GameObject coinPrefab; // Coin prefab

    public Transform generationPoint1; // First point where obstacles will be generated
    public Transform generationPoint2; // Second point where obstacles will be generated
    public Transform generationPoint3; // Third point where obstacles will be generated
    public Transform generationPoint4; // Fourth point where coins will be generated
    public Transform generationPoint5; // Fifth point where coins will be generated

    public float minTimeBetweenObstacles; // Minimum time interval between obstacle creation
    public float maxTimeBetweenObstacles; // Maximum time interval between obstacle creation

    public float obstacleSpeed; // Speed of the obstacles
    public Canvas canvas;

    private Transform selectedGenerationPoint; // Reference to the selected generation point
    private GameObject lastObstacle; // Reference to the last created obstacle

    void Start()
    {
        StartCoroutine(GenerateObstacles());
    }

    IEnumerator GenerateObstacles()
    {
        while (true)
        {
            CreateObstacle();

            CreateCoin();

            yield return new WaitForSeconds(Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles));
        }
    }

    void CreateObstacle()
    {
        float randomNumber = Random.value;
        GameObject[] selectedObstaclePrefabs;
        GameObject obstacleToInstantiate;

        do
        {
            if (randomNumber < 0.2f)
            {
                selectedGenerationPoint = generationPoint1;
                selectedObstaclePrefabs = obstaclePrefabs1;
            }
            else if (randomNumber < 0.4f)
            {
                selectedGenerationPoint = generationPoint2;
                selectedObstaclePrefabs = obstaclePrefabs2;
            }
            else
            {
                selectedGenerationPoint = generationPoint3;
                selectedObstaclePrefabs = obstaclePrefabs3;
            }

            int randomIndex = Random.Range(0, selectedObstaclePrefabs.Length);
            obstacleToInstantiate = selectedObstaclePrefabs[randomIndex];
        } while (obstacleToInstantiate == lastObstacle);

        lastObstacle = obstacleToInstantiate;

        GameObject obstacle = Instantiate(obstacleToInstantiate, selectedGenerationPoint.position, Quaternion.identity, canvas.transform);
        obstacle.GetComponent<Rigidbody2D>().velocity = Vector2.left * obstacleSpeed;

        Destroy(obstacle, 5f);
    }

    // Создание монеты
    void CreateCoin()
    {
        if (selectedGenerationPoint == generationPoint1)
        {
            GameObject coin = Instantiate(coinPrefab, generationPoint4.position, Quaternion.identity, canvas.transform);
            coin.GetComponent<Rigidbody2D>().velocity = Vector2.left * obstacleSpeed;
            
            Destroy(coin, 5f);
        }
        else if (selectedGenerationPoint == generationPoint2)
        {
            GameObject coin = Instantiate(coinPrefab, generationPoint5.position, Quaternion.identity, canvas.transform);
            coin.GetComponent<Rigidbody2D>().velocity = Vector2.left * obstacleSpeed;
            Destroy(coin, 5f);
        }
    }
}
