using UnityEngine.Audio;
using System;
using UnityEngine;
using Random=UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
   public SoundClass[] sounds;
	public static SoundManager instance;
    // Start is called before the first frame update
    void Awake()
    {
    	if (instance == null)
    	{
    		instance = this;
    	} else {
    		Destroy(gameObject);
    		return;
    	}

    	DontDestroyOnLoad(gameObject);
        foreach(SoundClass s in sounds)
        {
        	s.source = gameObject.AddComponent<AudioSource>();
        	s.source.clip = s.clip;
        	s.source.volume = s.volume;
        	s.source.pitch = s.pitch;
        	s.source.loop = s.loop;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
    	SoundClass s = Array.Find(sounds, sound => sound.name == name);
    	s.source.Play();
    }
    public void randoPlay(string name)
    {
    	SoundClass s = Array.Find(sounds, sound => sound.name == name);
    	s.source.pitch = Random.Range(s.pitch-0.1f, s.pitch+0.1f);
    	s.source.Play();
    }
    public void StopPlaying (string sound)
 {
    SoundClass s = Array.Find(sounds, item => item.name == sound);
    if (s == null)
    {
        Debug.LogWarning("Sound: " + name + " not found!");
        return;
    }

    s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));
    s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch/ 2f, s.pitch / 2f));

     s.source.Stop ();
    }
}
