using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class IamgeToMaterialWithTextInput : MonoBehaviour
{


    string PG;
    
    //public string ImagePath;
    public string ImageName;

    public string url;
    public Renderer thisRenderer;

    public TMP_InputField InportText, InportImageName;

    // Start is called before the first frame update
    void Start()
    {

        //url = "file://" + ImagePath;

        //StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently

        //// the following will be called even before the load finished
        //thisRenderer.material.color = Color.red;



        InportText.onEndEdit.AddListener(SubmitName);
        InportImageName.onEndEdit.AddListener(SubmitImageName);


    }


    private void SubmitName(string arg0)
    {


       PG  = arg0;

        url = "file://" + PG + '\\';



    }



    private void SubmitImageName(string arg0)
    {

        ImageName = arg0;

        url = "file://" + PG + '\\' + ImageName;

        StartCoroutine(LoadFromLikeCoroutine());
    }





    // this section will be run independently
    private IEnumerator LoadFromLikeCoroutine()
    {
        Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
        yield return wwwLoader;         // start loading whatever in that url ( delay happens here )

        Debug.Log("Loaded");
        thisRenderer.material.color = Color.white;              // set white
        thisRenderer.material.mainTexture = wwwLoader.texture;  // set loaded image
    }
}
