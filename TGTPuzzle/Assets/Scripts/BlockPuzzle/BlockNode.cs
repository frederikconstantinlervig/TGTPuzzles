using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNode : MonoBehaviour
{
    [SerializeField] private Block blockOnNode;

    public Block BlockOnNode { get => blockOnNode; set => blockOnNode = value; }
}
