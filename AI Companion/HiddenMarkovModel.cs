    using UnityEngine;

    public class HiddenMarkovModel : MonoBehaviour
    {

        // Define the observation space (the possible observed states)
        public string[] observationSpace = { "Not Observing", "Ball", "Not Observing" };

        // Define the hidden states (the states of the finite state machine)
        public string[] hiddenStates = { "Normal", "Playful", "Distressed" };

        // Define the initial state probabilities
        public float[] initialStateProbabilities = { 0.4f, 0.3f, 0.3f };

        // Define the state transition probabilities
        public float[,] stateTransitionProbabilities = {
            { 0.2f, 0.8f, 0.0f },
            { 0.3f, 0.7f, 0.0f },
            { 0.5f, 0.5f, 0.0f }
        };

        // Define the observation probabilities
        public float[,] observationProbabilities = {
            { 0.6f, 0.3f, 0.1f },
            { 0.1f, 0.6f, 0.3f },
            { 0.3f, 0.1f, 0.6f }
        };

        private void Start()
        {
            // Perform the HMM logic at the start
            PerformHMM();
        }

        private void PerformHMM()
        {
            // Generate a random initial state based on the initial probabilities
            string initialState = GenerateRandomState(initialStateProbabilities);

            // Log the initial state
            Debug.Log("Initial state: " + initialState);

            // Observe the current state
            string observedState = ObserveState(initialState);

            // Log the observed state
            Debug.Log("Observed state: " + observedState);



            // Repeat the process for a number of iterations
            for (int i = 0; i < 5; i++)
            {
                // Calculate the next state based on the current state and transition probabilities
                string nextState = CalculateNextState(initialState, stateTransitionProbabilities);

                // Log the next state
                Debug.Log("Next state: " + nextState);

                // Observe the next state
                observedState = ObserveState(nextState);

                // Log the observed state
                Debug.Log("Observed state: " + observedState);

          

                // Update the current state
                initialState = nextState;
            }
        }

        public string GenerateRandomState(float[] probabilities)
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
                    return hiddenStates[i];
                }
            }

            // Return the last state if no state is selected (should not happen)
            return hiddenStates[hiddenStates.Length - 1];
        }

        public string CalculateNextState(string currentState, float[,] transitionProbabilities)
        {
            // Find the index of the current state
            int currentStateIndex = System.Array.IndexOf(hiddenStates, currentState);

            // Generate a random value between 0 and 1
            float randomValue = Random.value;

            // Iterate through the transition probabilities for the current state to find the next state
            float cumulativeProbability = 0f;
            for (int i = 0; i < hiddenStates.Length; i++)
            {
                cumulativeProbability += transitionProbabilities[currentStateIndex, i];

                if (randomValue <= cumulativeProbability)
                {
                    return hiddenStates[i];
                }
            }

            // Return the last state if no state is selected (should not happen)
            return hiddenStates[hiddenStates.Length - 1];
        }

        public string ObserveState(string currentState)
        {
            // Find the index of the current state
            int currentStateIndex = System.Array.IndexOf(hiddenStates, currentState);

            // Generate a random value between 0 and 1
            float randomValue = Random.value;

            // Iterate through the observation probabilities for the current state to find the observed state
            float cumulativeProbability = 0f;
            for (int i = 0; i < observationSpace.Length; i++)
            {
                cumulativeProbability += observationProbabilities[currentStateIndex, i];

                if (randomValue <= cumulativeProbability)
                {
                    return observationSpace[i];
                }
            }

            // Return the last observed state if no state is selected (should not happen)
            return observationSpace[observationSpace.Length - 1];
        }






    public void IncreaseObservedStateProbability(string observedState, float increaseAmount)
    {
        // Find the index of the observed state
        int observedStateIndex = System.Array.IndexOf(observationSpace, observedState);

        // Increase the probability of the observed state by the specified amount
        for (int i = 0; i < hiddenStates.Length; i++)
        {
            observationProbabilities[i, observedStateIndex] += increaseAmount;
        }
    }












    public float GetObservedStateProbability(string observedState)
    {
        // Find the index of the observed state
        int observedStateIndex = System.Array.IndexOf(observationSpace, observedState);

        // Get the probability for the observed state in each hidden state
        float[] probabilities = new float[hiddenStates.Length];
        for (int i = 0; i < hiddenStates.Length; i++)
        {
            probabilities[i] = observationProbabilities[i, observedStateIndex];
        }

        // Calculate the average probability
        float averageProbability = 0f;
        if (probabilities.Length > 0)
        {
            averageProbability = Mathf.Max(0f, Mathf.Min(1f, Mathf.Max(probabilities)));
        }

        return averageProbability;
    }












}
