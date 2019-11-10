using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {
    public Rigidbody body;
    private Transform holder = null;

    private void FixedUpdate() {
        if(holder != null) {
            body.MovePosition(holder.position);
        }
    }

    public void Pickup(Pickuper picker) {
        holder = picker.transform;
        body.useGravity = false;
    }

    public void Release() {
        holder = null;
        body.useGravity = true;
    }

}
