using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScreenOpen : MonoBehaviour
{

    public float TransitionDelay;
    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TranAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transition()
    {

        transition.SetTrigger("Transition");

    }


    IEnumerator TranAnimation()  //  <-  its a standalone method
    {

        yield return new WaitForSeconds(TransitionDelay);
        Transition();
    }
}
