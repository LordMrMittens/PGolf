using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject endOfGamePanel;
    [SerializeField] TMP_Text endOfGameScoreText;
    [SerializeField] GameObject powerLevelPanel;
    [SerializeField] TMP_Text powerLevelText;
    [SerializeField] Slider powerLevelBar;
    [SerializeField] Image powerLevelImage;
    [SerializeField] TMP_Text levelParText;
    [SerializeField] TMP_Text scoreText;
    public void DisplayEndOfGameScore(int par, int currentScore)
    {
        endOfGamePanel.SetActive(true);
        endOfGameScoreText.text = $"Level Par: {par} <br> Your Score: {currentScore}";
    }

    public void UpdateLevelPar(int par){
        levelParText.text = par.ToString();
    }
        public void UpdateScore(int score){
        scoreText.text = score.ToString();
    }

    public void UpdatePowerLevel(float powerLevel)
    {
        powerLevelText.text = powerLevel.ToString("F1");
        powerLevelBar.value = powerLevel;

        float normalizedValue = Mathf.Clamp01(powerLevel / 10f);
        Color lerpedColor = Color.Lerp(Color.green, Color.yellow, normalizedValue);
        if (normalizedValue >= 0.5f)
        {
            lerpedColor = Color.Lerp(Color.yellow, Color.red, (normalizedValue - 0.5f) * 2f);
        }
        powerLevelImage.color = lerpedColor;
    }
    public void TogglePowerLevelPanel(bool shouldBeOn){
        powerLevelPanel.SetActive(shouldBeOn);
    }
}
