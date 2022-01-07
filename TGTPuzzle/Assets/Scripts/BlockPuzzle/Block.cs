using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
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
        //float moveDir = (xAxies) ? distance.x : distance.y;


        if (Mathf.Abs(distance.magnitude) > .5f)
        {
            BlockNode[,] grid = level.NodeGrid;

            vectorAxies = (distance.magnitude > 0) ? vectorAxies: vectorAxies * -1;

            Vector2 nextNodePosition = (distance.magnitude > 0) ? nodeTopRight.PositionInGrid + vectorAxies : nodeBottonLeft.PositionInGrid + vectorAxies;

            //try
            //{
            //    BlockNode nextNode = grid[(int)nextNodePosition.x, (int)nextNodePosition.y];

            //    if (nextNode.BlockOnNode == null)
            //    {
            //        transform.position = (Vector2)transform.position + vectorAxies;

            //        nextNode.BlockOnNode = this;

            //        if(distance.magnitude > 0)
            //        {
            //            nodeTopRight = nextNode;

            //            nextNodePosition = nodeBottonLeft.PositionInGrid + vectorAxies;

            //            nodeBottonLeft = grid[(int)nextNodePosition.x, (int)nextNodePosition.y];

            //            Vector2 clearNodePosition = nodeBottonLeft.PositionInGrid - vectorAxies;

            //            grid[(int)clearNodePosition.x, (int)clearNodePosition.y].BlockOnNode = null;
            //        }
            //        else
            //        {
            //            nodeBottonLeft = nextNode;

            //            nextNodePosition = nodeTopRight.PositionInGrid + vectorAxies;

            //            nodeTopRight = grid[(int)nextNodePosition.x, (int)nextNodePosition.y];

            //            Vector2 clearNodePosition = nodeTopRight.PositionInGrid - vectorAxies;

            //            grid[(int)clearNodePosition.x, (int)clearNodePosition.y].BlockOnNode = null;
            //        }

            //    }
            //    else
            //    {
            //        Debug.Log(nextNode.BlockOnNode);
            //    }

            //}
            //catch (System.Exception)
            //{
            //    Debug.LogWarning(this.name + " out of bounds");
            //}
        }
    }

    public void PlaceBlock()
    {
        //Vector2 offset = (xAxies) ? new Vector2((length - 1) / 2, 0) : new Vector2(0, (length - 1) / 2);

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
