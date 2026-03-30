using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pickup-derived class which handles a key collectable
/// </summary>
public class KeyPickup : Pickup
{
   
    [Header("Key Settings")]
    [Tooltip("The ID of the key used to determine which doors it unlocks (unlocks doors with matching IDs)\n" +
        "A key ID of 0 allows the player to open unlocked doors, and is therefore pointless.")]
    public int keyID = 0;

    [Header("Objectives")]
    public Objective objective = null;

    /// <summary>
    /// Description:
    /// When picked up, adds to the player's score
    /// Inputs: Collider2D collision
    /// Outputs: N/A
    /// </summary>
    /// <param name="collision">The collider that picked up this key</param>
    public override void DoOnPickup(Collider collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {
            KeyRing.AddKey(keyID);
            PlayerPrefs.SetInt("Flag_HasBaseKey", 1);

            if (objective != null)
            {
                ObjectiveManager.Instance.SetObjective(objective);
            }

        }
        base.DoOnPickup(collision);
    }
}
