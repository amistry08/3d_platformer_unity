using UnityEngine;

public class SetFinalObjective : MonoBehaviour
{
    
    private Objective objective = new Objective();

    public Objective ActivateFinalObjective()
    {
        objective.showObjective = true;
        objective.objectivePopupText = "Ready to board the Spacecraft";
        objective.hudText = "Board the Spacecraft";

       return objective;
    }
}
