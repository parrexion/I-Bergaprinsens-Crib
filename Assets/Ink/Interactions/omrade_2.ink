-> area2.lamp_man

=== area2 ===

= lamp_man
VAR lamp_man_visit=false
VAR Item_keys=false
VAR lamp_man_visit2=false
VAR lamp_man_visit3=false
VAR lamp_man_visit4=false
VAR lamp_man_visit5=false
VAR lamp_man_visit6=false
VAR lamp_man_visit7=false

{lamp_man_visit7: ->lamp_man_again7}

{lamp_man_visit6: ->lamp_man_again6}

{lamp_man_visit5: ->lamp_man_again5}

{lamp_man_visit4: ->lamp_man_again4}

{lamp_man_visit3: ->lamp_man_again3}

{lamp_man_visit2: ->lamp_man_again2}

{lamp_man_visit: ->lamp_man_again1}
->lamp_man_intro

=lamp_man_intro
LM: Oi, who goes there?
PR: Whoa, it’s just me, the mountain prince.
LM: Oh it’s just you.
LM: Oh no… it IS actually you…
PR: Hey what do you mean by that you old fart?
LM: Listen here boy, what are you doing out here?
LM: Don’t you know it’s dangerous?
PR: Yo control that fire of yours old man
PR: I’m not just out on a stroll, someone stole all mah stuff
LM: Stuff?
PR: Yeah, everything in my mountain crib is gone.
LM: Well… that is unfortunate
LM: Haven’t your parents told you to lock the door?
PR: Haven’t YOUR parents told you about breaking in anyway?
PR: Whatever, you haven’t seen anything about it?
LM: Of course not, I’ve been busy keeping the forest safe from outlanders
LM: You should know this boy!
PR: ugh ye work and all that.
LM: Now if you wanna get somewhere in life then maybe you should help me for once
PR: With what? And what do I get out of it?
LM: You lil’... I lost my keys somewhere on my patrol, find them and I’ll give you this lamp.
PR: OOoo now that is a nice lamp, strikingly similar to what I had in mah crib.
PR: You got good taste, old man.
LM: humpf, thanks I guess. Now piss off before I get angry for real.
PR: Yo chill I’m outta here
~ lamp_man_visit=true
->END

=lamp_man_again1
LM: Oi boy, found my keys yet?
{Item_keys: ->lamp_man_keysgot}
PR: Nope, only found an ancient bearded old man
LM: PISS OFF
->END

=lamp_man_keysgot
PR: Yeah I found them down by the lake #Item-remove:keys
LM: Well would you look at that, useful for once
PR: Wait what did you call me?
LM: Here’s the lamp I promised now scram #Item-get:lamp
PR: Aww sweet, this is going directly to the crib
~ lamp_man_visit2=true
->END

=lamp_man_again2
LM: Hey who goes there? Oh it’s you.
PR: …
LM: I Appreciate the help kid, now that you’re here.
LM: If you could get me some more lamp oil for my latern I would concider not cutting you down the next time you show up.
PR: Yikes you could’ve asked nicely you stump.
~ lamp_man_visit3=true
->END

=lamp_man_again3
LM: Oi boy, that lamp oil for me?
{Item_lampoil: ->lamp_man_lampoilgot}
PR: Nope, but I did find this.
PR: <i>Farts on him and runs away</i>
LM: YOU LITTLE RAT!
->END

=lamp_man_lampoilgot
PR: SIR YES SIR!
LM: Fantastic! I didn't think you had it in you. #Item-remove:lampoil
PR: What can I say I'm full of surprises.
LM: Who knows, now get lost before i find my axe.
PR: <i>Gulp</i>
LM: Here you can take this flask of the finest homebrew in the land. #Item-get:flask
PR: Moonshine? Aww hell yeah.
LM: And also this chair I bought. It's not as good as the good ol' stumps.  #Item-get:chair
PR: Oh um thanks I guess.
LM: Now where did I put my axe?
PR: Oh shit better get moving then.
~ lamp_man_visit4=true
->END

=lamp_man_again4
LM: Show yourself!
PR: It's just me as always...
LM: oh right um... ye right...
PR: What's up gramps?
LM: Well I can't seem to find my axe, my good ol' mighty axe... <i>Sniff</i>
PR: I would say that's a YOU problem old man.
LM: Don't test your luck boy, my fists are still fir for a fight.
PR: We both know I can run away from you before you even had the thought to do anything.
LM: Listen here you twat, take this plant and hope the shop got an axe in stock. #Item-get:plant
LM: If you get me a new axe we can draw a big line over all this. What do you say?
PR: The line you crossed the second you saw me? Baah sure I'll help.
LM: Great, can't wait to chop... stuff... again...
PR: ...
~ lamp_man_visit5=true
->END

=lamp_man_again5
LM: Finally! Did he have an axe in stock?
{Item_axe: ->lamp_man_axegot}
PR: I haven't even check if I'm gonna be honest.
LM: then why aRE YOU HERE?!?
->END

=lamp_man_axegot
PR: He actually did.
LM: Wonderful I can feel my will to live come back to me #Item-remove:axe
PR: Now you can finally cut down some more trees gramps!
LM: Um yes, I will cut down... trees... yes um.
PR: Okey well that is not suspicios at all.
LM: If you want to climb or tie someone up, take this rope as a thanks! #Item-get:rope
PR: Now this will be useful when I finally find that burglar.
LM: Haha that's the spirit boy.
LM: Oh and one more thing. Take this drawer I bought second hand. #Item-get:drawer
PR: Oh cheers.
PR: I need everything I can get after all.
PR: Wait this one still got clothes in it.
PR: Almost identical to what I like to wear.
PR: Someone else got 
~ lamp_man_visit6=true
->END

=lamp_man_again6
LM: Hey Prince! I actually don't feel like hurting you anymore
LM: All thanks to you and your helpful hand.
PR: oh ok? That was unexpected but greatly appreciated.
PR: Maybe working isn't so bad after all.
LM: Haha you're good at being a delivery boy!
PR: Who would've known, take care you old fart.
->END

=lamp_man_again7
LM: You're not so bad after all!
->END
