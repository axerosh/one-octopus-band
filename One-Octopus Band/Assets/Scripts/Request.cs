using UnityEngine;
using UnityEditor;

public class Request : ScriptableObject
{
    public Request(InstrumentType instrumentType, int progress)
    {
        InstrumentType = instrumentType;
        Progress = progress;
    }
    public InstrumentType InstrumentType;
    public int Progress;
}