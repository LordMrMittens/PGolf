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
    public void DisplayEndOfGameScore(int par, int currentScore)
    {
        endOfGamePanel.SetActive(true);
        endOfGameScoreText.text = $"Level Par: {par} <br> Your Score: {currentScore}";
    }

    public void UpdatePowerLevel(float powerLevel)
    {
        powerLevelText.text = powerLevel.ToString();
        powerLevelBar.value = powerLevel;
    }
    public void TogglePowerLevelPanel(bool shouldBeOn){
        powerLevelPanel.SetActive(shouldBeOn);
    }
}
