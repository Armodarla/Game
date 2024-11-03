using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShatteredPlatforms : MonoBehaviour
{
    private float fallDelay = 0.3f;
    private float destroyDelay = 0.4f;

    [SerializeField] private Rigidbody2D rb;

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
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}