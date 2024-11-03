using UnityEngine;
using TMPro;

public class CollectionManager : MonoBehaviour
{
    public TextMeshProUGUI shardUI;
    public int numShardCollected = 0;

    private void OnEnable()
    {
        Shard.OnShardCollected += ShardCollected;
    }

    private void OnDisable()
    {
        Shard.OnShardCollected -= ShardCollected;
    }

    void ShardCollected()
    {
        numShardCollected++;
        shardUI.text = numShardCollected.ToString();
    }
}
