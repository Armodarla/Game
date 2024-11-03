using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShatteredPlatforms : MonoBehaviour
{
    private float fallDelay = 0.3f;
    private float respDelay = 4f;
    private Vector2 defPos;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        defPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CatFollowPlayer.catMode == 2)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Cat"))
            {
                StartCoroutine(Fall());
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Fall());
            }
        }
        StartCoroutine(RespawnPlatform());
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<Collider2D>().enabled = false;
        rb.AddForce(new Vector3(0, -1.0f, 0) * rb.mass * 100);
    }

    private IEnumerator RespawnPlatform()
    {
        yield return new WaitForSeconds(respDelay);
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<Collider2D>().enabled = true;
        transform.position = defPos;
    }
}