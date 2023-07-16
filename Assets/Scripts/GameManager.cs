using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field : SerializeField] public int par { get; private set;}
    [field : SerializeField] public int minPar { get; private set; } = 2;
    public int currentScore { get; private set; } = 0;
    public UIManager uIManager { get; set; }
    public LevelGeneration.LevelCreationManager levelCreator { get; set; }

    [SerializeField] LayerMask layersInfluencingParCalculation;

    protected override void Awake()
    {
        base.Awake();
        
        ResetGame();
        
    }
        public void ResetGame()
    {
        AssignManagers();
        levelCreator.SetupGame();
        currentScore = 0;
        DisplayPowerLevel(0, false);
        uIManager.UpdateScore(currentScore);
        SetLevelPar();
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

    public void SetLevelPar(){
        par = minPar;
        Vector3 startPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        Vector3 goalPos = GameObject.FindGameObjectWithTag("Goal").transform.position;
        float distance = Vector3.Distance(startPos, goalPos);
        if (distance >30){
            par += Mathf.FloorToInt(distance/10);
        }
        par += Mathf.FloorToInt((levelCreator.numberOfObstacles + levelCreator.numberOfBumpers) /10);
        Vector3 direction = goalPos-startPos;
        if(Physics.Raycast(startPos, direction, distance, layersInfluencingParCalculation)){
            par++;
        } else {
            par--;
        }
        uIManager.UpdateLevelPar(par);
    }
    
private void Update() {
    if (uIManager == null|| levelCreator ==null){ // dropping references for some reason try not to have this as the solution 
        AssignManagers();
    }
}
}
