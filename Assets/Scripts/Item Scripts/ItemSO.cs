using UnityEngine;

namespace Item_Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class ItemSO: ScriptableObject
    {
        [Header("Item Info")] 
        [SerializeField] private Sprite itemIcon;

        [SerializeField] private string itemName;
        
        public Sprite ItemIcon => itemIcon;

        public string ItemName => itemName;
    }
}