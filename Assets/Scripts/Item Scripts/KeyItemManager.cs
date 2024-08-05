using UnityEngine;

namespace Item_Scripts
{
    public class KeyItemManager : MonoBehaviour
    {
        public static KeyItemManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Ensures only one instance exists.
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Optionally keep it alive across scenes.
            }
        }

        public void UseKeyItem(ItemSO item)
        {
            // Logic to handle key item usage
            Debug.Log("Using key item: " + item.ItemName);
            // Implement the specific key item effects here
        }
    }
}