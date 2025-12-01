using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //This script manages the game. Ie. which level to load, enemies to spawn, player spawning, 



    public bool gameOver;
    //Map
    public List<GameObject> maps;
    //Timer
    public float durationPreMatchTimer;
    private float remainingDuration;
    public TextMeshProUGUI timerText;
    public Image timerBarFill;
    public Image timer;
    //Match Start Text
    public TextMeshProUGUI matchStartText;
    public int textGrowSpeed;
    public float durationForMatchStartText;



    // Start is called before the first frame update
    void Start()
    {

        //ReSet Game State 
        ResetGame();
        //Spawn Map
        
        //Spawn Enemies
        //Spawn Player
        //Spawn Pickups
        //Stop Enemies/Players
        //Display Controls

        //Pre Match Countdown
        PreMatchCountdown(durationPreMatchTimer);
        //Begin Match
        //Had to move because ie enumerators aren't sequential.

    }

    public void ResetGame() 
    {
        timer.gameObject.SetActive(false);
        matchStartText.gameObject.SetActive(false);
    }

    private void SpawnMap() 
    {
        int maptoPick = Random.Range(1, maps.Count+1);
        //TODO should I have Maps be a class? So I just have all data?
        maps[maptoPick].gameObject.SetActive(true);



    }
    private void PreMatchCountdown(float time) 
    {
        //Enable Timer
        timer.gameObject.SetActive(true);
        remainingDuration = time;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer() 
    {
        while (remainingDuration >= 0f) 
        {
            //Seconds(00) and hundredths of a second(00)
            int seconds = (int)remainingDuration;
            int hundredths = (int)((remainingDuration - seconds) * 100);
            timerText.text = $"{seconds:00}:{hundredths:00}";
            timerBarFill.fillAmount = Mathf.InverseLerp(0, durationPreMatchTimer, remainingDuration);
            // Decrease by deltaTime each frame
            remainingDuration -= Time.deltaTime;
            yield return null; // wait until next frame

            //timerText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
            //timerBarFill.fillAmount = Mathf.InverseLerp(0, durationPreMatchTimer, remainingDuration);
            //remainingDuration--;
            //yield return new WaitForSeconds(1f);
        }
        BeginMatch(durationForMatchStartText);
    }

    void BeginMatch(float time) 
    {
        //Disable Timer
        timer.gameObject.SetActive(false);
        //Enable
        matchStartText.gameObject.SetActive(true);
        remainingDuration = time;
        StartCoroutine(GrowText(matchStartText));
    }

    private IEnumerator GrowText(TextMeshProUGUI textToGrow) 
    {
        while(remainingDuration >= 0f) 
        {
            float scaleX = textToGrow.transform.localScale.x;
            float scaleY = textToGrow.transform.localScale.y;
            float scaleRate = textGrowSpeed * Time.deltaTime;
            textToGrow.transform.localScale = new Vector3(scaleX + scaleRate,scaleY + scaleRate,1f);
            textToGrow.alpha = Mathf.InverseLerp(0, durationForMatchStartText, remainingDuration);
            remainingDuration -= Time.deltaTime;
            yield return null;
        }   
       
    }
    
}
