using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
	public string URL;
    // Start is called before the first frame update
    public void OpenURLAdress()
    {
         Application.OpenURL(URL);
    }
}
