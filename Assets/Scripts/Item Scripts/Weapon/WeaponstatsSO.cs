using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Item/Weapon Stats")]
public class WeaponstatsSO : ScriptableObject
{
    [Header("Damage")]
    [SerializeField] private int physical;
    [SerializeField] private int fire;
    [SerializeField] private int lightning;
    [SerializeField] private int dark;
       
    public int Physical => physical;
    public int Fire => fire;
    public int Dark => dark;
    public int Lightning => lightning;
}
