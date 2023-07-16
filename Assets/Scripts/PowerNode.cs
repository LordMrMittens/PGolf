using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerNode : MonoBehaviour
{
    public BreakableObstacleColour nodeColour {get; private set;}

    [SerializeField] float timeBetweenColourChanges = 3.0f;
    float colourChangeTimer;
    ParticleSystem nodeParticles;
    // Start is called before the first frame update
    void Start()
    {
        nodeParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        colourChangeTimer += Time.deltaTime;
        if (colourChangeTimer > timeBetweenColourChanges)
        {
            ChangeColour();
            colourChangeTimer = 0;
        }
    }

    void ChangeColour()
    {
        int colourIndex = Random.Range(1, System.Enum.GetValues(typeof(BreakableObstacleColour)).Length);

        nodeColour = (BreakableObstacleColour)colourIndex;

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
                ball.SetBallColour(nodeColour);
                nodeColour = BreakableObstacleColour.none;
            }
        }
    }
}
