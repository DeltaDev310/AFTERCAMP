using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playSFX(AudioClip audioClip, float volume = 1f)
    {
        if (audioClip == null)
            return;

        StartCoroutine(PlaySFXCoroutine(audioClip, volume));
    }

    private IEnumerator PlaySFXCoroutine(AudioClip audioClip, float volume)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        yield return new WaitForSeconds(audioClip.length);

        Destroy(audioSource);
    }
}