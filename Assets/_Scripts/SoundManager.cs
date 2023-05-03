using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Sounds
{   
    Shoot,
    Heal,
    TakeDamage,
    Death
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] Clips;
    [SerializeField] private AudioSource MusicSource, EffectsSource;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void SliderIndexer(int index, float value)
    {
        switch (index) {
            case 0:
                ChangeMasterVolume(value);
                break;
            case 1:
                ChangeMusicVolume(value);
                break;
            case 2:
                ChangeEffectsVolume(value);
                break;
        }
    }

    public void PlaySound(Sounds sound)
    {
        switch(sound) {
            case Sounds.Shoot:
                EffectsSource.PlayOneShot(Clips[0]);
                break;
            case Sounds.Heal:
                EffectsSource.PlayOneShot(Clips[1]);
                break;
            case Sounds.TakeDamage:
                EffectsSource.PlayOneShot(Clips[2]);
                break;
            case Sounds.Death:
                EffectsSource.PlayOneShot(Clips[3]);
                break;
        }
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        MusicSource.volume = value;
    }

    public void ChangeEffectsVolume(float value)
    {
        EffectsSource.volume = value;
    }

}