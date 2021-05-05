using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    protected virtual void Start() {
        currentHealth = maxHealth;
    }

    public void ReduceCurrentHealth(float x) {
        currentHealth = currentHealth - x;
    }
    public float GetCurrentHealth() {
        return currentHealth;
    }
    public float GetMaxHealth() {
        return maxHealth;
    }
    public abstract void TakeDamage(float damage);

}
