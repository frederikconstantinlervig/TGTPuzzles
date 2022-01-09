using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
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
    private GameObject[] levels;


    private Animator animator;

    private int levelIndex;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        levelIndex = 0;
        UpdateIndicators();
        levels[levelIndex].SetActive(true);
    }
    private void UpdateIndicators()
    {
        for (int i = 0; i < levelIndicarors.Length; i++)
        {
            levelIndicarors[i].color = (i < levelIndex) ? completeColor : notCompleteColor;
        }
    }

    public void LevelComplete()
    {
        animator.SetTrigger("LevelComplete");
    }


    public void LoadNextLevel()
    {
        levels[levelIndex].SetActive(false);
        levelIndex++;
        if (levelIndex < levels.Length)
        {
            UpdateIndicators();
            levels[levelIndex].SetActive(true);
        }
        else
        {
            UpdateIndicators();
            Debug.LogWarning("Win Not impemeted");
        }
    }
}
