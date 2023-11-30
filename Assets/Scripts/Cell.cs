using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A Cell represents an entry of the constructible grid. It stores and handles its content and its events.
/// </summary>
public class Cell : MonoBehaviour
{
    public List<CellContent> contents;
    public void Start()
    {
        contents = new List<CellContent>();
    }

    public bool IsFull()
    {
        foreach (CellContent content in contents)
        {
            if (!content.isPassThrough) return true;
        }
        return false;
    }

    public bool ContainsContent(string contentName)
    {
        foreach (CellContent content in contents)
        {
            if (content.name == contentName) return true;
        }
        return false;
    }

    public void OnSelectEntered()
    {
        // Terrifying instruction that basically tells the object in hand to invoke it's OnSelect function.
        CellContent handContent = GetComponentInParent<Grid>().dominantHand.GetComponent<Hand>().GetInHandContent();
        if (handContent != null) handContent.OnSelect(transform, IsFull());
    }
}