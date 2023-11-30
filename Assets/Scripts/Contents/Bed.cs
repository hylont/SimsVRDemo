using Assets.Scripts;
using System.Collections;
using UnityEngine;
public class Bed : CellContent
{
    private Transform posBeforeSleep;
    public Bed(GameObject prefab) : base("BED", prefab, false, true)
    {

    }

    public override void OnAgentInteract(AgentBehaviour agent)
    {
        agent.Sleep();
    }

    public override void OnAgentTarget(AgentBehaviour agent)
    {

    }
}