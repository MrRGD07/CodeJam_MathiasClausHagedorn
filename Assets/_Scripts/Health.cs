using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health, _maxHealth = 1;
    [SerializeField] Image healthbar;

    private void Awake()
    {
        ResetHealth();
    }

    /// <summary>
    ///  Make the player take one damage. This function can be overloaded with a int for the damage value.
    /// </summary>
    /// <param name="TakeDamage">Makes the player take one damage.</param>
    /// <returns></returns>
    public void TakeDamage()
    {
        _health--;
        UpdateUI();
        SoundManager.instance.PlaySound(Sounds.TakeDamage);
        CheckIfDead();
    }

    /// <summary>
    ///  Make the player take a given value of damage.
    /// </summary>
    /// <param name="TakeDamage">Makes the player take a given amount of damage.</param>
    /// <returns></returns>
    public void TakeDamage(int damage)
    {
        _health -= damage;
        UpdateUI();
        SoundManager.instance.PlaySound(Sounds.TakeDamage);
        CheckIfDead();
    }

    /// <summary>
    ///  Make the player restore one health. This function can be overloaded with a int for the heal value.
    /// </summary>
    /// <param name="RestoreHealth">Makes the player restore one health.</param>
    /// <returns></returns>
    public void RestoreHealth()
    {
        if (_health < _maxHealth)
        {
            _health++;
        }
        UpdateUI();
        SoundManager.instance.PlaySound(Sounds.Heal);
    }

    /// <summary>
    ///  Make the player restore a given value of health.
    /// </summary>
    /// <param name="RestoreHealth">Makes the player restore a given amount of health.</param>
    /// <returns></returns>
    public void RestoreHealth(int healing)
    {
        if (_health + healing > _maxHealth)
        {
            _health = _maxHealth;
        } else _health += healing;
        UpdateUI();
        SoundManager.instance.PlaySound(Sounds.Heal);
    }

    /// <summary>
    ///  Resets the player health and set it to max health, this can be used when reseting game.
    /// </summary>
    /// <param name="ResetHealth">Resets the player health and set it to max health.</param>
    /// <returns></returns>
    public void ResetHealth()
    {
        _health = _maxHealth;
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthbar.fillAmount = (float)_health / _maxHealth;
    }

    private void CheckIfDead()
    {
        if (_health <= 0)
        {
            GameManager.instance.GameEnded();
            _health = 0;
        }
    }
}