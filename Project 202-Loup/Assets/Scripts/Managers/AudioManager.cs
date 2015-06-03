using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using Debug = UnityEngine.Debug;

public class AudioManager : MonoBehaviour
{
    private List<AudioClip> allAudioClips = new List<AudioClip>();

    public string AudioFolder;

    private bool clipFound;

    [Header("FeedBack Options : ")]
    public float FeedBackVolume;

    AudioClip GetClipAudio(string folder, string clipName)
    {
        var audio = Resources.Load(folder + "/" + clipName, typeof(AudioClip));
        return audio as AudioClip;
    }

    public void PlayAudio(string folder, string clipName)
    {
        GetComponent<AudioSource>().PlayOneShot(GetClipAudio(folder, clipName), FeedBackVolume);
    }

    public void Snapshots_Transition(AudioMixerSnapshot snapshot, float t)
    {
        snapshot.TransitionTo(t);
    }
}