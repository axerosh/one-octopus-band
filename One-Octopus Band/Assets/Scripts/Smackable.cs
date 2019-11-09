using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public class Smackable : MonoBehaviour
{
    public UnityEvent OnSmacked;
    public AudioSource audio;
    public AudioClip clip;

    void Update()
    {
        //OnSmacked.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        
        var tentacle = other.gameObject.GetComponent<Tentacle>();

        audio.clip = clip;
        audio.Play();

        Debug.Log("this runs");
        if (tentacle != null)
        {
           
            OnSmacked.Invoke();
        }
    }
}
