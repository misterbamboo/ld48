using TMPro;
using UnityEngine;

namespace Assets.OxygenManagement
{
    public class StatsHudController : MonoBehaviour
    {
        [SerializeField] private RectTransform oxygenBarImage;
        [SerializeField] private TextMeshProUGUI deepnessText;

        void Start()
        {
            oxygenBarImage.localScale = new Vector3(0, 1, 1);
            deepnessText.text = "0";
        }

        void FixedUpdate()
        {
            oxygenBarImage.localScale = new Vector3(Oxygen.Instance.Quantity / Oxygen.Instance.Capacity, 1, 1);
            deepnessText.text = $"{Submarine.Instance.Deepness}";
        }
    }
}