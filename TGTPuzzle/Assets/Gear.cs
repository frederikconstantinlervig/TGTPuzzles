using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [Header("ID")]
    [SerializeField, Range(0, 8)]
    private int id;
    [Header("Saturation")]
    [SerializeField, Range(.5f, 1f)]
    private float max;
    [SerializeField, Range(0, .5f)]
    private float min;
    private bool corectPlace;
    private Animator animator;



    void Start()
    {
        corectPlace = false;
        animator = GetComponent<Animator>();
        CheckPlacement();
        GenerateColorFromId();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            corectPlace = !corectPlace;
            CheckPlacement();
        }
    }

    private void GenerateColorFromId()
    {
        //this version only takes up to 9

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float sVal;
        if (id < 6)
        {
            sVal = (id < 3) ? min : max;
        }
        else
        {
            sVal = (min + max) / 2;
        }

        switch (id % 3)
        {
            case 0:
                sr.color = new Color(max, sVal, min);
                break;
            case 1:
                sr.color = new Color(min, max, sVal);
                break;
            case 2:
                sr.color = new Color(sVal, min, max);
                break;
            default:
                break;
        }
    }

    private void CheckPlacement()
    {
        animator.enabled = corectPlace;
    }
}

