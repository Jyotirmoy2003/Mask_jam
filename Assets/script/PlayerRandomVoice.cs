using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class PlayerRandomVoice : MonoBehaviour
{
    [Header("Voice Clips")]
    [SerializeField] private List<AudioClip> voiceClips = new List<AudioClip>();

    [Header("Delay (seconds)")]
    [SerializeField] private float minDelay = 8f;
    [SerializeField] private float maxDelay = 20f;

    [Header("Audio")]
    [SerializeField] private float volume = 1f;

    private AudioSource audioSource;
    private Coroutine voiceRoutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = volume;
    }

    private void OnEnable()
    {
        StartVoiceLoop();
    }

    private void OnDisable()
    {
        StopVoiceLoop();
    }

    // ---------------- PUBLIC ----------------

    public void StartVoiceLoop()
    {
        if (voiceRoutine != null)
            StopCoroutine(voiceRoutine);

        voiceRoutine = StartCoroutine(VoiceRoutine());
    }

    public void StopVoiceLoop()
    {
        if (voiceRoutine != null)
        {
            StopCoroutine(voiceRoutine);
            voiceRoutine = null;
        }

        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    // ---------------- INTERNAL ----------------

    private IEnumerator VoiceRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            if (audioSource.isPlaying || voiceClips.Count == 0)
                continue;

            AudioClip clip = voiceClips[Random.Range(0, voiceClips.Count)];

            audioSource.clip = clip;
            audioSource.Play();

            // Wait until clip finishes so no overlap can happen
            yield return new WaitForSeconds(clip.length);
        }
    }
}
