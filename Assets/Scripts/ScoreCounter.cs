using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance;

    public float currentScore;
    public float totalScore;

    [SerializeField] Text collectedPointsUI;
    [SerializeField] Text totalScoreUI;

    void Awake()
    {
        //create instance for the script
        if (instance != null)
        {
            Debug.LogWarning("There is multiple ScoreCounter instances!");
            return;
        }

        instance = this;
    }

    public void CollectPoints(float collectedPoints)
    {
        currentScore += collectedPoints;
        collectedPointsUI.text = currentScore + " %";
    }

    public void IncreaseTotalScore()
    {
        totalScore += currentScore;
        currentScore = 0f;
        totalScoreUI.text = totalScore.ToString();
        collectedPointsUI.text = currentScore + " %";
    }
}
