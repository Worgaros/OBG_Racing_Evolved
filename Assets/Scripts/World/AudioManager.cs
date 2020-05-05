using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour {

    List<AudioSource> audioSources;

    [SerializeField] AudioMixer ambiantMixer;
    [SerializeField] AudioMixer playerActionMixer;

    [SerializeField] float speedVolumeUp; 
    
    enum State {
        VOLUME_MAX,
        VOLUME_MIN
    }

    State state = State.VOLUME_MAX;
    
    void Start()
    {
        audioSources = new List<AudioSource>();

        for (int i = 0; i < 10; i++) {
            GameObject instance = new GameObject();
            instance.transform.parent = transform;
            instance.name = "AudioSource" + i;
            
            audioSources.Add(instance.AddComponent<AudioSource>());
            audioSources[i].outputAudioMixerGroup = playerActionMixer.outputAudioMixerGroup;
        }
    }

    public void SetAmbiantCutoff(float valueVolume) {
        ambiantMixer.SetFloat("cutOffHigh", Mathf.Lerp(10, 4000, Mathf.Abs(valueVolume)));
    }

    void Update()
    {
        switch (state) {
            case State.VOLUME_MAX:
                ambiantMixer.SetFloat("volume", 1);
                playerActionMixer.SetFloat("volume", 1);
                break;
            case State.VOLUME_MIN:
                float volume;
                ambiantMixer.GetFloat("volume", out volume);
                
                ambiantMixer.SetFloat("volume", volume + Time.deltaTime * speedVolumeUp);
                playerActionMixer.SetFloat("volume", volume + Time.deltaTime * speedVolumeUp);

                if (volume > 0) {
                    state = State.VOLUME_MAX;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnVolumDown() {
        state = State.VOLUME_MIN;
        
        ambiantMixer.SetFloat("volume", -40f);
        playerActionMixer.SetFloat("volume", -40f);
    }

    public void PlayOneShot(SO_Clip audioClip) {
        foreach (AudioSource audioSource in audioSources) {
            if (!audioSource.isPlaying) {
                audioSource.pitch = 1;
                audioSource.clip = audioClip.Clip;
                audioSource.volume = audioClip.Volume;
                audioSource.Play();
                return;
                
            }
        }
    }
    
    public void PlayWithRandomPitch(SO_Clip audioClip, float minPitch = 0.9f, float maxPitch = 1.1f) {
        foreach (AudioSource audioSource in audioSources) {
            if (!audioSource.isPlaying) {
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                audioSource.clip = audioClip.Clip;
                audioSource.volume = audioClip.Volume;
                audioSource.Play();
                return;
                
            }
        }
    }
}
