using System;
using UnityEngine.UI;
using UnityEngine;
using Ink.Runtime;
public class StoryManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public static event Action OnNext;

    [SerializeField]TextAsset inkJSONAsset = null;
    [SerializeField]GameObject DialoguePanel,ChoicesPanel;
    [SerializeField] Text textUI,nameUI;
    [SerializeField] Image imageUI;
	[SerializeField] Button buttonPrefab = null;

	public Story story;

    void Awake()
    {
        StartStory();
        DialoguePanel.SetActive(false);
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
		// Remove all the UI on screen
        Clear();
        OnNext=null;
		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			string text = story.Continue ();
			// This removes any white space from the text.
			text = text.Trim();
            textUI.text+=text;
			// Display the text on screen!
		}

		// Display all the choices, if there are any!
        if(story.currentChoices.Count==1 && (story.currentChoices[0].text=="next"))
        {
             Choice choice = story.currentChoices [0];
				// Tell the button what to do when we press it
				OnNext+= (delegate {
					OnClickChoiceButton (choice);
				});
        }
		else if(story.currentChoices.Count >0) {
            
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
        
		// If we've read all the content and there's no choices, the story is finished!
		else {
            	OnNext+= (delegate {
				DialoguePanel.SetActive(false);
                OnNext=null;
				});
		}
	}
    Button CreateChoiceView (string text) 
    {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (ChoicesPanel.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		// HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		// layoutGroup.childForceExpandHeight = false;

		return choice;
	}
    void OnClickChoiceButton (Choice choice) 
    {
		story.ChooseChoiceIndex (choice.index);
		RefreshView();
	}
    void Update()
    {
        if(!Input.anyKey)
        return;

        if(OnNext!=null)
        {
            if(Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButton(0))
                OnNext();
        }
    }

}
