using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class Smackable : MonoBehaviour
{
    public InstrumentType InstrumentType;
    public SmackedEvent OnSmacked;
	public AudioSource audioSourcePrefab;
    public AudioClip[] clips;

    private Queue<AudioSource> audioQueue = new Queue<AudioSource>();

    void OnTriggerEnter(Collider other)
    {
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
		
		if (clips.Length > 0)
		{
			audioSource.clip = clips[Random.Range(0, clips.Length)];
			audioSource.Play();
		}
    
        OnSmacked.Invoke(InstrumentType);
    }
}
