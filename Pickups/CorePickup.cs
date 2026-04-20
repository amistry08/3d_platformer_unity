using Unity.Collections;
using UnityEngine;
/// <summary>
/// Class which represents a score pickup
/// </summary>
public class CorePickup : Pickup
{
    [Header("Core Pickup Settings")]
    public int core = 1;
    public int requiredCoreParts = 5;

    private Objective objective = new Objective();

 
    public override void DoOnPickup(Collider collision)
    {
        if (collision.tag == "Player" && GameManager.instance != null)
        {
            GameManager.AddCoreParts(core);
            UpdateObjective();

        }
        base.DoOnPickup(collision);
    }

    private void UpdateObjective()
    {
        if (GameManager.coreParts == requiredCoreParts)
        {
            objective.showObjective = true;
            objective.hudText = "Repair the Portal";
            objective.objectivePopupText = "Found all parts! Repair the Portal";
        }
        else
        {
            objective.showObjective = true;
            objective.hudText = "Find Core Parts " + GameManager.coreParts + "/" + requiredCoreParts;
            objective.objectivePopupText = 5 - GameManager.coreParts + " cores remaining";
        }

        ObjectiveManager.Instance.SetObjective(objective);
    }

}
