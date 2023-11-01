
using System.Collections.Generic;
using UnityEngine;

public class NodeControl : MonoBehaviour
{
    public List<NodeControl> listAllAdjacentes;
    public float weightFactor = 1f; 

    void Start()
    {

    }

    void Update()
    {

    }

    public NodeControl GetNextNode()
    {
        int index = Random.Range(0, listAllAdjacentes.Count);
        return listAllAdjacentes[index];
    }
}
