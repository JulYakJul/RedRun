using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D rb;
    private bool isJumping = false;
    public TextMeshProUGUI ScoreText;
    private int score = 0;
    public Transform respawnPoint;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        {
            StartJump();
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetMouseButtonUp(0))
        {
            EndJump();
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void StartJump()
    {
        isJumping = true;
    }

    private void EndJump()
    {
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Move the player to the respawn point
            transform.position = respawnPoint.position;

            // Destroy all obstacles and coins
            DestroyAllObstaclesAndCoins();

            // Update the score display
            ScoreText.text = score.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            score += 1;
            ScoreText.text = score.ToString();
        }
    }

    // Method to destroy all obstacles and coins
    private void DestroyAllObstaclesAndCoins()
    {
        // Destroy all objects with the "Obstacle" tag
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        // Destroy all objects with the "Coin" tag
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
    }
}
