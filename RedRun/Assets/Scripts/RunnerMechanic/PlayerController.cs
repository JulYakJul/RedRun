using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float sliderIncreaseRate;
    private Rigidbody2D rb;
    private bool isJumping = false;
    public TextMeshProUGUI ScoreText;
    private int score = 0;
    public Transform respawnPoint;
    public Slider slider;
    private Coroutine sliderCoroutine;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sliderCoroutine = StartCoroutine(IncreaseSliderValue());
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
            transform.position = respawnPoint.position;

            slider.value = 0f;

            if (sliderCoroutine != null)
            {
                StopCoroutine(sliderCoroutine);
            }

            sliderCoroutine = StartCoroutine(IncreaseSliderValue());

            DestroyAllObstaclesAndCoins();

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

    private void DestroyAllObstaclesAndCoins()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
    }

    private IEnumerator IncreaseSliderValue()
    {
        float targetValue = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * sliderIncreaseRate;
            slider.value = Mathf.Lerp(0f, targetValue, elapsedTime);
            yield return null;
        }
    }
}
