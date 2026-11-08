using UnityEngine;
using TMPro;

public class energy : MonoBehaviour
{
    public static float current = 0;
    public static float max;

    [SerializeField] private float maxEnergy = 5;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        max = maxEnergy;

        if (current > max)
            current = max;

        if (text != null)
            text.text = $"{(int)current} / {(int)max}";
    }
}