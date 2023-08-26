using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{
    public enum Sound
    {
        Implosion,
        Drown,
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource aS = soundGameObject.AddComponent<AudioSource>();
        aS.outputAudioMixerGroup = (Resources.Load("MainMixer") as AudioMixer).FindMatchingGroups("SFX")[0];
        aS.PlayOneShot(GetAudioClip(sound));
    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
