using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour _collectableBehaviour;

    public AudioSource audioSource;
    public AudioClip CollectableSound;

    private void Awake(){
        _collectableBehaviour = GetComponent<ICollectableBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision){

        var player = collision.GetComponent<PlayerMovement>();
        if (player != null){
            _collectableBehaviour.OnCollected(player.gameObject);
            audioSource.PlayOneShot(CollectableSound);
            Destroy(gameObject);
        }
    }
}
