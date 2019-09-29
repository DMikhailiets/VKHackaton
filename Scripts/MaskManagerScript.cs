using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManagerScript : MonoBehaviour
{

	public int id;
	public GameObject[] masks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateMasks(int _id)
    {
    	id =_id;
        for(int i = 0;i<masks.Length;i++)
        {
        	masks[i].active = false;
        }
       // if(id<masks.Length&&id>-1)
        masks[_id].active = true;
    }
}
