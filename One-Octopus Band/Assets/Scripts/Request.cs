using UnityEngine;
using UnityEditor;

public class Request : ScriptableObject
{
    public Request(InstrumentType instrumentType, int progress)
    {
        this.instrumentType = instrumentType;
        Progress = progress;
    }
    public InstrumentType instrumentType;
    public int Progress;
}
