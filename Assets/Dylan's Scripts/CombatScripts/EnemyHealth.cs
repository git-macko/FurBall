using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Image healthBar;
    private float healthAmount;
    private float healthMax;

    
    // Start is called before the first frame update
    void Start()
    {
        healthMax = transform.parent.gameObject.GetComponent<EnemyCombat>().health;
    }

    // Update is called once per frame
    void Update()
    {
        healthAmount = transform.parent.gameObject.GetComponent<EnemyCombat>().health;
        transform.rotation = Quaternion.identity;
    }

    public void TakeDamage(float damage) {
        healthBar.fillAmount = healthAmount / healthMax;
    }

    public void Heal(float healingAmount) {
        // healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / healthMax;
    }
}
