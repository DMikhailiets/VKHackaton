using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartRecordingManager : MonoBehaviour
{

	public GameObject buttons;
	public GameObject transition;
	public GameObject recorder;
	public Image sportsRUImage;
	public float delay;
	public float alphaChangeSpeed;

	float time = 0;

	bool startAlpha;

    // Start is called before the first frame update
    public void Start()
    {	
    	sportsRUImage.GetComponent<Image>().color = new Color(255,255,255,255);
    	//sportsRUImage.color.a = 255; 
    	startAlpha = false;
    	buttons.active = true;
        //delay = transition.GetComponent<ChangePosTransition>().delayTime;
    }

    public void StartOfRecordingProcesses()
    {
    	time = 0;
    	startAlpha = true;
    	StartCoroutine(DelayTimer());
    	transition.GetComponent<ChangePosTransition>().StartCor();
    }


    // Update is called once per frame
    void StartRecording()
    {
 
    	recorder.GetComponent<UIController>().OnClickStartRecord();
        buttons.active = false;
    }

    IEnumerator DelayTimer()
    {
    	yield return new WaitForSeconds(delay);
    	StartRecording();
    }

    void Update()
    {
    	time += Time.time;

    	if(transition.transform.localPosition.y < 100){
    	Color col = new Color(0,0,0,0);
    	sportsRUImage.GetComponent<Image>().color = Color.Lerp (sportsRUImage.GetComponent<Image>().color, col, alphaChangeSpeed*time);
    }
    }
}
