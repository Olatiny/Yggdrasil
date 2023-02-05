using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource music;
    public AudioSource kazoo;
    public AudioSource sfx;

    public AudioClip normalMusic;
    public AudioClip kazooMusic;

    public bool isKazoo;

    public List<AudioClip> damageNoise;
    public List<AudioClip> littleSwipe;
    public List<AudioClip> buttonClick;
    public List<AudioClip> fireball;
    public List<AudioClip> bushmenShoot;
    public List<AudioClip> bushmenDie;
    public AudioClip dragonEnters;


    // Start is called before the first frame update
    void Start()
    {

        music.volume = 0.75f;
        kazoo.volume = 0.0f;

        music.clip = normalMusic;
        kazoo.clip = kazooMusic;

        
        isKazoo = false;

        music.Play();
        kazoo.Play();

    }

    public void ToggleKazoo() // SHOULD ONLY BE CALLED WHEN GAME IS PAUSED
    {
        isKazoo = !isKazoo;
        if (isKazoo)
        { // going TOWARDS KAZOO
            StartCoroutine(FadeAudioSource.StartFade(kazoo, .1f, .35f));
            StartCoroutine(FadeAudioSource.StartFade(music, .1f, 0f));
        } else
        { // going TOWARDS NORMAL
            StartCoroutine(FadeAudioSource.StartFade(kazoo, .1f, 0f));
            StartCoroutine(FadeAudioSource.StartFade(music, .1f, .35f));
        }
    }

    public void PauseAdjust()
    {
        if (isKazoo)
        { // fade kazoo music down
            StartCoroutine(FadeAudioSource.StartFade(kazoo, .1f, .35f));
        } else
        { // otherwise just fade the normal music down
            StartCoroutine(FadeAudioSource.StartFade(music, .1f, .35f));
        }
    }

    public void UnpauseAdjust()
    {
        if (isKazoo)
        { // fade kazoo music down
            StartCoroutine(FadeAudioSource.StartFade(kazoo, .1f, .75f));
        }
        else
        { // otherwise just fade the normal music down
            StartCoroutine(FadeAudioSource.StartFade(music, .1f, .75f));
        }
    }


    public void DamageSFX()
    {
        sfx.PlayOneShot(damageNoise[UnityEngine.Random.Range(0, damageNoise.Count)]);
    }

    public void SwipeSFX()
    {
        sfx.PlayOneShot(littleSwipe[0]);
    }

    public void ButtonSFX()
    {
        sfx.PlayOneShot(buttonClick[UnityEngine.Random.Range(0, buttonClick.Count)]);
    }

    public void DragonFireballSFX()
    {
        sfx.PlayOneShot(fireball[0]);
    }

    public void DragonEntersSFX()
    {
        sfx.PlayOneShot(dragonEnters);
    }

    public void BushmenShootSFX()
    {
        sfx.PlayOneShot(bushmenShoot[0]);
    }

    public void BushmenDieSFX()
    {
        sfx.PlayOneShot(bushmenDie[0]);
    }
}

// code borrowed from online, fades audio out
// use:
// StartCoroutine(FadeAudioSource.StartFade(AudioSource audioSource, float duration, float targetVolume));
public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
