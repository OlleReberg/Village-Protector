using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Item_Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/WeaponItem")]
    public class WeaponItem : ItemSO
    {
        [SerializeField] private GameObject modelPrefab;
        [SerializeField] private bool isUnarmed;


        public GameObject ModelPrefab => modelPrefab;
        public bool IsUnarmed => isUnarmed;
    }
}