using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAbilityBehaviour : MonoBehaviour, ICollectableBehaviour
{
    public void OnCollected(GameObject player) {
        player.GetComponent<PlayerMovement>().ActivateShotgunAbility();
    }
}
