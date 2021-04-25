using TMPro;
using UnityEngine;

namespace Assets.OxygenManagement
{
    public class OxygenHudController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI oxygenText;

        void Start()
        {
            oxygenText.text = "0";
        }

        void Update()
        {
            oxygenText.text = $"{Oxygen.Instance.Quantity:0.00}";
        }
    }
}