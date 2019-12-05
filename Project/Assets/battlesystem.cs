using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYERATTACKED, PLAYERHEALED, ENEMYTURN, WON, LOST} 

public class battlesystem : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public string soundname;
    public string soundname2;
    public string soundname3;
    public string soundname4;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public float camShakeAmt = 0.1f;
    ScreenShake camShake;

    public HUDScript playerHUD;
    public HUDScript enemyHUD;

    public BattleState state;
    public List<GameObject> PlayerButtons;
    public List<GameObject> ReplaceButtons;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        camShake = GameObject.Find("GameManager").GetComponent<ScreenShake>();
        if (camShake == null)
        {
            Debug.Log("Fuck");
        }
         FindObjectOfType<SoundManager>().Play(soundname);

    }

    IEnumerator SetupBattle()
    {
    	GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    	playerUnit = playerGO.GetComponent<Unit>();
    	GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
    	enemyUnit = enemyGO.GetComponent<Unit>();

    	dialogueText.text = "Your sibling " + enemyUnit.unitName + " is here to show you who's the boy of the house.";

    	playerHUD.SetHUD(playerUnit);
    	enemyHUD.SetHUD(enemyUnit);

    	yield return new WaitForSeconds(2f);

    	state = BattleState.PLAYERTURN;
    	PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You used Good Ol' Maturity!";
        camShake.Shake (camShakeAmt, 0.1f);
        FindObjectOfType<SoundManager>().Play(soundname2);
        state = BattleState.PLAYERATTACKED;
        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EnemyTurn()
    {
        string[] fightingwords = new string[]{" used EPIC TATTLE TALE!!", " LIES to MOMMA about you doing chores!!", " used More Good-Boy Points Bragging Rights!", " used PUNCH IN FACE!!",
        " used LEGO Brick assault!", " used Power Ranger Punch!", " used FortNite Fist!", " used TERROR TANTRUM!!", " used Wee-wee in your Tea!", " used EPIC ROAST he learned from Brandon from school!!", 
        " used hurtful BAD WORDS he learned from TikTok!!", " used Roblox RAMPAGE!!"};

        dialogueText.text = enemyUnit.unitName + fightingwords[Random.Range(0, fightingwords.Length)];

        yield return new WaitForSeconds(2f);

        //bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        bool isDead = playerUnit.TakeDamage(Random.Range(0, 30));
        camShake.Shake (camShakeAmt, 0.2f);
        FindObjectOfType<SoundManager>().Play(soundname2);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        foreach (GameObject g in PlayerButtons) {
            g.SetActive(false);
        }

        if(state == BattleState.WON)
        {
            for (int i = 0; i < ReplaceButtons.Count; i++) {
                if (i == 0) continue;
                ReplaceButtons[i].SetActive(true);
            }
            FindObjectOfType<SoundManager>().StopPlaying(soundname);
            FindObjectOfType<SoundManager>().Play(soundname3);
            dialogueText.text = "YOU DID IT! YOU WIN A TRIP TO HAPPY LAND!!";
        } else if (state == BattleState.LOST)
        {
            foreach (GameObject g in ReplaceButtons) {
                g.SetActive(true);
            }
            FindObjectOfType<SoundManager>().StopPlaying(soundname);
            FindObjectOfType<SoundManager>().Play(soundname4);
            dialogueText.text = "You lost! Why does evil prevail?";
        }
    }

    void PlayerTurn()
    {
        string[] trashtalk = new string[]{"MEANIE BRO: What are you gonna do now, MAMA'S BOY?", "MEANIE BRO: You know I have more Good-Boy Stars than you!", 
        "MEANIE BRO: Just wait, I'm gonna tell dad that you called me the F-word!", "MEANIE BRO: A bug like you can't hurt an EPIC PRO ROBLOX GAMER like me!", 
        "MEANIE BRO: Nyah nyah nah nah nyah!!", "MEANIE BRO: I'm gonna make you cry to mom!", "MEANIE BRO: I'm gonna show you what's up you Goody Two Shoes!", 
        "MEANIE BRO: *Dabs*", "MEANIE BRO: I'm gonna tell Chester at school about your SECRET!", "MEANIE BRO: What's wrong? Are you afraid?!", 
        "MEANIE BRO: I'm gonna show you that my procedurally generated attack points are going to trump against your measly fixed value attack!", 
        "MEANIE BRO: You got nothing!!", "MEANIE BRO: What are you gonna do? Mature me to death?", "MEANIE BRO: Oh wow, what a big boy you are. Wow.",
        "MEANIE BRO: Only stupid people do chores.", "MEANIE BRO: I purposely didn't do the chores so I can make you do them. Ha!"};

    	dialogueText.text = trashtalk[Random.Range(0, trashtalk.Length)];
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(Random.Range(0, 30));
        state = BattleState.PLAYERHEALED;
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You've gained some self-confidence.";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
    	if (state != BattleState.PLAYERTURN)
    		return;

    	StartCoroutine(PlayerAttack());
    }
    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

}
