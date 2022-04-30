INCLUDE Global.ink

===Main===
=Aribter
->DONE

===LittleGirl===
=Conv_1
#Gabriel
-Hmm?Who are you lady?
I haven't seen you before.
Are you new?
+[next]
-
I haven't seen you before.
Are you new?
+[next]
-
I haven't seen you before.
Are you new?
+[next]
-
I haven't seen you before.
Are you new?
+[next]
-
I haven't seen you before.
Are you new?
-
#You
+[yes?]
 #Gabriel
 You don't sound too sure lady.
 ++[next]
 --
 Hehehe.You are funny lady.
 ++[next]
 --
 Well,lemme welcome you to our town anyway.
 Welcome to ---, my home!
 ++[next]
 --
+[I don't know?]
 #Gabriel

+[...]
 #Gabriel
-
 #Gabriel
 By the way...
 Whats your name lady?  
 +[next]
 -
 +[I dont remember]
  #Gabriel
 Really?
 Hmmm, that must be difficult.
 +[...]
  #Gabriel
 You dont wanna tell me?
 ++[next]
 --
 ++[No its not that]
  #Gabriel
 Okay..., 
 ++[...]
  Ah,
-I am sorry for asking too many questions.
 As an apology you can ask me questions too!
+[next]
-
+[next]
+[next]
// +[next]
// +[next]
// +[next]
// +[next]
-Ask me anything.

+[next]
-
->Questions1->

-~playCutscene("curtains")
->DONE

=Questions1

-
*[Who are you?]
 Me?
 I am Gabriel! 
 Nice to meet you.
 My dream is to be a ballet dancer one day!
 ++[next]
 ->Questions1
*[What are you doing here?]
++[next]
 ->Questions1
 
*[What is this place?]
++[next]
 ->Questions1

*[No not really]
++[next]
        ->->

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
        - -> END