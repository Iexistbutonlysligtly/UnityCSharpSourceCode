using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Companion : MonoBehaviour
{

    public Animator animator;
    public Animation agentAnimation;

    public Transform Player;
    public NavMeshAgent agent;

   public  bool HasReached = false;
   

    public float speed;
    public float DisplayV;
    public float stoppingRadius = 1.0f;
    public float acceptanceRadius = 5.0f;

  public   float FolowLevel;

    public float RD;
    public float SR;

    bool canMove = true;

    public bool toMe = true;





    public bool isFollowing = true;
    public bool SF = true;

    [SerializeField] private float stopDistance = 2.0f; // Distance to stop following the player
    [SerializeField] private float resumeDistance = 5.0f; // Distance to resume following the player


    public string currentState;
    public string currentOberveState;
    public HiddenMarkovModel hmm;
    public float probability;


    public AudioSource passiveSounds;
    bool isAudioPlaying = false;





    bool flipFlop = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentAnimation = GetComponent<Animation>();

        currentState = hmm.GenerateRandomState(hmm.initialStateProbabilities);

      
        passiveSounds = gameObject.GetComponent<AudioSource>();
     


    }

    private void Update()
    {

        UpdateProbabilityDisplay();



        RD = agent.remainingDistance;
        SR = agent.stoppingDistance;

 




        //if (Input.GetKeyDown(KeyCode.S))
        //{

        //    Sit();

        //}


        Sounds();







        //if (canMove && !animator.GetCurrentAnimatorStateInfo(0).IsName("SitUp") && toMe)
        if (isFollowing && SF)
        {
            FollowPlayer();
        }
        else
        {
            CheckResumeFollowing();
        }

        //InvokeRepeating("IdleActions1", 3f, 6f);

        Movment();

        




    }

    void IdleActions1()
    {

        bool horizontalPressed;
        bool verticalPressed;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        horizontalPressed = horizontalInput != 0f;
        verticalPressed = verticalInput != 0f;

        if ((DisplayV >= 0) && !(DisplayV >= 1))
        {

            if (!horizontalPressed && !verticalPressed)
                animator.SetTrigger("Look");



        }
        
    }





    #region HMM Stuff



    private void TransitionToState(string newState)
    {
        // Perform any necessary actions when transitioning from currentState to newState

        // Get the index of the current state and the new state in the hiddenStates array
        int currentStateIndex = System.Array.IndexOf(hmm.hiddenStates, currentState);
        int newStateIndex = System.Array.IndexOf(hmm.hiddenStates, newState);

        // Get the color probabilities for the current state and the new state from the observationProbabilities matrix
        float[] currentColorProbabilities = new float[hmm.observationSpace.Length];
        float[] newColorProbabilities = new float[hmm.observationSpace.Length];
        for (int i = 0; i < hmm.observationSpace.Length; i++)
        {
            currentColorProbabilities[i] = hmm.observationProbabilities[currentStateIndex, i];
            newColorProbabilities[i] = hmm.observationProbabilities[newStateIndex, i];
        }

        // Generate a random observed state based on the new state's color probabilities
        string observedState = GenerateRandomState(newColorProbabilities);

        // Log the observed state
        Debug.Log("Observed state: " + observedState);

        // Set an action based on the observed state



        currentOberveState = observedState;

        // Update the currentState
        currentState = newState;
    }

    private string GenerateRandomState(float[] probabilities)
    {
        // Generate a random value between 0 and 1
        float randomValue = Random.value;

        // Iterate through the probabilities to find the corresponding state
        float cumulativeProbability = 0f;
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];

            if (randomValue <= cumulativeProbability)
            {
                return hmm.observationSpace[i];
            }
        }

        // Return the last state if no state is selected (should not happen)
        return hmm.observationSpace[hmm.observationSpace.Length - 1];
    }





    #endregion



    #region Actions




    void Movment()
    {
        agent.speed = speed;

        DisplayV = agent.velocity.magnitude;


      


        if (agent.velocity.magnitude >= 1)
        {
            DisplayV = 1;

        }

        //  animator.SetFloat("CompanionMovment", DisplayV);







            if (currentState == "Distressed")
            {

                flipFlop = !flipFlop;

                animator.SetBool("Aggressive", true);

           // passiveSounds.clip = Resources.Load<AudioClip>("FoxCry");
          


        }

        else
        {
            animator.SetBool("Aggressive", false);


        }
           
















        if (animator.GetBool("Aggressive") == false)
        {


            animator.SetFloat("CompanionMovment", Mathf.Lerp(animator.GetFloat("CompanionMovment"), DisplayV, Time.deltaTime * 10f));

        }



        if (animator.GetBool("Aggressive")){


            animator.SetFloat("CompanionAggressive", Mathf.Lerp(animator.GetFloat("CompanionAggressive"), DisplayV, Time.deltaTime * 10f));

        }




    }

    private void FollowPlayer()

    {




        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);


        // If the companion is not within the stop distance, continue following the player
        if (distanceToPlayer > stopDistance)
        {
            agent.SetDestination(Player.position);
        }
        else
        {
            // Stop following the player when within the stop distance
            agent.isStopped = true;
            isFollowing = false;



            Debug.Log("Staying on Following");


        }
    }




    void Sounds()
    {



        if (currentState == "Distressed")
        {
           // passiveSounds.Stop();

            if (!passiveSounds.isPlaying)
            {
                passiveSounds.loop = true;
                passiveSounds.clip = Resources.Load<AudioClip>("FoxCry");
                passiveSounds.Play();

            }
                


        }


        if (passiveSounds.clip != null)
        {

            if (passiveSounds.isPlaying && passiveSounds.clip.name == "FoxCry")
                passiveSounds.Play();

        }

        


        //if (DisplayV >= 1)
        //{
        //    if (!passiveSounds.isPlaying)
        //    {
        //        passiveSounds.loop = false;
        //        passiveSounds.clip = Resources.Load<AudioClip>("FoxMSteps");
        //        passiveSounds.Play();

        //    }

        //}





    }


    public void Inspect(Transform toInspect)
    {


        TransitionToState(hmm.CalculateNextState(currentState, hmm.stateTransitionProbabilities));


        if ( currentState == "Playful" && currentOberveState == "Ball")
        {


            SF = false;
            Debug.Log("Inspect");

            float stopDistanceForObject = 0;

            float distanceToPlayer = Vector3.Distance(transform.position, toInspect.position);

            // If the companion is not within the stop distance, continue following the object to inspect
            if (distanceToPlayer > stopDistanceForObject)
            {
                agent.SetDestination(toInspect.position);
            }
            else
            {
                // Stop following the object when within the stop distance
                agent.isStopped = true;
                isFollowing = false;
            }







            // Call CheckResumeFollowing to resume following the player once the inspection is completed
            Invoke("CheckResumeFollowing", 2f);



        }




    }








    private void CheckResumeFollowing()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        // If the companion is at or beyond the resume distance, resume following the player
        if (distanceToPlayer >= resumeDistance)
        {
            agent.isStopped = false;
            isFollowing = true;
        }
    }


    public void Sit()
    {
        if (SF)
        {
            animator.SetTrigger("Sit");
            SF = false;

        }

        else
        {

            animator.SetTrigger("SitUp");
            SF = true;

        }










    }





    #endregion





    private void UpdateProbabilityDisplay()
    {
        // Get the probability of "ObservedState2"
         probability = hmm.GetObservedStateProbability("Ball");

        // Update the UI text or any other visual representation
       
    }


}




