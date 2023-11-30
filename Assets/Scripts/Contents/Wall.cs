using Assets.Scripts;
using System.Collections;
using UnityEngine;
public class Wall : CellContent
{
    public Wall(GameObject prefab) : base("WALL", prefab, false, false)
    {

    }
    public override void OnAgentInteract(AgentBehaviour agent)
    {
        // nothing either
    }

    public override void OnAgentTarget(AgentBehaviour agent)
    {

    }
}