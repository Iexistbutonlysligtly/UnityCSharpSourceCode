using System.Collections;
using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    public Material highlightMaterial;
    private Material originalMaterial;
    private Renderer objectRenderer;
    private bool isHighlighted;

    Companion Companion;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;

        Companion = FindObjectOfType<Companion>();
    }



    private void Update()
    {


        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.gameObject == gameObject)
        //        {
        //            // This object was clicked
        //            // Implement your click handling logic here

        //            Debug.Log("Done");

        //        }
        //    }
        //}



    }


    private void OnMouseDown()  
    {
        if (isHighlighted)
        {

            Companion.currentState = "Normal";
            objectRenderer.material = originalMaterial;
            isHighlighted = false;
            Companion.SF = true;
          
          



            // Companion.toMe = true;
        }
        else
        {

           // Companion.currentState = "StateB";

            objectRenderer.material = highlightMaterial;
            isHighlighted = true;

            //  Companion.toMe = false;

            Companion.SF = false;
            Companion.agent.ResetPath();
            //  Companion.Inspect(gameObject.transform);
            StartCoroutine(DelayedInspect());
        }
    }


    void Click()
    {







        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform the raycast and ignore all colliders except the one on the GameObject
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Click")))
            {
                // Check if the hit collider belongs to this GameObject
                if (hit.collider.gameObject == gameObject)
                {
                    // Object clicked, do something
                    Debug.Log("Object clicked: " + hit.collider.name);
                }
            }
        }



    }




    private IEnumerator DelayedInspect()
    {
        yield return new WaitForSeconds(0.1f); // Adjust the delay time as needed

        Companion.Inspect(gameObject.transform);
    }



}
