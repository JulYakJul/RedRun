using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform player;
    public float parallaxSpeed = 0.5f;

    private Vector3 lastPlayerPosition;
    private bool playerExists = true;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        if (playerExists && player != null)
        {
            float playerDeltaY = player.position.y - lastPlayerPosition.y;

            float parallaxOffsetY = playerDeltaY * parallaxSpeed;

            transform.position += new Vector3(0f, parallaxOffsetY, 0f);

            lastPlayerPosition = player.position;
        }
    }

    public void SetPlayerExistence(bool exists)
    {
        playerExists = exists;
    }
}
