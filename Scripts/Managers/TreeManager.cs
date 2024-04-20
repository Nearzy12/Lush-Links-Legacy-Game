using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int object_health = 3;
    public string interaction_text = "Press mouse1 to hit tree";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to take away the health of the tree
    public void hitObject(int damage)
    {
        object_health = object_health - damage;
    }

    public int getObjectHealth() 
    { 
        return object_health; 
    }
}
