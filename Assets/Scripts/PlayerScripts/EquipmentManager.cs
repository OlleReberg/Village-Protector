using Item_Scripts;
using UnityEngine;

namespace PlayerScripts
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager Instance { get; private set; }
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    
        public void Equip(ItemSO item)
        {
            // Equip logic here
        }
    }
}