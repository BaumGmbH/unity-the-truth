using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public string deathScene;

    private void Start()
    {
        health = maxHealth;
    }

    int Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            SceneManager.LoadScene(deathScene);
        }

        return health;
    }
}