using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioManager instance;
    public Sound[] sounds;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start() {
        Play("MainTitleTheme");
    }

    public void Play(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null) {
            Debug.LogWarning("Sound: " + name + "Not Found");
        }
        sound.source.Play();
    }

    //Fade method?

    public void Stop(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null) {
            Debug.LogWarning("Sound: " + name + "Not Found");
        }
        sound.source.Stop();
    }
}
