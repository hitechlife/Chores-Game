using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private GameObject other;
    [SerializeField] bool isPlayer = false;
    [SerializeField] [Range(0.1f, 0.5f)] private float attackPower = 0.2f;
    [SerializeField] [Range(0.1f, 0.5f)] private float recoverPower = 0.1f;
    Health otherHealth;
    Fighter otherFighter;
    Health myHealth;
    bool isAttacking = false;

    void Start()
    {
        myHealth = GetComponent<Health>();
        otherHealth = other.GetComponent<Health>();
        otherFighter = other.GetComponent<Fighter>();
        if (isPlayer) isAttacking = true;
        StartCoroutine(TurnRoutine());
    }

    public bool Attacking {
        get {
            return isAttacking;
        }
        set {
            isAttacking = value;
        }
    }

    public bool Defending {
        get {
            return myHealth.DefenseActive();
        }
        set {
            myHealth.ActivateDefense(value);
        }
    }

    public void ExecuteMove(string t) {
        switch (t) {
            case "Attack":
                break;
            case "Defend":
                break;
            case "Replenish":
                break;
            default:
                // shouldn't be here
                return;
        }
    }

    // A attack, D defend, H health
    IEnumerator TurnRoutine()
    { 
        while (true) {
            // Player gets first turn
            if (isPlayer && isAttacking) {
                Debug.Log("Player attacking");
                if (Input.GetKey(KeyCode.A)) {
                    //TODO: do attack animation
                    if (!otherFighter.Defending) otherHealth.decreaseHealth(attackPower);
                    otherFighter.Defending = false;

                    if (otherHealth.IsDead()) {
                        GameManager.S.ShowGameOver();
                        break;
                    }

                    isAttacking = false;
                    otherFighter.Attacking = true;
                } else if (Input.GetKey(KeyCode.D)) {
                    //TODO: do defense animation
                    myHealth.ActivateDefense(true);

                    isAttacking = false;
                    otherFighter.Attacking = true;
                } else if (Input.GetKey(KeyCode.H)) {
                    //TODO: do health animation
                    myHealth.IncreaseHealth(recoverPower);

                    isAttacking = false;
                    otherFighter.Attacking = true;
                } else {
                    yield return null;
                    continue;
                }

            // AI is attacking
            } else if (isAttacking) {
                Debug.Log("AI Attacking");
                int rand = Random.Range(1, 3);
                switch (rand) {
                    //TODO: add the animations
                    case 1:
                        if (!otherFighter.Defending) otherHealth.decreaseHealth(attackPower);
                        otherFighter.Defending = false;
                        break;
                    case 2:
                        myHealth.ActivateDefense(true);
                        break;
                    default:
                        myHealth.IncreaseHealth(recoverPower);
                        break;
                }
                if (otherHealth.IsDead()) {
                    GameManager.S.ShowGameOver();
                    break;
                }
                isAttacking = false;
                otherFighter.Attacking = true;
            } else {
                Debug.Log("Waiting");
                //Wait until other fighter does something
                yield return new WaitUntil(() => isAttacking == true);
            }

            yield return null;
        }
    }
}
