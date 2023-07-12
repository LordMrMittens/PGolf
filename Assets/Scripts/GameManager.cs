using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field : SerializeField] public int par { get; set; } = 2;
    public int currentScore { get; private set; } = 0;
    public UIManager uIManager { get; set; }
    public LevelGeneration.LevelCreationManager levelCreator { get; set; }

    protected override void Awake()
    {
        base.Awake();
        
        ResetGame();
        
    }
    private void AssignManagers()
    {
        uIManager = GameObject.FindObjectOfType<UIManager>();
        levelCreator = GameObject.FindObjectOfType<LevelGeneration.LevelCreationManager>();
    }
    public void AddPointsToScore(int points)
    {
        currentScore += points;
        uIManager.UpdateScore(currentScore);
    }
    public void EndOfGame()
    {
        uIManager.DisplayEndOfGameScore(par, currentScore);
    }
    public void DisplayPowerLevel(float powerLevel, bool shouldPanelBeOn)
    {
        uIManager.UpdatePowerLevel(powerLevel);
        uIManager.TogglePowerLevelPanel(shouldPanelBeOn);
    }
    public void ResetGame()
    {
        AssignManagers();
        currentScore = 0;
        DisplayPowerLevel(0, false);
        uIManager.UpdateLevelPar(par);
        uIManager.UpdateScore(currentScore);

    }

}
