using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearHolder : MonoBehaviour
{
    // vi børlave en custom editor til de her 


    #region feilds
    [SerializeField] //refarence til GearPuzzelManageren 
    private GearPuzzleManager manager;
    [SerializeField] //positionen hvor et gear skal være hvis den er seleted fra denne holder 
    private Transform selectedPos;
    [SerializeField] //arayet med de spawnpoints holderen indeholder
    private Transform[] spawnPoints;
    [SerializeField] //arayet med de gears holderen skal starte med
    private Gear[] initialGears;
  
    private Stack<Gear> gears; //stacken med gears
    #endregion

    #region properties
    public bool Complete
    {
        get
        {
            if (gears == null) { return false; }

            //hvis holderen er tom teller den som complete 
            if (gears.Count < 1) { return true; }

            //hvis holder ikke er fuld kan den ikke være complete
            if (gears.Count < spawnPoints.Length) { return false; }

            //tjækker om alle gears har samme id
            int id = gears.Peek().Id;
            foreach(Gear g in gears)
            {
                //hvis der er to gears der ikke har samme id ikke, er den ikke complete
                if(g.Id != id) 
                {
                    return false;
                }
            }

            //hvis den når her ned betyder det at holderen er fuld og at den kun indeholder gear 
            return true;
        }
    }

    private Stack<Gear> Gears { get { return gears; } }
    #endregion

    void Start()
    {
        //oprettter den tomme stack af gears 
        gears = new Stack<Gear>(); 

        //tilføjer de gears der ligger i initial gaears srreyet til staccken
        for (int i = 0; i < initialGears.Length; i++)
        {
            AddGear(initialGears[i]);
        }
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (manager.SelectedGearHolder == null)
        {
            manager.SelectedGearHolder = SelectHolder();
        }
        else if (manager.SelectedGearHolder != this)
        {
            if (PlaceGearFromManager())
            {
                manager.SelectedGearHolder = null;
            }
            else 
            {
                manager.SelectedGearHolder.DeselectHolder();
                manager.SelectedGearHolder = SelectHolder();
            }
        }
        else
        {
            DeselectHolder();
            manager.SelectedGearHolder = null;
        }
    }

    private bool PlaceGearFromManager()
    {
        if (gears.Count >= spawnPoints.Length) { return false; }

        // måske ikke den bedste måde man kunne bruge try catch eller noget andet måske ?
        if (gears.Count > 0)
        {
            if (gears.Peek().Id != manager.SelectedGearHolder.Gears.Peek().Id) { return false; }
        }

        AddGear(manager.SelectedGearHolder.Gears.Pop());

        return true;
    }


    private GearHolder SelectHolder()
    {
        if (Complete) { return null; }

        gears.Peek().transform.position = selectedPos.position;
        return this;
    }

    public void DeselectHolder()
    {
        gears.Peek().transform.position = spawnPoints[gears.Count - 1].position;
    }



    private void AddGear(Gear gear)
    {

        if (gears.Count >= spawnPoints.Length) { return; }


        gears.Push(gear);
        gear.transform.position = spawnPoints[gears.Count - 1].position;

        if (Complete)
        {
            foreach(Gear g in gears)
            {
                g.Animate();
            }
            manager.WinCheck();
        }
    }
}
