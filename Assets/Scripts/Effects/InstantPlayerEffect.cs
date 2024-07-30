using UnityEngine;

public class InstantPlayerEffect : ScriptableObject
{
    [Header("Effect ID")] public int instantEffectID;

    public virtual void ProcessEffect(PlayerController player)
    {
        
    }
}
