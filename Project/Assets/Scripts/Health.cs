using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    private bool isDefending = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool IsDead() {
        return healthBar.value <= healthBar.minValue;
    }

    public void ActivateDefense(bool b) {
        isDefending = b;
    }

    public bool DefenseActive() {
        return isDefending;
    }

    public void IncreaseHealth(float val) {
        if (healthBar.value < healthBar.maxValue) healthBar.value += val;
    }

    public void decreaseHealth(float f) {
        if (healthBar.value > healthBar.minValue) healthBar.value -= f;
    }
}
