using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosScript : MonoBehaviour
{
  	public RectTransform rt;
  	public float xToPos;
  	public float xFromPos;
  	public float speed;
	float timeCount;
	Vector2 poTo = new Vector2(0f,0f);
	Vector2 poFrom = new Vector2(0f,0f);
	public bool ok;


    // Start is called before the first frame update
    void Start()
    {
      MovePanneToBack();  
    	ok = false;
    }


    public void ReCheck()
    {
    	ok = !ok;
    	if(ok)
    	{
    		 MovePanneToTarget();
    	}
    	else{
    		MovePanneToBack();
    	}
    }

    public void MovePanneToTarget()
	{		
		poTo = new Vector2(xToPos,rt.localPosition.y);

		timeCreate();
	}

		public void MovePanneToBack()
	{
		poTo = new Vector2(xFromPos,rt.localPosition.y);
		timeCreate();
	}

	void timeCreate()
	{
		timeCount = 0.0f;
	}


    // Update is called once per frame
    void Update()
    {
    	timeCount = timeCount + Time.deltaTime;
        rt.localPosition = Vector2.Lerp(rt.localPosition, poTo, timeCount*speed);
    }
}
