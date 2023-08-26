using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class AmbienceManager : MonoBehaviour
{
    public AudioClip StartClip;
    private AudioSource track1, track2;
    
    public static AmbienceManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        track1 = gameObject.AddComponent<AudioSource>();
        track2 = gameObject.AddComponent<AudioSource>();
        track1.loop = true;
        track2.loop = true;
        track1.outputAudioMixerGroup = (Resources.Load("MainMixer") as AudioMixer).FindMatchingGroups("SFX")[0];
        track2.outputAudioMixerGroup = (Resources.Load("MainMixer") as AudioMixer).FindMatchingGroups("SFX")[0];

        SwapTrack(StartClip);
    }

    public void SwapTrack(AudioClip newClip)
    {
        StopAllCoroutines();

        StartCoroutine(FadeTrack(newClip));
    }

    private IEnumerator FadeTrack(AudioClip newClip)
    {
        float timeToFade = 1.25f;
        float timeElapsed = 0;

        if (track1.isPlaying)
        {
            track2.clip = newClip;
            track2.Play();

            while (timeElapsed < timeToFade)
            {
                track2.volume = Mathf.Lerp(0,1, timeElapsed / timeToFade);
                track1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            track1.Stop();
        }
        else
        {
            track1.clip = newClip;
            track1.Play();
            while (timeElapsed < timeToFade)
            {
                track1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            track2.Stop();
        }
    }
}
