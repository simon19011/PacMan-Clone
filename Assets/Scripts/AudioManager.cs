using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip introMusic;
    [SerializeField] private AudioClip defaultMusic;
    [SerializeField] private AudioClip scaredGhostMusic;
    [SerializeField] private AudioClip deadGhostMusic;

    private Coroutine musicCoroutine;
    void Start()
    {
        StartMusic();
    }

    public void StartMusic()
    {
        musicCoroutine = StartCoroutine(PlayIntroThenDefault());
    }

    private IEnumerator PlayIntroThenDefault()
    {
        audioSource.clip = introMusic;
        audioSource.loop = false;
        audioSource.Play();

        float timer = 0f;

        while (audioSource.isPlaying && timer < 3f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        PlayDefault();
    }

    private void PlayDefault()
    {
        audioSource.clip = defaultMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void PlayScared()
    {
        audioSource.clip = scaredGhostMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void PlayDead()
    {
        audioSource.clip = deadGhostMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}
