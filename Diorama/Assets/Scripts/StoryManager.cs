using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Playables;

using Ink.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class StoryManager : MonoBehaviour
{
    [SerializeField] Scenario[] scenarios;
    public static event Action<Story> OnCreateStory;
    public static event Action OnNext;
	public Story story;
    string currentSpeaker="hello";
    [SerializeField]TextAsset inkJSONAsset = null;

    [Space(10)]
    [SerializeField]GameObject DialoguePanel;
    [SerializeField]GameObject ChoicesPanel;

    [Space(10)]
    [SerializeField] Text textUI;
    [SerializeField] Text nameUI;
    [SerializeField] Image imageUI;
    
    [Space(10)]    
	[SerializeField] Button buttonPrefab = null;
    void Awake()
    {
        StartStory();
        DialoguePanel.SetActive(false);

        string path=Application.persistentDataPath+GameManager.Instance.saveName;
        FileStream stream=new FileStream(path,FileMode.Open,FileAccess.Read);
        PlayerData data=new PlayerData();
        BinaryFormatter formatter=new BinaryFormatter();

        data=formatter.Deserialize(stream) as PlayerData;
        Debug.Log(data.playercounter);
        story.variablesState["Scenario"]=data.playercounter;

        foreach(Scenario scenario in scenarios)
        {
            if(scenario.index==data.playercounter)  
            {
                scenario.Activate();
                break;
            } 
        }

    }
    void Update()
    {
        if(!Input.anyKey)
        return;
        if(Input.GetKey(KeyCode.Return))
            {
                GetComponent<PlayableDirector>().Play();
            }
        if(OnNext!=null)
        {
            if(Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButton(0))
                OnNext();
        }
    }
    public void RemoveCharacter()
    {
          foreach(Scenario scenario in scenarios)
        {
            if(scenario.character.name==currentSpeaker)  
            {
                scenario.character.SetActive(false);
                break;
            } 
        }
    }
    void StartStory () {
		story = new Story (inkJSONAsset.text);
        if(OnCreateStory != null) OnCreateStory(story);
	}
    void Clear()
    {
         textUI.text="";
        int childCount = ChoicesPanel.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (ChoicesPanel.transform.GetChild (i).gameObject);
		}
    }
    public void Begin()
    {
        story.ChoosePathString("Start");
        RefreshView();
        DialoguePanel.SetActive(true);
    }
    void RefreshView () {
        Clear();
        OnNext=null;
		while (story.canContinue) {
			string text = story.Continue ();
			text = text.Trim();
            textUI.text+=text;
		}

        if(story.currentChoices.Count==1 && (story.currentChoices[0].text=="next"))
        {
             Choice choice = story.currentChoices [0];
				OnNext+= (delegate {
					OnClickChoiceButton (choice);
				});
        }
		else if(story.currentChoices.Count >0) {
            
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
        
		else {
            	OnNext+= (delegate {
				DialoguePanel.SetActive(false);
                OnNext=null;
				});
		}
	}
    Button CreateChoiceView (string text) 
    {
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (ChoicesPanel.transform, false);
		
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;
	/* 	
    Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;
    */
		return choice;
	}
    void OnClickChoiceButton (Choice choice) 
    {
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}
}
[Serializable]
public class Scenario{
    public int index;
    public GameObject character;
    public void Activate()
    {
        character.SetActive(true);
    }
}