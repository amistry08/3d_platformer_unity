using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class which manages a checkpoint
/// </summary>
public class Checkpoint : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The location this checkpoint will respawn the player at")]
    public Transform respawnLocation;
    [Tooltip("The animator for this checkpoint")]
    public Animator checkpointAnimator = null;
    [Tooltip("The name of the parameter in the animator which determines if this checkpoint displays as active")]
    public string animatorActiveParameter = "isActive";
    [Tooltip("The effect to create when activating the checkpoint")]
    public GameObject checkpointActivationEffect;

    [Header("Checkpoint Settings")]
    [Tooltip("Interactive Hint for Player when walking near checkpoint")]
    public TextMeshPro checkpointHint;
    [Tooltip("Interactive Hint Color when checkpoint inactive")]
    public Color inactiveColor = Color.red;
    [Tooltip("Interactive Hint Color when checkpoint active")]
    public Color activeColor = Color.green;



    private void Awake()
    {
        checkpointHint.color = inactiveColor;
        checkpointHint.gameObject.SetActive(false);
    }

    /// <summary>
    /// Description:
    /// Standard unity function called when a trigger is entered by another collider
    /// Input:
    /// Collider collision
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collider that has entered the trigger</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Health>() != null)
        {


            if (CheckpointManager.instance.isCheckpointActivated(this))
            {
                checkpointHint.color = activeColor;
                checkpointHint.text = "Active";
            }

            checkpointHint.gameObject.SetActive(true);


            if (checkpointActivationEffect != null && CheckpointManager.instance.isCheckpointActivated(this) == false)
            {

                Instantiate(checkpointActivationEffect, transform.position, Quaternion.identity, null);
            }

            CheckpointManager.instance.RegisterCheckpoint(this);
            checkpointAnimator.SetBool(animatorActiveParameter, true);
           
        
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Player")
        {
            checkpointHint.gameObject.SetActive(false);
        }
    }
}
