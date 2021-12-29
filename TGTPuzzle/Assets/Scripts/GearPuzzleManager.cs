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

    [Header("Levels")]
    [SerializeField]
    private GameObject[] gearLevels;

    private int currentLevel;




    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        UpdateIndicators();
        gearLevels[currentLevel].SetActive(true);
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

    public void LoadNextLevel()
    {
        gearLevels[currentLevel].SetActive(false);
        currentLevel++;
        UpdateIndicators();
        gearLevels[currentLevel].SetActive(true);
    }
}
