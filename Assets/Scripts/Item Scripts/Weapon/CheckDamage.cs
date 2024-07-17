using UnityEngine.UIElements;

namespace Item_Scripts
{
    using UnityEngine;
    using System;
    
    public class CheckDamage: MonoBehaviour
    {
        [SerializeField] private Vector3 weaponColliderCheck;

        [SerializeField] private float damageRadius;
        
        //During attack animation, create a collider in front of weapon which checks for objects hit by it
            //If damageable objects appear in sphere, apply damage to object
        //Check if animation exists.
            //Create collider
        //Spherecheck should be applied as animation event as weapon type and animation type may vary
        //Really only have to calculate where overlapsphere should be drawn
        private void Update()
        {
            CreateDamageCollider();
            OnDrawGizmos();
        }

        public void CreateDamageCollider()
        {
            Vector3 weaponTip = weaponColliderCheck;
            float colliderRadius = damageRadius;
            Collider[] colliders = Physics.OverlapSphere(weaponTip, colliderRadius);

            foreach (var collider in colliders)
            {
                //apply damage logic
                Debug.Log("Take that!");
            }
        }

        private void OnDrawGizmos()
        {
                Gizmos.DrawWireSphere(weaponColliderCheck, damageRadius);
        }
    }
}