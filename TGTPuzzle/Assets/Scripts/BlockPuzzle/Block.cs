using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private bool xAxies;
    [SerializeField] private Vector2 startPosition;

    private BlockNode[] nodes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceBlock(BlockNode[,] grid)
    {
        Vector2 offset = (xAxies) ? new Vector2((length - 1) / 2, 0) : new Vector2(0, (length - 1) / 2);

        try
        {
            transform.position = (Vector2)grid[(int)startPosition.x, (int)startPosition.y].transform.position + offset;
        }
        catch (System.Exception)
        {
            Debug.LogWarning(this.name + " dosent have the corect info");
            return;
        }

        for (int i = 0; i < length; i++)
        {
            BlockNode node = (xAxies) ? grid[(int)startPosition.x + i, (int)startPosition.y] : grid[(int)startPosition.x, (int)startPosition.y + i];

            if (node.BlockOnNode != null)
            {
                Debug.LogWarning(this.name + " owerlaps with " + node.BlockOnNode.name);
                return;
            }

            node.BlockOnNode = this;
        }
    }
}
