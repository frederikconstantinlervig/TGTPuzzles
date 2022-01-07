using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] private bool keyBlock;
    [SerializeField] private float length;
    [SerializeField] private bool xAxies;
    [SerializeField] private Vector2 startPosition;

    [SerializeField] private BlockNode nodeTopRight;

    [SerializeField] private BlockNode nodeBottonLeft;

    private Vector2 mouseOffset;

    private BlockLevel level;

    private Vector2 vectorAxies;

    private void Awake()
    {
        vectorAxies = (xAxies) ? Vector2.right : Vector2.up;

        level = GetComponentInParent<BlockLevel>();
    }

    private void OnMouseDown()
    {
        mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector2 distance = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset - (Vector2)transform.position) * vectorAxies;

        if (distance.magnitude > .5f)
        {
            BlockNode[,] grid = level.NodeGrid;

            Vector2 movementDiretion;
            Vector2 nextNodePosition;
            if(distance.x + distance.y > 0)
            {
                movementDiretion = vectorAxies;
                nextNodePosition = nodeTopRight.PositionInGrid + movementDiretion;
            }
            else
            {
                movementDiretion = vectorAxies * -1;
                nextNodePosition = nodeBottonLeft.PositionInGrid + movementDiretion;
            }

            if (keyBlock)
            {
                level.WinCheck(nextNodePosition);
            }

            try
            {
                BlockNode nextNode = grid[(int)nextNodePosition.x, (int)nextNodePosition.y];

                if (nextNode.BlockOnNode == null)
                {
                    transform.position = (Vector2)transform.position + movementDiretion;

                    nextNode.BlockOnNode = this;

                    if (distance.x + distance.y > 0)
                    {
                        nodeTopRight = nextNode;

                        nextNodePosition = nodeBottonLeft.PositionInGrid + movementDiretion;

                        nodeBottonLeft = grid[(int)nextNodePosition.x, (int)nextNodePosition.y];

                        Vector2 clearNodePosition = nodeBottonLeft.PositionInGrid - movementDiretion;

                        grid[(int)clearNodePosition.x, (int)clearNodePosition.y].BlockOnNode = null;
                    }
                    else
                    {
                        nodeBottonLeft = nextNode;

                        nextNodePosition = nodeTopRight.PositionInGrid + movementDiretion;

                        nodeTopRight = grid[(int)nextNodePosition.x, (int)nextNodePosition.y];

                        Vector2 clearNodePosition = nodeTopRight.PositionInGrid - movementDiretion;

                        grid[(int)clearNodePosition.x, (int)clearNodePosition.y].BlockOnNode = null;
                    }

                }

            }
            catch (System.Exception)
            {

            }
        }
    }

    public void PlaceBlock()
    {
        Vector2 offset = vectorAxies * (length - 1) / 2;

        BlockNode[,] grid = level.NodeGrid;

        try
        {
            transform.position = (Vector2)grid[(int)startPosition.x, (int)startPosition.y].transform.position + offset;

            for (int i = 0; i < length; i++)
            {
                BlockNode node = (xAxies) ? grid[(int)startPosition.x + i, (int)startPosition.y] : grid[(int)startPosition.x, (int)startPosition.y + i];

                if (node.BlockOnNode != null)
                {
                    Debug.LogError(this.name + " owerlaps with " + node.BlockOnNode.name);
                    return;
                }

                node.BlockOnNode = this;

                if (i == 0)
                {
                    nodeBottonLeft = node;
                }
                else if (i == length - 1)
                {
                    nodeTopRight = node;
                }
            }
        }
        catch (System.Exception)
        {
            Debug.LogError(this.name + " out of bounds");
            return;
        }
    }
}
