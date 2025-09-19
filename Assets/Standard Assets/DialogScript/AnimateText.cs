using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// using UnityStandardAssets.CrossPlatformInput;

public class AnimateText : MonoBehaviour {
	public EventSetting source;
	public Text textUI;
	private float wait = 0.0f;
	public float delay = 0.03f;
	private bool begin = false;
	private bool ff = false;
	private bool autoText = true;
//	public bool autoSkip = false;
	public string wordComplete = "";
	private string showText = "";
	private int i = 0;

	// Use this for initialization
	void Start () {
	//	bool autoSkip = GetComponent<autoSkip>();
		if(!textUI && GetComponent<Text>()){
			textUI = GetComponent<Text>();
		}
		begin = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!textUI){
			return;
		}
	//	EventActivator other = GetComponent<EventActivator>();
	//	if(other != null && other.autoSkip){
		if(source.autoSkip){
			if(Input.GetKeyDown("e") || Input.GetButtonDown("Interact")){
				DoneText();
			//	Biga();
			}
		}
		if(begin){
			if(wait >= delay){
				if(wordComplete.Length > 0){
					showText += wordComplete[i++];
				}
				wait = 0;
				if(i >= wordComplete.Length){
					//Done Animate
					if(autoText && begin){
						Baga();
					}
					begin = false;
				}else if(i >= wordComplete.Length){
					i = 0;
				}
			}else{
				//wait += Time.deltaTime;
				wait += Time.unscaledDeltaTime;
			}
			textUI.text = showText;
		}
	}

	void Baga(){
		StartCoroutine(DelayNextPage());
	}
	
	
	
	IEnumerator DelayNextPage(){
		Time.timeScale = 1.0f;
		if(source.ff){
				yield return new WaitForSeconds(0.3f);
			}
		else {
			yield return new WaitForSeconds(2.5f);
		}
	//	yield return new WaitForSeconds(1.0f);
	//	yield return new WaitForSeconds(2.5f);
		begin = false;
		DoneText();
		//NextPage();
	}
	
	void Biga(){
		StartCoroutine(NewPage());
	}
	
	IEnumerator NewPage(){
		Time.timeScale = 1.0f;
		yield return new WaitForSeconds(2.0f);
		if(source){
				source.FinishDialogue();
		}
	
	}

	public void StartAnimate(string strComplete){
		if(!textUI && GetComponent<Text>()){
			textUI = GetComponent<Text>();
		}
		begin = false;
		i = 0;
		showText = "";
		wordComplete = strComplete;
		begin = true;
	}

	void DoneText(){
		if(begin){
			showText = wordComplete;
			textUI.text = showText;
			begin = false;
			
			return;
		}
		if(source){
			source.FinishDialogue();
		}
		//GetComponent<AnimateText>().enabled = false;
	}
}
