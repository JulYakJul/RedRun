using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f; // Сила прыжка игрока
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Проверяем нажатие клавиши пробела, стрелки вверх или кнопки мыши
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Применяем силу вверх для прыжка игрока
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
