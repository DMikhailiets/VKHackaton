using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosTransition : MonoBehaviour
{

	public RectTransform rt;
  	public float yToPos;
  	public float yFromPos;
  	public float speed;
  	public float delayTime;
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

    public void StartCor()
    {
    	StartCoroutine(Coroutine());
    }


   	public IEnumerator Coroutine()
   	{
   		MovePanneToTarget();
   		yield return new WaitForSeconds(delayTime);
   		MovePanneToBack();
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
