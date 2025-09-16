using UnityEditor.Build.Reporting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public Sprite heart;
    public Image[] hearts;
 
    private void Start()
    {
        currentHealth = maxHealth;
        // healthText.text = "Health: " + currentHealth.ToString();
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].enabled = false;
            } /*
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            } */
        }
    }


    public void ChangeHealth(int amount)
    { 

        currentHealth += amount;
        // healthTextAnim.Play("TextUpdate");

        // healthText.text = "Health: " + currentHealth.ToString();

        if (currentHealth == 0)
        {
            gameObject.SetActive(false);
        }
    }
}

