using UnityEngine;
using UnityEditor;

public class Request : ScriptableObject
{
    public Request(InstrumentType instrumentType, double maxTimeLeft = 10) {
        this.instrumentType = instrumentType;
        this.maxTimeLeft = maxTimeLeft;
        timeLeft = maxTimeLeft;
    }
    public InstrumentType instrumentType;
    public bool met = false;
    public double maxTimeLeft;
    public double timeLeft;
}
