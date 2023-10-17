using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TrainMovement.instance.hasArrived)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(SceneToLoad);
            }
        }
    }
}
