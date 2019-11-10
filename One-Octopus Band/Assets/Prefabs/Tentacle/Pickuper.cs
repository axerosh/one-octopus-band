using System.Collections.Generic;
using UnityEngine;

public class Pickuper : MonoBehaviour {
    private bool pickupMode = false;
    public AudioClip clip;

    private List<Pickupable> pickedStuff = new List<Pickupable>();

    private void OnTriggerEnter(Collider other) {
        if (pickupMode) {
            Pickupable pickup = other.gameObject.GetComponent<Pickupable>();
            if(pickup != null && !pickedStuff.Contains(pickup)) {
                pickup.Pickup(this);
                pickedStuff.Add(pickup);

                // Play sound
                var source = GetComponent<AudioSource>();
                source.clip = clip;
                source.Play();
            }
        }
    }

    public void StartPickUp() {
        pickupMode = true;
    }

    public void Release() {
        pickupMode = false;
        foreach(Pickupable p in pickedStuff) {
            p.Release();
        }
        pickedStuff.Clear();
    }
}
