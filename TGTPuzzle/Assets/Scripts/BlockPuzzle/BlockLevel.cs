using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLevel : MonoBehaviour
{
    [SerializeField] private int gridSice;

    private Block[] blocksToPlace;

    private BlockNode[,] nodeGrid;

    public BlockNode[,] NodeGrid { get => nodeGrid; }

    private void Awake()
    {
        blocksToPlace = GetComponentsInChildren<Block>();
    }

    private void Start()
    {
        GenerateNodeGrid();

        for (int i = 0; i < blocksToPlace.Length; i++)
        {
            blocksToPlace[i].PlaceBlock();
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
                node.PositionInGrid = new Vector2(x, y);
                nodeGrid[x, y] = node;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
