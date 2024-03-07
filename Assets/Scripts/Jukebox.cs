using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Jukebox : MonoBehaviour
{
    public static Jukebox instance;

    public AudioClip[] dropTokenSounds;
    public AudioClip buttonClick;
    public AudioClip buttonClickLight;

    public AudioClip[] diceClickingSounds;

    private AudioSource audio;

    void Awake()
    {
        instance = this;
        audio = GetComponent<AudioSource>();
    }

    public void OnDropToken()
    {
        audio.PlayOneShot(dropTokenSounds[Random.Range(0, dropTokenSounds.Length - 1)]);
    }

    public void OnButtonClick()
    {
        audio.PlayOneShot(buttonClick);
    }

    public void OnLightButtonClick()
    {
        audio.PlayOneShot(buttonClickLight);
    }

    public void OnDiceCollision()
    {
        audio.PlayOneShot(diceClickingSounds[Random.Range(0, diceClickingSounds.Length-1)]);
    }
}
