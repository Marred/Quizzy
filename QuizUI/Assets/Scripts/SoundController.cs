using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    [SerializeField]
    private AudioClip tickSound;

    [SerializeField]
    private AudioClip correctSound;

    [SerializeField]
    private AudioClip incorrectSound;

    [SerializeField]
    private AudioClip ambient;

    private AudioSource singleSource;
    private AudioSource ambientSource;

	// Use this for initialization
	void Start () {
        singleSource = gameObject.AddComponent<AudioSource>();
        ambientSource.playOnAwake = false;
	}

    public void playHoverSound()
    {
        singleSource.PlayOneShot(tickSound);
    }

    public void playCorrectSound()
    {
        singleSource.PlayOneShot(correctSound);
    }

    public void playIncorrectSound()
    {
        singleSource.PlayOneShot(incorrectSound);
    }
}
