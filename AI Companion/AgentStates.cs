using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Patrol,
    Chase
}

public class AgentStates : MonoBehaviour
{
    public State currentState;
}
