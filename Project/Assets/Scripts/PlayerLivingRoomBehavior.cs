using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivingRoomBehavior : MonoBehaviour
{
    public bool vactime = false;
    public GameObject vac;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {
        if (GameManager.S.DisabledMovement()) {
            return;
        }
        // if (Input.GetKeyDown(KeyCode.R)) {
        //     if (transform.childCount > 0) {
        //         transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        //         transform.GetChild(0).GetComponent<cakeslice.Outline>().eraseRenderer = false;

        //         foreach (GameObject g in GameObject.FindGameObjectsWithTag("dirt")) {
        //             g.GetComponent<cakeslice.Outline>().eraseRenderer = true;
        //         }
        //         transform.GetChild(0).parent = null;
        //     }
        // }
    }

    void OnTriggerStay2D (Collider2D other) {
        if (Input.GetKey(KeyCode.Space)) {
            cakeslice.Outline outline = other.GetComponent<cakeslice.Outline>();
            switch (other.tag) {
                case "dirt":
                    // If we're carrying the vacuum, pick it up
                    if (transform.childCount == 1) {
                        Destroy(other.gameObject);
                        GameManager.S.UpdateScore(1);

                    }
                    break;
                case "vacuum":
                    vactime = true;
                    vac.SetActive(false);
                    other.GetComponent<cakeslice.Outline>().eraseRenderer = true;
                    other.transform.parent = transform;
                    other.GetComponent<BoxCollider2D>().enabled = false;

                    foreach (GameObject g in GameObject.FindGameObjectsWithTag("dirt")) {
                        g.GetComponent<cakeslice.Outline>().eraseRenderer = false;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
