using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLogic : MonoBehaviour {

    public static ObjectLogic instance;
    // Use this for initialization
    
	void Start () {
        instance = this;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetObjectStats(float att, float def, float cor, float hp, string name)
    {
       
        switch (name)
        {
            case "Apple":
                hp += 5;
                break;
            default:
                break;

        }

        PlayerLogic.instance.SetObjectsStats(att, def, cor, hp);
    }

    
    }
