using System.Collections.Generic;
using UnityEngine;

public class Pickuper : MonoBehaviour {
    private bool pickupMode = false;

    private List<Pickupable> pickedStuff = new List<Pickupable>();

    private void OnTriggerEnter(Collider other) {
        if (pickupMode) {
            Pickupable pickup = other.gameObject.GetComponent<Pickupable>();
            if(pickup != null) {
                pickup.Pickup(this);
                pickedStuff.Add(pickup);
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
