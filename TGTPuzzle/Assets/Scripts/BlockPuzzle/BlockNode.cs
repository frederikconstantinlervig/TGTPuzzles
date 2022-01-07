using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNode : MonoBehaviour
{
    [SerializeField] private Block blockOnNode;
    [SerializeField] private Vector2 positionInGrid;

    public Block BlockOnNode { get => blockOnNode; set => blockOnNode = value; }
    public Vector2 PositionInGrid { get => positionInGrid; set => positionInGrid = value; }
}
