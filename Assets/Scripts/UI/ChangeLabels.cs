using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLabels : MonoBehaviour
{
    private TrashTracker trashTracker;
    private TimeCycle timeCycle;
    private LevelUp levelUp;
    private int totalTrash;
    private Player player;
    
    [SerializeField] private TextMeshProUGUI trashCounterLabel;
    [SerializeField] private TextMeshProUGUI dayCounterLabel;
    [SerializeField] private TextMeshProUGUI levelCounterLabel;
    [SerializeField] private TextMeshProUGUI livesCounterLabel;

    private void Awake()
    {
        trashTracker = FindObjectOfType<TrashTracker>();
        timeCycle    = FindObjectOfType<TimeCycle>();
        levelUp      = FindObjectOfType<LevelUp>();
        player       = FindObjectOfType<Player>();
    }
    
    void FixedUpdate()
    {
        totalTrash = trashTracker.TrashThrownCount();
        trashCounterLabel.SetText($"Trash: {totalTrash}");
        dayCounterLabel.SetText($"Day {timeCycle.GetDays()}");
        levelCounterLabel.SetText($"Level {levelUp.GetLevel()}");
        livesCounterLabel.SetText($"Lives: {player.GetLives()}");
    }
}
