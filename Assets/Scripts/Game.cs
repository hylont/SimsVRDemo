using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public EHand dominantHand = EHand.RIGHT;
    private List<IGameListener> listeners;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO Load parameters on startup

    public void SetDominantHand(EHand newDominantHand)
    {
        dominantHand = newDominantHand;
        listeners.ForEach(delegate (IGameListener listener)
        {
            listener.OnDominantHandChange(dominantHand);
        });
    }

    /**
     * Adds a Listener and invokes all listener methods with current values 
     */
    public void AddListener(IGameListener newListener)
    {
        if (listeners == null) listeners = new List<IGameListener>();
        if (!listeners.Contains(newListener))
        {
            listeners.Add(newListener);
            listeners[listeners.Count - 1].OnDominantHandChange(dominantHand);
        }
        else Debug.LogWarning("[WARN] Tried to add an already existing GameListener !");
    }

    public void RemoveListener(IGameListener theListener)
    {
        if (!listeners.Remove(theListener)) Debug.LogWarning("[WARN] Tried to remove a GameListener that doesn't exist !");
    }

    //TODO Save parameters on change
}
