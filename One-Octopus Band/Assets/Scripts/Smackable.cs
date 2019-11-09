﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class Smackable : MonoBehaviour
{
    public InstrumentType InstrumentType;
    public UnityEvent<InstrumentType> OnSmacked;
	public AudioSource audioSourcePrefab;
    public AudioClip clip;
    public AudienceManager audienceManager;

    private Queue<AudioSource> audioQueue = new Queue<AudioSource>();

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Am i here?");
        var tentacle = other.gameObject.GetComponent<Tentacle>();
        
		AudioSource audioSource;
		if (audioQueue.Count == 0 || audioQueue.Peek().isPlaying)
		{
			audioSource = Instantiate(audioSourcePrefab, transform);
			audioQueue.Enqueue(audioSource);
		}
		else
		{
			audioSource = audioQueue.Dequeue();
			audioQueue.Enqueue(audioSource);
		}
		audioSource.clip = clip;
		audioSource.Play();
        audienceManager.OnInstrumentSmacked(InstrumentType);
        if (tentacle != null)
        {
            OnSmacked.Invoke(InstrumentType);
        }
    }
}
