using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLevel : MonoBehaviour
{

    [Header("Gear holders")]
    [SerializeField]
    private GearHolder[] gearHolders;

    [SerializeField] //refarence til GearPuzzelManageren 
    private PuzzleManager manager;
    
    public GearHolder[] GearHolders { get => gearHolders; }
    public PuzzleManager Manager { get => manager; }

    public GearHolder SelectedGearHolder { get; set; }

    public void WinCheck()
    {
        for (int i = 0; i < gearHolders.Length; i++)
        {
            if (!gearHolders[i].Complete)
            {
                return;
            }
        }

        manager.LevelComplete();

    }
}
