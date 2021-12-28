using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearHolder : MonoBehaviour
{
    [SerializeField]
    private GearPuzzleManager manager;
    [SerializeField]
    private Transform selectedPos;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Gear[] initialGears;
  
    private Stack<Gear> gears;

    public bool Complete
    {
        get
        {
            if (gears.Count < spawnPoints.Length) { return false; }

            int id = gears.Peek().Id;
            foreach(Gear g in gears)
            {
                if(g.Id != id) 
                {
                    return false;
                }
            }

            return true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gears = new Stack<Gear>();

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
        if (manager.SelectedGear == null)
        {
            manager.SelectedGear = RemoveGear();
        }
        else
        {
            if (AddGear(manager.SelectedGear))
            {
                manager.SelectedGear = null;
            }
        }
    }

    public bool AddGear(Gear gear)
    {
        if (gears.Count >= spawnPoints.Length) { return false; }

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

        return true;
    }

    public Gear RemoveGear()
    {
        if (gears.Count < 1 || Complete) { return null; }

        Gear gear = gears.Pop();

        gear.transform.position = selectedPos.position;

        return gear;
    }
}
