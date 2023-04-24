using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Stats : MonoBehaviour
{
   // public GameObject objHealth;
    float health;
    float maxHealth = 100;
    bool DamageTaken = false;
    float regen = 2;
    float second = 0;
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    void TakeDamage()
    {
        if (DamageTaken == true)
        {
            health -= 5;
            DamageTaken = false;
            second = 0f;
        }
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void HealthRegen()
    {
        if (health < maxHealth && DamageTaken == false)
        {
            second += Time.deltaTime;
            if (second >= regen)
            {
                health = Mathf.Min(health + regen, maxHealth);
                second = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            DamageTaken = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //objHealth.GetComponent<TextMeshProUGUI>().text = "Health: " + health.ToString();
        TakeDamage();
        HealthRegen();
    }
}
