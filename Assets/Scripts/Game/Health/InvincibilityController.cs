using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController _healthController;

    private void Awake(){
        _healthController = GetComponent<HealthController>();
    }


    public void StartInvincibility(float invincibilityDuration){
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration){
        // Accede al objeto hijo que contiene el SpriteRenderer
        SpriteRenderer spriteRenderer = transform.Find("Graphics").GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color flashColor = Color.red;
        float flashDuration = 0.1f; // Duración del parpadeo en segundos

        _healthController.IsInvincible = true;

        while (invincibilityDuration > 0)
        {
            spriteRenderer.color = (spriteRenderer.color == originalColor) ? flashColor : originalColor;
            yield return new WaitForSeconds(flashDuration);
            invincibilityDuration -= flashDuration;
        }

        spriteRenderer.color = originalColor;
        _healthController.IsInvincible = false;
    }
}
