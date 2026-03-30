
using Unity.Collections;
using UnityEngine;
using static UnityEngine.InputManagerEntry;
/// <summary>
/// Class which represents a score pickup
/// </summary>
public class GearPickup : Pickup
{
    [Header("Gear Pickup Settings")]
    [Tooltip("The amount of score gained when picked up.")]
    public int gear = 1;

    private Objective objective = new Objective();

    /// <summary>
    /// Description:
    /// When picked up, add score to the player via the game manager
    /// Inputs: Collider collision
    /// Outputs: N/A
    /// </summary>
    /// <param name="collision">The collider which caused this to be picked up</param>
    public override void DoOnPickup(Collider collision)
    {
        if (collision.tag == "Player" && GameManager.instance != null)
        {
            GameManager.AddGearParts(gear);
            UpdateObjective();
            
        }
        base.DoOnPickup(collision);
    }

    private void UpdateObjective()
    {
        if (GameManager.gearParts == 5)
        {
            objective.showObjective = true;
            objective.hudText = "Report Back to Dr. Arjun";
            objective.objectivePopupText = "Found all parts! Report back to Dr. Arjun";
        }
        else
        {
            objective.showObjective = true;
            objective.hudText = "Find Repair Parts" + GameManager.gearParts + "/5";
            objective.objectivePopupText = 5 - GameManager.gearParts + " parts remaining";
        }

        

      

        ObjectiveManager.Instance.SetObjective(objective);
    }

}