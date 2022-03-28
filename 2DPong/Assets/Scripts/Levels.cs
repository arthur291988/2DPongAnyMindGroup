using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> achievementIcons;
    [SerializeField]
    private Text highScoreText;
    [HideInInspector]
    public int highScore;
    [HideInInspector]
    public int achievementsScore;
    public Button button;

    public void setAchievements() {
        for (int i = 0; i < achievementIcons.Count; i++) {
            if (i < achievementsScore) achievementIcons[i].GetComponent<RawImage>().color = new Color(0.93f, 0, 0, 1);
            if (!achievementIcons[i].activeInHierarchy) achievementIcons[i].SetActive(true);
        } 
    }
    public void setHighScore(int score) {
        if (highScore < score)
        {
            highScore = score;
        }
        highScoreText.text = highScore.ToString();
        if (!highScoreText.gameObject.activeInHierarchy) highScoreText.gameObject.SetActive(true);
    }
}
