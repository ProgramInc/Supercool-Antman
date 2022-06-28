using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] float pickupValue;
    [SerializeField] PickupTypesEnum pickupType;

    PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (pickupType)
            {
                case PickupTypesEnum.Health:
                    playerStats.ChangeHealth(pickupValue);
                    break;

                case PickupTypesEnum.Energy:
                    playerStats.ChangeEnergy(pickupValue);
                    break;

                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}
