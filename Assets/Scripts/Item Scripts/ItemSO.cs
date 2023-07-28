using UnityEngine;

namespace Item_Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class ItemSO: ScriptableObject
    {
        [Header("Item Info")] 
        [SerializeField] private Sprite itemIcon;

        [SerializeField] private string itemName;
        [TextArea(15,20)]
        [SerializeField] private string itemDescription;
        
        public Sprite ItemIcon => itemIcon;

        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
    }
}