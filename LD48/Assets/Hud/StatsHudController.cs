using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.OxygenManagement
{
    public class StatsHudController : MonoBehaviour
    {
        [SerializeField] private RectTransform lifeBarImage;
        [SerializeField] private RectTransform oxygenBarImage;
        [SerializeField] private TextMeshProUGUI deepnessText;
        [SerializeField] private Image hurtPanel;

        private float lastLifeQuantity;
        private bool lastLosingLife;

        private float totalTime;
        private float timeSinceHurting;

        void Start()
        {
            lifeBarImage.localScale = new Vector3(0, 1, 1);
            oxygenBarImage.localScale = new Vector3(0, 1, 1);
            deepnessText.text = "0";
        }

        void FixedUpdate()
        {
            totalTime += Time.deltaTime;
            CheckHurt();

            lastLifeQuantity = Life.Instance.Quantity;

            lifeBarImage.localScale = new Vector3(Life.Instance.Quantity / Life.Instance.Capacity, 1, 1);
            oxygenBarImage.localScale = new Vector3(Oxygen.Instance.Quantity / Oxygen.Instance.Capacity, 1, 1);
            deepnessText.text = $"{Submarine.Instance.Deepness}";
        }

        private void CheckHurt()
        {
            CheckIfStartHurting();

            if (Life.Instance.LosingLife)
            {
                HurtFlashing();
            }
            else
            {
                StopFlashing();
            }
        }

        private void CheckIfStartHurting()
        {
            var lifeIsDroping = Life.Instance.Quantity < lastLifeQuantity;
            if (Life.Instance.LosingLife && !lastLosingLife)
            {
                StartHurtFlashing();
            }
            lastLosingLife = lifeIsDroping;
        }

        private void StartHurtFlashing()
        {
            hurtPanel.gameObject.SetActive(true);
            timeSinceHurting = 0;
        }

        private void HurtFlashing()
        {
            timeSinceHurting += Time.deltaTime;
            if (timeSinceHurting > 1)
            {
                timeSinceHurting = 0;
            }

            var flashSpeed = 16;
            var flashCursor = timeSinceHurting * flashSpeed;
            if (flashCursor < Mathf.PI)
            {
                var color = hurtPanel.color;
                color.a = (Mathf.Clamp(Mathf.Sin(flashCursor), 0, 1) * 100) / 255;
                hurtPanel.color = color;
            }
        }

        private void StopFlashing()
        {
            hurtPanel.gameObject.SetActive(false);
            var color = hurtPanel.color;
            color.a = 0;
            hurtPanel.color = color;
        }
    }
}