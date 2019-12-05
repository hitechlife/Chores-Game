using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(UpdateScore());
    }

    // Update is called once per frame
    IEnumerator UpdateScore()
    {
        while(true) {
            text.text = "Score: " + GameManager.S.GetScore();
            yield return null;
        }
    }
}
