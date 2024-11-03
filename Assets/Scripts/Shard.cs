using UnityEngine;
using System;

public class Shard : MonoBehaviour, ICollectible
{

    public AudioSource sfxSource;
    public AudioClip collectSound;

    public static event Action OnShardCollected;
    public void Collect()
    {
        OnShardCollected?.Invoke();
        Debug.Log("Shard collected!");
        sfxSource.clip = collectSound;
        sfxSource.Play();
        Destroy(gameObject);
    }
}
