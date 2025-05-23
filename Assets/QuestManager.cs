using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [System.Serializable]
    public class Quest
    {
        public string questID;
        public string description;
        public bool isCompleted;
        public bool isActive;
        public Text uiText;  // Drag Text dari UI ke sini
    }

    public List<Quest> quests = new List<Quest>();
    private int currentQuestIndex = -1;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ShuffleQuests();     // Acak urutan quest
        ActivateNextQuest(); // Aktifkan quest pertama
    }

    void ShuffleQuests()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            Quest temp = quests[i];
            int randomIndex = Random.Range(i, quests.Count);
            quests[i] = quests[randomIndex];
            quests[randomIndex] = temp;
        }
    }

    public void CompleteQuest(string id)
    {
        foreach (var quest in quests)
        {
            if (quest.questID == id && quest.isActive && !quest.isCompleted)
            {
                quest.isCompleted = true;
                quest.isActive = false;
                quest.uiText.text = quest.description + " ✅";
                ActivateNextQuest();
                break;
            }
        }
    }

    void ActivateNextQuest()
    {
        currentQuestIndex++;
        if (currentQuestIndex < quests.Count)
        {
            quests[currentQuestIndex].isActive = true;
            quests[currentQuestIndex].uiText.text = quests[currentQuestIndex].description + " ❗";
        }
    }
}
