using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AddEnergyButton : MonoBehaviour
{
    [SerializeField] private float amount = 10f;

    public void Add()
    {
        energy.current += amount;
    }
}
