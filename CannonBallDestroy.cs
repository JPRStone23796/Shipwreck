using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallDestroy : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

 
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Sea" || other.gameObject.name == "Environment")
        {
         
            Destroy(this.gameObject);
            
        }

    


    }
}
