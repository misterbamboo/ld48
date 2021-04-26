using TMPro;
using UnityEngine;

namespace Assets.OxygenManagement
{
    public class StatsHudController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI oxygenText;
        [SerializeField] private TextMeshProUGUI deepnessText;

        void Start()
        {
            oxygenText.text = "0";
            deepnessText.text = "0";
        }

        void FixedUpdate()
        {
            oxygenText.text = $"{Oxygen.Instance.Quantity:0.00}";
            deepnessText.text = $"{Submarine.Instance.Deepness}";
        }
    }
}