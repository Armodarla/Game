using UnityEngine;

public class CharacterRespawn : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform cat;
    private Vector2 resCoordinate;
    [SerializeField] Collider2D collision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.setResCoordinate(player.position);
    }
    public void setResCoordinate(Vector2 newCoor)
    {
        resCoordinate = newCoor;
    }

    public void Update()
    {
        if (player.position.y < -5.0f)
        {
            player.position = resCoordinate;
            cat.position = resCoordinate;
        }

        if (cat.position.y < -5.0f)
        {
            cat.position = player.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            this.setResCoordinate(player.position);
        }
    }
}
