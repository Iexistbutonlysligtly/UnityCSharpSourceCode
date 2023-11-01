using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class  Timer : MonoBehaviour
{
    public int timeLeft = 20;
    public Text countdownText;
    GameObject timeuptext;
    GameObject timeend;
    public int timeCountdownPlays;
    public AudioClip CountDownSound;
    public AudioSource SoundSource;
    

    
    void Start()
    {
        timeuptext = GameObject.Find("TimeUp");
        timeuptext.SetActive(false);
        timeend = GameObject.Find("Timer Text");
        StartCoroutine("LoseTime");
        SoundSource.clip = CountDownSound;
    }

  
    void Update()
    {
       
        countdownText.text = ("" + timeLeft);
            

        if (timeLeft <= 0)
        {
            timeuptext.SetActive(true);
            timeend.GetComponent<Text>().enabled = false;
            
        }

        if (timeLeft == timeCountdownPlays)
        {
            SoundSource.Play();
        }
            
            
        
      
       
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}