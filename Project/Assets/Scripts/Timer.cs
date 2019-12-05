using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float maxTime = 60;
    private float timer;
    Text text;
    private bool won = false;
    private bool stop = false;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        timer = maxTime;
    }

    public void Reset(float max) {
        maxTime = max;
        timer = maxTime;
        won = false;
        text.color = Color.white;
    }

    public void Stop(){
        stop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        if (timer >= 0) {
            timer -= Time.deltaTime;
            if (timer <= 10) text.color = Color.red;
            text.text = ((int) timer).ToString() + " s";
        } else if (!won) {
            text.text = "Time's up!";
            GameManager.S.NextRoom("Kitchen", "LivingRoom");
            won = true;
        }
    }
}
