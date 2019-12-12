using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Timer timer;
    [SerializeField] Vector3 livingRoomPosition;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject door;
    [SerializeField] GameObject finalDoor;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject successScreen;
    public static GameManager S;
    private int score = 6;
    private bool disabledMovement = false;
    private string currRoom = "Kitchen";
    bool won = false;
    public GameObject xtradirt;


    void Start()
    {
        S = this;
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            MainMenu();
        }
        switch (currRoom) {
            case "Kitchen":
                if (score <= 0 && !won) {
                    won = true;
                    NextRoom("Kitchen", "LivingRoom");
                }
                break;
            case "LivingRoom":
                if (score <= 0 && !won) {
                    won = true;
                    NextRoom("LivingRoom", "FightScene");
                }
                break;
            default:
                break;
        }
    }

    public void UpdateScore(int s) {
        score -= s;
    }

    public bool DisabledMovement() {
        return disabledMovement;
    }

    public void NextRoom(string lastRoom, string nextRoom) {
        disabledMovement = true;
        if (!won || gameOverScreen.activeSelf) {
            ShowGameOver();
            return;
        }
        timer.Reset(15000);
        //TODO: remove all children, move player's location, reenable movement, reset timer

        switch (lastRoom) {
            case "Kitchen":
                player.GetComponent<PlayerKitchenBehavior>().enabled = false;
                xtradirt.SetActive(true);
                door.SetActive(false);
                player.GetComponent<PlayerLivingRoomBehavior>().enabled = true;
                timer.Reset(60);
                score = 10;
                if (player.transform.childCount > 0) player.transform.GetChild(0).parent = null;
                player.transform.position = livingRoomPosition;
                disabledMovement = false;
                currRoom = "LivingRoom";
                won = false;
                break;
            case "LivingRoom":
                won = false;
                // winText.SetActive(true);
                finalDoor.SetActive(true);
                timer.Stop();
                //TODO: open the dialogue here.
                SceneManager.LoadScene("FightScene");
                break;
        }
    }

    public int GetScore() {
        return score;
    }

    public void ShowGameOver() {
        disabledMovement = true;
        gameOverScreen.SetActive(true);
    }

    public void ShowSuccessScreen() {
        successScreen.SetActive(true);
    }

    public void ReloadScene() {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame() {
        SceneManager.LoadScene("Channel Game");
    }

        public void MainMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame() {
        //End Game Here
        Application.Quit();
    }
}
