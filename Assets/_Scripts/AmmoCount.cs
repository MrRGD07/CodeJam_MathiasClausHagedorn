using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _ammoCount;
    private PlayerShooter playerShooter;

    void Start()
    {
        playerShooter = FindObjectOfType<PlayerShooter>();
    }

    void Update()
    {
        _ammoCount.SetText(playerShooter.CurrentAmmo.ToString());
    }
}
