using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Relies on player having no children except whatever it's picked up
public class PlayerKitchenBehavior : MonoBehaviour
{
    [SerializeField] private GameObject sink = null;
    public bool pickedUp = false;

    // Update is called once per frame
    void Update()
    {
        //TODO: not hardcode this
        if (GameManager.S.DisabledMovement()) {
            return;
        }

        //TODO: make this also E
        // Drops a dish
        if (Input.GetKeyDown(KeyCode.Space) && pickedUp == true) {
            if (transform.childCount > 0) {
                pickedUp = false;

                transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
                if (sink) sink.GetComponent<cakeslice.Outline>().eraseRenderer = true;
                transform.GetChild(0).parent = null;
                //StartCoroutine(ExecuteAfterTimela());

                foreach (GameObject g in GameObject.FindGameObjectsWithTag("dish")) {
                    g.GetComponent<cakeslice.Outline>().eraseRenderer = false;
                } 
            }
            /*IEnumerator ExecuteAfterTimela()
            {
                yield return new WaitForSeconds(0.25f);
                pickedUp = false;
            }*/
        }

    }

    void OnTriggerStay2D (Collider2D other) {
        if (Input.GetKey(KeyCode.Space)) {
            cakeslice.Outline outline = other.GetComponent<cakeslice.Outline>();
            switch (other.tag) {
                case "dish":
                    // If we're not carrying anything else, pick it up
                    if (transform.childCount == 0) {
                        other.transform.parent = transform;
                        if (outline) outline.eraseRenderer = true;
                        if (sink) sink.GetComponent<cakeslice.Outline>().eraseRenderer = false;
                        other.GetComponent<CircleCollider2D>().enabled = false;
                        pickedUp = true;
                        //StartCoroutine(ExecuteAfterTime());

                        foreach (GameObject g in GameObject.FindGameObjectsWithTag("dish")) {
                            if (g.transform.parent != this.transform) {
                                g.GetComponent<cakeslice.Outline>().eraseRenderer = true;
                            }
                        }
                        /*IEnumerator ExecuteAfterTime()
                        {
                            yield return new WaitForSeconds(0.25f);
                            pickedUp = true;
                        }*/

                    }
                    break;
                case "sink":
                    // If we have a dish, destroy it
                    if (transform.childCount > 0 && transform.GetChild(0).tag == "dish") {
                        Destroy(transform.GetChild(0).gameObject);
                        GameManager.S.UpdateScore(1);
                        other.GetComponent<cakeslice.Outline>().eraseRenderer = true;
                        pickedUp = false;

                        foreach (GameObject g in GameObject.FindGameObjectsWithTag("dish")) {
                            g.GetComponent<cakeslice.Outline>().eraseRenderer = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
