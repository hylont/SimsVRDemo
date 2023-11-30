using Assets.Scripts;
using System.Collections;
using UnityEngine;
public class Agent : CellContent
{
    public Agent(GameObject prefab) : base("AGENT", prefab, true, true)
    {

    }

    public override void OnAgentInteract(AgentBehaviour agent)
    {
        // Schizophrenia ?
    }

    public override void OnAgentTarget(AgentBehaviour agent)
    {
        // Schizophrenia ?
    }
}