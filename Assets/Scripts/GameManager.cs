using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [field : SerializeField] public int par { get; set; } = 2;
    public int currentScore { get; private set; } = 0;
    public static GameManager Instance;
    [SerializeField] UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddPointsToScore(int points)
    {
        currentScore += points;
    }

    public void EndOfGame()
    {
        uIManager.DisplayEndOfGameScore(par,currentScore);
    }
    public void DisplayPowerLevel(float powerLevel){
        uIManager.UpdatePowerLevel(powerLevel);
    }
}
