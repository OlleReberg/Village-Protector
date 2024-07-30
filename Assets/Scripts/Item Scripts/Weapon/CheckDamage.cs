namespace Item_Scripts
{
    using UnityEngine;

    public class CheckDamage: MonoBehaviour
    {
        private WeaponstatsSO weaponStats;
        private PlayerStatsSO playerStats;
        private EnemyStatsSO enemyStats;
        [SerializeField] private Vector3 weaponColliderCheck;
        [SerializeField] private float damageRadius;

        [Header("Poise")] 
        public float poiseDamage = 0;
        public bool poiseIsBroken = false;    
        
        //[Header("Final Damage")] private float finalDamageDealt = 0; //damage taken after all calculations made

        [Header("Animation")] 
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")] 
        public bool playDamageSFX = true;
        public AudioClip elementalDamageSoundFX; //used on top of default sound if elemental sound

        [Header("Direction Damage Taken From")]
        public float angleHitFrom; //determine which animation to play
        public Vector3 contactPoint; //determine damage fx instantiate
        
        //During attack animation, create a collider in front of weapon which checks for objects hit by it
            //If damageable objects appear in sphere, apply damage to object
        //Check if animation exists.
            //Create collider
        //Spherecheck should be applied as animation event as weapon type and animation type may vary
        //Really only have to calculate where overlapsphere should be drawn

        private void Update()
        {
            //CreateDamageCollider();
            //OnDrawGizmos();
        }

        // public void CreateDamageCollider()
        // {
        //     Vector3 weaponTip = weaponColliderCheck;
        //     float colliderRadius = damageRadius;
        //     Collider[] colliders = Physics.OverlapSphere(weaponTip, colliderRadius);
        //
        //     foreach (var collider in colliders)
        //     {
        //         //apply damage logic
        //         Debug.Log("Take that!");
        //     }
        // }

        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(weaponColliderCheck, damageRadius);
        }
    }
}