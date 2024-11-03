using UnityEngine;

public class MovingDoor : MonoBehaviour
{
    public Transform platform;
    public Transform endPoint;
    public bool isMoved = false;
    public MovingDoor startAfter; // leave blank if this is the first door
    public float moveSpeed = 1f;
    public AudioSource sfxSource;
    public int shardsNeedToUnlock;
    public AudioClip doorUnlockedSound;
    public CollectionManager colman;
    bool doorUnlocked = false;

    private void Update()
    {
        if (doorUnlocked)
        {
            UnlockDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (startAfter != null)
        {
            if (startAfter.isMoved == true) // if this is the first door
            {
                if (colman.numShardCollected >= shardsNeedToUnlock)
                {
                    doorUnlocked = true;
                    Debug.Log("case 1 door unlocked");
                    sfxSource.clip = doorUnlockedSound;
                    sfxSource.Play();
                }
            }
        }
        else
        {
            if (colman.numShardCollected >= shardsNeedToUnlock)
            {
                doorUnlocked = true;
                Debug.Log("case 2 door unlocked");
                sfxSource.clip = doorUnlockedSound;
                sfxSource.Play();
            }
        }
    }

    public void UnlockDoor()
    {
        Vector2 target = endPoint.position;

        platform.position = Vector2.Lerp(platform.position, target, moveSpeed * Time.fixedDeltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if(distance <=0.1f && isMoved == false)
        {
            isMoved = true;
            colman.numShardCollected -= shardsNeedToUnlock;
            colman.shardUI.text = colman.numShardCollected.ToString();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
            

        
    }

    private void OnDrawGizmos()
    {
        if(platform!=null && endPoint != null)
        {
            Gizmos.DrawLine(platform.position, endPoint.position);
        }
    }
}
