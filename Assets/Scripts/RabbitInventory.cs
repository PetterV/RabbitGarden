using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitInventory : MonoBehaviour
{
	public int food = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
    	if (col.gameObject.tag == "Food"){
    		food += 1;
    		Destroy(col.gameObject);
    	}
    }
}
