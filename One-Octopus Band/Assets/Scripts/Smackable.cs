using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public class Smackable : MonoBehaviour
{
    public UnityEvent OnSmacked;

    void Update()
    {
        OnSmacked.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        
        var tentacle = other.gameObject.GetComponent<Tentacle>();
        Debug.Log("this runs");
        if (tentacle != null)
        {
            OnSmacked.Invoke();
        }
    }
}
