using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLevel : MonoBehaviour
{
    [SerializeField] private int gridSice;
    [SerializeField] private Block[] BlocksToPlace;

    private BlockNode[,] nodeGrid;

    //public BlockNode[,] NodeGrid { get => nodeGrid; }

    // Start is called before the first frame update
    void Start()
    {
        GenerateNodeGrid();
        for (int i = 0; i < BlocksToPlace.Length; i++)
        {
            BlocksToPlace[i].PlaceBlock(nodeGrid);
        }
    }

    private void GenerateNodeGrid()
    {
        nodeGrid = new BlockNode[gridSice, gridSice];

        for (int y = 0; y < gridSice; y++)
        {
            for (int x = 0; x < gridSice; x++)
            {
                GameObject nodeObject = new GameObject("Node " + x + " " + y);
                nodeObject.transform.position = -Vector2.one * (gridSice - 1) * .5f + new Vector2(x, y);
                nodeObject.transform.parent = transform;

                BlockNode node = nodeObject.AddComponent<BlockNode>();
                nodeGrid[x, y] = node;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
