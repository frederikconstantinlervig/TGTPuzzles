using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLevel : MonoBehaviour
{

    [Header("Gear holders")]
    [SerializeField]
    private GearHolder[] gearHolders;

    [SerializeField] //refarence til GearPuzzelManageren 
    private GearPuzzleManager manager;
    
    public GearHolder[] GearHolders { get => gearHolders; }
    public GearPuzzleManager Manager { get => manager; }

    public GearHolder SelectedGearHolder { get; set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        manager.LoadNextLevel();

    }
}
