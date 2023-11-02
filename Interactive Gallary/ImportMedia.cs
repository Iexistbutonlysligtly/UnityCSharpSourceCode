using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportMedia : MonoBehaviour
{


    public GameObject SpawnMediaImportText;


    public bool OnCollistion, OnClick;




    private void OnTriggerEnter(Collider other)
    {


        if ((other.gameObject.tag == "Player") && (OnCollistion))
        {
            SpawnMediaImportText.SetActive(true);
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (OnCollistion))
        {
            SpawnMediaImportText.SetActive(false);
        }
    }


    private void OnMouseDown()
    {
        if (OnClick)
        {
            if (SpawnMediaImportText.activeSelf)
            {

                SpawnMediaImportText.SetActive(false);

            }



            if (!(SpawnMediaImportText.activeSelf))
            {

                SpawnMediaImportText.SetActive(true);

            }
        }
    }


 





}
