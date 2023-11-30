using Assets.Scripts;
using System.Collections;
using UnityEngine;
/// <summary>
/// A CellContent is an abstraction of every object that can be placed in the grid.
/// </summary>
public abstract class CellContent
{
    public GameObject prefab { get; }
    public bool isPassThrough { get; } = false;
    public bool isSeeThrough { get; } = false;
    public string name { get; } = "UNDEFINED";
    public CellContent(string name, GameObject prefab, bool isPassThrough, bool isSeeThrough)
    {
        this.name = name;
        this.prefab = prefab;
        this.isPassThrough = isPassThrough;
        this.isSeeThrough = isSeeThrough;
    }

    /// <summary>
    /// The function that will be called when the given transform has been poked.
    /// </summary>
    public void OnSelect(Transform cellTransform, bool isFull)
    {
        //default behaviour !
        Cell pokedCell = cellTransform.gameObject.GetComponent<Cell>();
        //instantiates if the poked cell is empty or if the content of the cell doesn't collide and doesn't contain the same object we're trying to create.
        if (!isFull || (isFull && pokedCell.ContainsContent(name)))
        {
            Object.Instantiate(prefab, cellTransform);
            pokedCell.contents.Add(this);
        }
        else
        {
            Debug.Log("[CELL] : Can't place, cell is already full or already contains this element !");
        }
    }

    /// <summary>
    /// This method will be called when an adjacent agent interacts with this cell
    /// </summary>
    public abstract void OnAgentInteract(AgentBehaviour agent);

    /// <summary>
    /// This method will be called when an agent set this content as its target
    /// </summary>
    public abstract void OnAgentTarget(AgentBehaviour agent);
}