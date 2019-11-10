using UnityEngine;
using UnityEditor;

public class Request : ScriptableObject
{
    public InstrumentType instrumentType;
    public bool met = false;
    public double maxTimeLeft;
    public double timeLeft;
}
