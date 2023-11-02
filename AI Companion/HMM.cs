using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMM : MonoBehaviour
{
    private AgentStates agentStates;
    private Dictionary<State, float> emissionProbabilities;
    private Dictionary<State, Dictionary<State, float>> transitionProbabilities;

    private void Awake()
    {
        agentStates = GetComponent<AgentStates>();

        // Initialize emission probabilities
        emissionProbabilities = new Dictionary<State, float>();
        emissionProbabilities[State.Idle] = 0.7f;
        emissionProbabilities[State.Patrol] = 0.2f;
        emissionProbabilities[State.Chase] = 0.1f;

        // Initialize transition probabilities
        transitionProbabilities = new Dictionary<State, Dictionary<State, float>>();
        transitionProbabilities[State.Idle] = new Dictionary<State, float>();
        transitionProbabilities[State.Patrol] = new Dictionary<State, float>();
        transitionProbabilities[State.Chase] = new Dictionary<State, float>();

        transitionProbabilities[State.Idle][State.Idle] = 0.7f;
        transitionProbabilities[State.Idle][State.Patrol] = 0.2f;
        transitionProbabilities[State.Idle][State.Chase] = 0.1f;

        transitionProbabilities[State.Patrol][State.Idle] = 0.3f;
        transitionProbabilities[State.Patrol][State.Patrol] = 0.6f;
        transitionProbabilities[State.Patrol][State.Chase] = 0.1f;

        transitionProbabilities[State.Chase][State.Idle] = 0.1f;
        transitionProbabilities[State.Chase][State.Patrol] = 0.2f;
        transitionProbabilities[State.Chase][State.Chase] = 0.7f;
    }

    public State GetNextState()
    {
        State currentState = agentStates.currentState;

        // Calculate the next state based on emission and transition probabilities
        float emissionProbability = emissionProbabilities[currentState];
        Dictionary<State, float> transitions = transitionProbabilities[currentState];

        float totalProbability = 0f;
        foreach (var transition in transitions)
        {
            totalProbability += transition.Value;
        }

        float randomValue = Random.value;

        foreach (var transition in transitions)
        {
            float probability = transition.Value / totalProbability;
            if (randomValue < probability)
            {
                return transition.Key;
            }

            randomValue -= probability;
        }

        return currentState;
    }
}
