using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]
    UnityEvent Triggered;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((Triggered.GetPersistentEventCount() > 0))
        {
            Triggered.Invoke();
        }
        else
        {
            Debug.LogWarning(this + " is not triggering any events.");
        }
    }

}
