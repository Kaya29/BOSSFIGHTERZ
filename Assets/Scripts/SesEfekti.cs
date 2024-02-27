using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesEfekti : MonoBehaviour
{
    public AudioClip jumpEffect, deadEffect, reloadEffect, shotEffect, damageSound, footstepsSound;
    public float soundDelay = 1f;
    private bool isPlaying = false;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isPlaying) //Fire1 tuşuna basınca ses çalamıyorsa çal
        {
            PlaySound();
        }
    }
    void PlaySound()
    {
        audioSource.Play();
        isPlaying = true;
        StartCoroutine(WaitForSoundEnd(audioSource));
    }

    IEnumerator WaitForSoundEnd(AudioSource audioSource)
    {
        yield return new WaitForSeconds(soundDelay); //sesin bitimi
        Destroy(audioSource);
        isPlaying = false;
    }
    public void PlayFootSteps(bool isPlay)
    {
        audioSource.clip = footstepsSound;
        if (isPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    public void PlayJumpSound(bool isPlay)
    {
        audioSource.clip = jumpEffect;
        if (isPlay)
        {
            audioSource.Play();
        }
        else audioSource.Stop();
    }
    public void PlayDeadSound(bool isPlay)
    {
        audioSource.clip = deadEffect;
        if (isPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    public void PlayReloadSound(bool isPlay)
    {
        audioSource.clip = reloadEffect;
        if (isPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    public void PlayShotSound(bool isPlay)
    {
        audioSource.clip = shotEffect;
        if (isPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    public void PlayDamageSound(bool isPlay)
    {
        audioSource.clip = damageSound;
        if (isPlay)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}