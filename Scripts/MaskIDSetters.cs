using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskIDSetters : MonoBehaviour
{

	public int maskID;
	public GameObject  maskManager; 
    // Start is called before the first frame update
    public void GetMaskID()
    {
        maskManager.GetComponent<MaskManagerScript>().UpdateMasks(maskID);
    }

   
}
