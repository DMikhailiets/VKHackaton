using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRecordManager : MonoBehaviour
{

	public RectTransform rt;
	public GameObject buttons;
	public GameObject startRecordManager;
  	public float yToPos;
  	public float yFromPos;
  	public float speed;
	float timeCount;
	Vector2 poTo = new Vector2(0f,0f);
	Vector2 poFrom = new Vector2(0f,0f);
	public bool ok;

	

    // Start is called before the first frame update
    void Start()
    {
        MovePanneToBack();  
    }

    // Update is called once per frame
    public void StopRecord()
    {
         MovePanneToTarget();

    }

    public void Reset()
    {
    	buttons.active = true;
    	MovePanneToBack();  
    	startRecordManager.GetComponent<StartRecordingManager>().Start();
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
		poTo = new Vector2(rt.localPosition.x,yToPos);

		timeCreate();
	}

		public void MovePanneToBack()
	{
		poTo = new Vector2(rt.localPosition.x,yFromPos);
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
