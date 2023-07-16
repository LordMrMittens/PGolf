using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerNode : MonoBehaviour
{
    public BreakableObstacleColour nodeColour {get; private set;}

    [SerializeField] float timeBetweenColourChanges = 3.0f;
    float colourChangeTimer;
    ParticleSystem nodeParticles;

    bool canColourSwap = true;
    // Start is called before the first frame update
    void Start()
    {
        nodeParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        colourChangeTimer += Time.deltaTime;
        if (colourChangeTimer > timeBetweenColourChanges && canColourSwap)
        {
            ChangeColour(Random.Range(1, System.Enum.GetValues(typeof(BreakableObstacleColour)).Length));
            
        }
    }

    void ChangeColour(int colour = 0)
    {

        nodeColour = (BreakableObstacleColour)colour;
        colourChangeTimer = 0;
        ParticleSystem.MainModule main = nodeParticles.main;
        switch (nodeColour)
        {
            case BreakableObstacleColour.none:
                nodeParticles.gameObject.SetActive(false);
                break;
            case BreakableObstacleColour.red:
                nodeParticles.gameObject.SetActive(true);
                main.startColor = Color.red;
                break;
            case BreakableObstacleColour.blue:
            nodeParticles.gameObject.SetActive(true);
                main.startColor = Color.blue;
                break;
            case BreakableObstacleColour.green:
            nodeParticles.gameObject.SetActive(true);
                main.startColor = Color.green;
                break;
            case BreakableObstacleColour.yellow:
            nodeParticles.gameObject.SetActive(true);
                main.startColor = Color.yellow;
                break;
            default:
                nodeParticles.gameObject.SetActive(false);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            if (nodeColour != BreakableObstacleColour.none)
            {
                canColourSwap = false;
                ball.SetBallColour(nodeColour);
                ChangeColour(0);
                nodeColour = BreakableObstacleColour.none;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            canColourSwap=true;
        }
    }
}
