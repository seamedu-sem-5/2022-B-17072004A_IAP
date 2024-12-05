using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Slider healthSlider;
    public int healthPoint;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(UpdateHealthBar(damage));
        if(currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void HealHealth(int hp)
    {
        currentHealth += hp;
        healthSlider.value += hp * 0.01f;

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            healthSlider.value = maxHealth * 0.01f;
        }
    }

    IEnumerator UpdateHealthBar(int damage)
    {
        yield return new WaitForSeconds(0.5f);
        healthSlider.value -= damage * 0.01f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "HP")
        {
            HealHealth(healthPoint);
            //Destroy(collision.gameObject,0.1f);
        }
    }
}
