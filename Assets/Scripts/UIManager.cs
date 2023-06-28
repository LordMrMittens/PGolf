using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject endOfGamePanel;
    [SerializeField] TMP_Text endOfGameScoreText;
    [SerializeField] TMP_Text powerLevelText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void DisplayEndOfGameScore(int par, int currentScore)
    {
        endOfGamePanel.SetActive(true);
        endOfGameScoreText.text = $"Level Par: {par} <br> Your Score: {currentScore}";
    }

    public void UpdatePowerLevel(float powerLevel){
        powerLevelText.text = powerLevel.ToString();
    }
}
