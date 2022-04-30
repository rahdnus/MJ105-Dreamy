INCLUDE Global.ink
VAR NEXT="next"
VAR END="end"

===Main===
=Arbiter
{
-currentSpeaker=="Gabriel":
 ->LittleGirl.Conv_1
}
->DONE

===LittleGirl===
=Conv_1
#Gabriel
-Hmm?Who are you lady?
I haven't seen you before.
Are you new?
+[{NEXT}]
-
#You
+[yes?]
 #Gabriel
 You don't sound too sure lady.
 ++[{NEXT}]
 --
 Hehehe.You are funny lady.
 ++[{NEXT}]
 --
 Well,lemme welcome you to our town anyway.
 Welcome to ---, my home!
 ++[{NEXT}]
 --
+[I don't know?]
 #Gabriel

+[...]
 #Gabriel
-
 #Gabriel
 By the way...
 Whats your name lady?  
+[{NEXT}]
 -
 +[I dont remember]
  #Gabriel
 Really?
 Hmmm, that must be difficult.
 +[...]
  #Gabriel
 You dont wanna tell me?
 ++[{NEXT}]
 --
 ++[No its not that]
  #Gabriel
 Okay..., 
 ++[...]
  Ah,
-I am sorry for asking too many questions.
 As an apology you can ask me questions too!
+[{NEXT}]
-Ask me anything.
+[{NEXT}]
-
-(questions)
        *[Who are you?]
         Me?
         I am Gabriel! 
         Nice to meet you.
         My dream is to be a ballet dancer one day!
         ++[{NEXT}]
        *[What are you doing here?]
          Me?
         I am Gabriel! 
         Nice to meet you.
         My dream is to be a ballet dancer one day!
        ++[{NEXT}]
        *[What is this place?]
          Me?
         I am Gabriel! 
         Nice to meet you.
         My dream is to be a ballet dancer one day!
        ++[{NEXT}]
        *[No not really]
        aw
        ->post
- -> questions
-(post)
+[{END}]
~playCutscene("curtains")
->DONE

===Artist===
->DONE

===Start===
- {Scenario}:I looked at Monsieur Fogg 
        +[next]
        -
        +   ... and I could contain myself no longer.
            'What is the purpose of our journey, Monsieur?'
            'A wager,' he replied.
            ++[next]
            --
            ++     'A wager!'[] I returned.
                    He nodded. 
                    +++  'But surely that is foolishness!'
                    +++  'A most serious matter then!'
                    - - -   He nodded again.
                    +++   'But can we win?'
                            'That is what we will endeavour to find out,' he answered.
                    +++   'A modest wager, I trust?'
                            'Twenty thousand pounds,' he replied, quite flatly.
                    +++ (final)  I asked nothing further of him then[.], and after a final, polite cough, he offered nothing more to me. <>
            ++     'Ah[.'],' I replied, uncertain what I thought.
            - -     After that, <>
        +   ... but I said nothing[] and <>
        - we passed the day in silence.
        +[next]     
        -~playCutscene("curtains")
-> END