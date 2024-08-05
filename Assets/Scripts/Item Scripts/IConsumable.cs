using PlayerScripts;

namespace Item_Scripts
{
    public interface IConsumable
    {
        void Consume(PlayerStats player);
    }
}