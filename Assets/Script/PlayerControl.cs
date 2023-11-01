using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public NodeControl startingNode; 
    public NodeControl currentNode;
    public float energy = 100f;
    private float currentEnergy;
    private Vector2 refVelocity;
    public float timeToMove;
    public Text energyDisplay;

    private bool isResting = false;
    private float restDuration = 5f;
    private float restTimer = 0f;

    void Start()
    {
        currentEnergy = energy;
        currentNode = startingNode;
        UpdateEnergyDisplay();
    }

    void Update()
    {
        if (isResting)
        {
            restTimer += Time.deltaTime;
            if (restTimer >= restDuration)
            {
                isResting = false;
                currentEnergy = energy;
                currentNode = startingNode.GetNextNode(); 
                UpdateEnergyDisplay();
            }
        }
        else if (currentNode != null)
        {
            transform.position = Vector2.SmoothDamp(transform.position, currentNode.transform.position, ref refVelocity, timeToMove);
        }
    }

    void UpdateEnergyDisplay()
    {
        energyDisplay.text = "Energía: " + Mathf.RoundToInt(currentEnergy);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Node"))
        {
            NodeControl collidedNode = collision.gameObject.GetComponent<NodeControl>();
            if (collidedNode == currentNode)
            {
                NodeControl nextNode = currentNode.GetNextNode();
                float cost = nextNode.weightFactor;

                if (currentEnergy >= cost)
                {
                    currentNode = nextNode;
                    currentEnergy -= cost;
                    UpdateEnergyDisplay();
                }
                else
                {
                    currentNode = null;
                    isResting = true;
                    restTimer = 0f;
                }
            }
        }
    }
}
