using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Ink.Runtime;
using Cinemachine;

public class StoryManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public static event Action OnNext;
	public Story story;
    [SerializeField]GameObject playerPrefab;
    [SerializeField]CinemachineVirtualCamera virtualCamera;
    [SerializeField]Transform spawnPoint;
    [SerializeField] Scenario[] scenarios;
    AudioSource audioSource;
    PlayableDirector director;
    string currentSpeaker="hello";
    [Space(20)]
    [SerializeField]Door[] doors;
    [SerializeField]PlayableAsset[] playableassets;
    [SerializeField]AudioClip[] audioClips;
    [Space(20)]
    Dictionary<string,AudioClip> audioDictionary=new Dictionary<string,AudioClip>();
    Dictionary<string,PlayableAsset> playableassetDictionary=new Dictionary<string, PlayableAsset>();
    [SerializeField]TextAsset inkJSONAsset = null;

    [Space(10)]
    [SerializeField]GameObject curtains;
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
        foreach(Door door in doors)
        {
            door.OnLoad+=TriggerCurtains;
        }
        foreach(AudioClip clip in audioClips)
        {
            audioDictionary.Add(clip.name,clip);
        }
         foreach(PlayableAsset asset in playableassets)
        {
            playableassetDictionary.Add(asset.name,asset);
        }
        
        director=GetComponent<PlayableDirector>();
        audioSource=GetComponent<AudioSource>();

        story.BindExternalFunction("playCutscene",(string Name) => {
            
            if(!playableassetDictionary.ContainsKey(Name))
                return;

            director.playableAsset=playableassetDictionary[Name];
            director.Play();
        });
        story.BindExternalFunction("playAudio",(string Name) => {
            
            if(!audioDictionary.ContainsKey(Name))
                return;

            audioSource.clip=audioDictionary[Name];
            audioSource.Play();
            
        });
    }
    void TriggerCurtains()
    {
        curtains.GetComponent<Animator>().Play("in");
    }
    void Start()
    {
        virtualCamera.Follow=Instantiate(playerPrefab,spawnPoint.position,Quaternion.identity).transform;
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
            if(Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButtonDown(0))
                OnNext();
        }
    }
    public void RemoveCharacter()
    {
          foreach(Scenario scenario in scenarios)
        {
            if(scenario.character.name==currentSpeaker)  
            {
                Destroy(scenario.character);
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
    public void Begin(string Name)
    {
        currentSpeaker=Name;
        story.variablesState["currentSpeaker"]=currentSpeaker;
        story.ChoosePathString("Main.Arbiter");
        RefreshView();
        DialoguePanel.SetActive(true);
    }
    void RefreshView () 
    {
        Clear();
        OnNext=null;
		while (story.canContinue) {
			string text = story.Continue();
			text = text.Trim();
            textUI.text+=text;
            Debug.Log(text);

		}
        if(story.currentChoices.Count==1 && ((story.currentChoices[0].text=="next")||(story.currentChoices[0].text=="end")))
        {
             Choice choice = story.currentChoices [0];
				OnNext+= (delegate {
					OnClickChoiceButton (choice);
				});
                if((story.currentChoices[0].text=="end"))
                {
                    OnNext+= (delegate {
				    DialoguePanel.SetActive(false);
                    FindObjectOfType<Player>().inConversation=false;
				    });
                }
        }
		else 
        if(story.currentChoices.Count >0) {
            
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
        else
		{
            	
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