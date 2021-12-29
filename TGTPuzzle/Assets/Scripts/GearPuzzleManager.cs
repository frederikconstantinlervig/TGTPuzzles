using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPuzzleManager : MonoBehaviour
{
    // vi børlave en custom editor til de her 

    [Header("Level indicators")]
    [SerializeField]
    private SpriteRenderer[] levelIndicarors;
    [SerializeField]
    private Color completeColor;
    [SerializeField]
    private Color notCompleteColor;

    [Header("Gear holders")]
    [SerializeField]
    private GearHolder[] gearHolders;

    private int currentLevel;

    public Gear SelectedGear { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        UpdateIndicators();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateIndicators()
    {
        for (int i = 0; i < levelIndicarors.Length; i++)
        {
            levelIndicarors[i].color = (i < currentLevel) ? completeColor : notCompleteColor;
        }
    }

    public void WinCheck()
    {
        for (int i = 0; i < gearHolders.Length; i++)
        {
            if (!gearHolders[i].Complete)
            {
                return;
            }
        }

        currentLevel++;
        UpdateIndicators();
        //load new level
    }
}
