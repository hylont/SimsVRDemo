using Assets.Scripts;
using System.Collections;
using UnityEngine;
public class Floor : CellContent
{
    public Floor(GameObject prefab) : base("FLOOR", prefab, true, true)
    {

    }

    public override void OnAgentInteract(AgentBehaviour agent)
    {
        // Agents shouldn't interact with the ground, I think 
    }

    public override void OnAgentTarget(AgentBehaviour agent)
    {

    }
}