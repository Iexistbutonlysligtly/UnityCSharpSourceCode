using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class AnimationTrigger : MonoBehaviour, IPointerDownHandler
{
    [Header("Condition")]
    public bool OnClick;
    public bool OnHover;
    public bool OnTriggerE;

    [Header("Elements")]
    public float AnimationDelay;
    public Animator OjectWitthAnimation;

    public string[] AnimationTriggerName;
    public string Tag;


    bool InCollider;


    public AudioClip[] SoundToTrigger;
    public AudioSource Source;





    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnClick)
        {

            StartCoroutine(TriggerDelay());

        }
    }



    private void OnMouseDown()
    {
        if (OnClick)
        {

            StartCoroutine(TriggerDelay());

        }
       


    }
    void OnTriggerEnter(Collider other)
    {
        InCollider = true;

        if (OnTriggerE)
            if (other.tag == Tag)
        {
            StartCoroutine(TriggerDelay());
        }



    }


    void OnTriggerExit(Collider other)
    {

        InCollider = false;

        if (OnTriggerE)

            if (other.tag == Tag)
        {
            StartCoroutine(TriggerDelay());
        }
    }





    public void Trigger()


    {

        OjectWitthAnimation.SetTrigger(AnimationTriggerName[0]);

    }

    public void TriggerForEnter()


    {

        OjectWitthAnimation.SetTrigger(AnimationTriggerName[0]);
        Source.clip = SoundToTrigger[0];
        Source.Play();

    }

    public void TriggerForExit()


    {

        OjectWitthAnimation.SetTrigger(AnimationTriggerName[1]);
        Source.clip = SoundToTrigger[1];
        Source.Play();

    }


    IEnumerator TriggerDelay()  //  <-  its a standalone method
    {

        yield return new WaitForSeconds(AnimationDelay);

     if (!OnTriggerE)
            Trigger();

       else if (OnTriggerE && (InCollider))
            TriggerForEnter();

        else if (OnTriggerE && (!InCollider))
            TriggerForExit();
    }
}
