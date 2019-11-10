using System.Collections.Generic;
using UnityEngine;

public class Shakeable : MonoBehaviour {
    public InstrumentType type;
    public SmackedEvent OnSmacked;
    public AudioSource audioSourcePrefab;
    public AudioClip clip;
    public float velocityThreshold;
    public float respawnThreshold;
    public float audioCooldown;

    private Rigidbody body;
    private Vector3 previousVelocity = Vector3.zero;
    private Vector3 spawnPoint = Vector3.zero;
    private float currentCooldown = 0;

    private Queue<AudioSource> audioQueue = new Queue<AudioSource>();

    private void Start() {
        body = GetComponent<Rigidbody>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < spawnPoint.y && spawnPoint.y - transform.position.y > respawnThreshold) {
            body.position = spawnPoint;
        }

        if (currentCooldown <= 0 && body.velocity.sqrMagnitude - previousVelocity.sqrMagnitude > velocityThreshold) {
            AudioSource audioSource;
            if (audioQueue.Count == 0 || audioQueue.Peek().isPlaying) {
                audioSource = Instantiate(audioSourcePrefab, transform);
                audioQueue.Enqueue(audioSource);
            } else {
                audioSource = audioQueue.Dequeue();
                audioQueue.Enqueue(audioSource);
            }
            audioSource.clip = clip;
            audioSource.Play();

            OnSmacked.Invoke(type);
            currentCooldown = audioCooldown;
        }
        currentCooldown -= Time.deltaTime;
    }
}
