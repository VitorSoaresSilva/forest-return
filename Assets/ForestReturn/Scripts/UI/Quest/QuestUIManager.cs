using System.Collections.Generic;
using UnityEngine;

namespace ForestReturn.Scripts.UI.Quest
{
    public class QuestUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private GameObject questCardPrefab;
        private List<QuestCard> _questCards = new();
        

        private void OnEnable()
        {
            foreach (QuestCard questCard in _questCards)
            {
                Destroy(questCard.gameObject);
            }
            _questCards.Clear();
            
            //Get Quests from some Manager
                List<QuestCardData> questCardDataList = new()
                {
                    new QuestCardData("Title", "Description", "100", "200"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                    new QuestCardData("Title 2", "Description", "300", "100"),
                };
            //
            foreach (QuestCardData questCardData in questCardDataList)
            {
                var questCardInstantiated = Instantiate(questCardPrefab, content.transform);
                var questCardScript = questCardInstantiated.GetComponent<QuestCard>();
                questCardScript.SetData(questCardData);
                _questCards.Add(questCardScript);
            }
            
            
        }
    }
}