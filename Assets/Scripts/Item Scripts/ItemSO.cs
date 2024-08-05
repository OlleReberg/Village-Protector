using UnityEngine;

namespace Item_Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class ItemSO: ScriptableObject
    {
        [Header("Item Info")] 
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private int quantity;
        [SerializeField] private string itemName;
        [TextArea(15,20)]
        [SerializeField] private string itemDescription;
        [SerializeField] private ItemType itemType;
        
        
        public Sprite ItemIcon => itemIcon;
        public int Quantity => quantity;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public ItemType Type => itemType;

        public void OnRightClick()
        {
            //tooltipWindow.ShowTooltip(this);
            Debug.Log("Right clicked");
        }
        
        public enum ItemType
        {
            Equipable,
            Consumable,
            KeyItem
        }
    }
}