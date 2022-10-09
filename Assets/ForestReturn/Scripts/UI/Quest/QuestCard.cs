using TMPro;
using UnityEngine;

namespace ForestReturn.Scripts.UI.Quest
{
    public class QuestCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI seedText;
        [SerializeField] private TextMeshProUGUI scrapText;

        public QuestCard(QuestCardData data)
        {
            SetData(data);
        }
        
        public void SetData(QuestCardData data)
        {
            titleText.text = data.Title;
            descriptionText.text = data.Description;
            seedText.text = data.SeedValue;
            scrapText.text = data.ScrapValue;
        }
    }

    public struct QuestCardData
    {
        public readonly string Title;
        public readonly string Description;
        public readonly string SeedValue;
        public readonly string ScrapValue;

        public QuestCardData(string title, string description, string seedValue, string scrapValue)
        {
            Title = title;
            Description = description;
            SeedValue = seedValue;
            ScrapValue = scrapValue;
        }
    }
}
