using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Start,
    Shoot,
    Explosion,
    Lose
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sound Lists")]
    [SerializeField]
    private AudioClip[] startSounds;
    [SerializeField]
    private AudioClip[] shootSounds;
    [SerializeField]
    private AudioClip[] explosionSounds;
    [SerializeField]
    private AudioClip[] loseSounds;


    //aux vars
    private AudioSource aud;
    private void singletonCreation()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }
    private void Awake()
    {
        singletonCreation();
    }

    private void getRefs()
    {
        aud = GetComponent<AudioSource>();
    }
    private void Start()
    {
        getRefs();
    }
    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.Start:
                aud.PlayOneShot(startSounds[Random.Range(0, startSounds.Length)]);
                break;
            case SoundType.Shoot:
                aud.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Length)]);
                break;
            case SoundType.Explosion:
                aud.PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Length)]);
                break;
            case SoundType.Lose:
                aud.PlayOneShot(loseSounds[Random.Range(0, loseSounds.Length)]);
                break;
        }
    }
}
