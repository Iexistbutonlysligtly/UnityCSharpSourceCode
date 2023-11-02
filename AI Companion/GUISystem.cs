using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.ObjectChangeEventStream;
using Image = UnityEngine.UI.Image;

public class GUISystem : MonoBehaviour
{

    private RawImage moodFace;

    TMP_Text currentState;
    TMP_Text OberveText;

    Companion Companion;

    GameObject InfoButton;
    Animator InfoButtonAni;

    GameObject InfoTab;
    bool info;

    GameObject PauseTab;
    bool paused;

    AudioSource bSound;

    public UnityEngine.UI.Slider redSlider;
    public UnityEngine.UI.Slider greenSlider;
    public UnityEngine.UI.Slider blueSlider;

    public UnityEngine.UI.Slider aSlider;

    public Material targetMaterial; // Reference to the material you want to change


    Image[] nImages;
    RawImage[] images;
    TMP_Text[] text;


    private void Awake()
    {

        images = FindObjectsOfType<RawImage>();
        text = FindObjectsOfType<TMP_Text>();
        nImages = FindObjectsOfType<Image>();
    }



    // Start is called before the first frame update
    void Start()
    {

        GameObject mf = GameObject.Find("MoodFace");
        moodFace = mf.GetComponent<RawImage>();


        GameObject cS = GameObject.Find("CurrentState");
        currentState = cS.GetComponent<TMP_Text>();

        GameObject oS = GameObject.Find("ObserveState");
        OberveText = oS.GetComponent<TMP_Text>();




        Companion = FindObjectOfType<Companion>();


         InfoTab = GameObject.Find("InfoTab");
      
        InfoTab.SetActive(false);


        PauseTab = GameObject.Find("Pause");
        PauseTab.SetActive(false);


        InfoButton = GameObject.Find("InfoButton");
        InfoButtonAni = InfoButton.GetComponent<Animator>();


       


        GameObject Canvas = GameObject.Find("Canvas");
        bSound = Canvas.GetComponent<AudioSource>();


        // Add listeners to the sliders to detect value changes
        redSlider.onValueChanged.AddListener(OnColorSliderValueChanged);
        greenSlider.onValueChanged.AddListener(OnColorSliderValueChanged);
        blueSlider.onValueChanged.AddListener(OnColorSliderValueChanged);
        aSlider.onValueChanged.AddListener(OnColorSliderValueChanged);


        aSlider.value = 1;





        targetMaterial.color = new Color(164f / 255f, 163f / 255f, 163f / 255f);



    }

    // Update is called once per frame
    void Update()
    {
        MoodFace();



        currentState.text = "CurrentState: " + Companion.currentState;
        OberveText.text = "ObserveState: " + Companion.currentOberveState;


        //if (InfoButtonAni.GetCurrentAnimatorStateInfo(0).IsName(""))
        //    InfoButtonAni.ResetTrigger("Info");



        if (Input.GetKeyUp(KeyCode.P))
            Pause();


    }





    public void Pause()
    {

        bSound.PlayOneShot(Resources.Load<AudioClip>("Click"), 1F);
        paused = !paused;

      
            PauseTab.SetActive(paused);

        Time.timeScale = paused ? 0f : 1.0f;






    }



    public void IButton()
    {
      

        InfoButtonAni.SetTrigger("Info");

        bSound.PlayOneShot(Resources.Load<AudioClip>("OfInterest"), 1F);

     

    }

    public void IButtonClick()
    {



       
        bSound.PlayOneShot(Resources.Load<AudioClip>("Click"), 1F);

    }


    void MoodFace()
    {

        if (Companion.currentState == "Normal")
        {

            moodFace.texture = Resources.Load<Texture2D>("UI/MoodHappy");


        }


       else if (Companion.currentState == "Playful")
        {

            moodFace.texture = Resources.Load<Texture2D>("UI/MoodPlayful");


        }




        else if (Companion.currentState == "Distressed")
        {

            moodFace.texture = Resources.Load<Texture2D>("UI/MoodSad");


        }




    }

    public void Info()
    {
        info = !info;

        InfoTab.SetActive(info);

       



    }











    private void OnColorSliderValueChanged(float value)
    {
        Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value, aSlider.value);
        targetMaterial.color = newColor;


        foreach (RawImage Image in images)
        {
            // Do something with the RawImage component, for example, change its color
            Image.color = newColor;
        }



        foreach (RawImage rawImage in images)
        {
            // Do something with the RawImage component, for example, change its color
            rawImage.color = newColor;
        }


        foreach (TMP_Text allText in text)
        {
            // Do something with the RawImage component, for example, change its color
           allText.color = newColor;
            allText.outlineColor = newColor;
        }






    }










    private void OnRedSliderValueChanged(float value)
    {
        Color currentColor = targetMaterial.color;
        targetMaterial.color = new Color(value, currentColor.g, currentColor.b);
    }

    private void OnGreenSliderValueChanged(float value)
    {
        Color currentColor = targetMaterial.color;
        targetMaterial.color = new Color(currentColor.r, value, currentColor.b);
    }

    private void OnBlueSliderValueChanged(float value)
    {
        Color currentColor = targetMaterial.color;
        targetMaterial.color = new Color(currentColor.r, currentColor.g, value);
    }
}
