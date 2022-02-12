-> area2.lamp_man

=== area2 ===

= lamp_man
VAR Item_keys=false
VAR Item_lamp=false
VAR lamp_man_visit=false
VAR lamp_man_visit2=false

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
PR: Yeah I found them down by the lake
LM: Well would you look at that, useful for once #Item-remove:keys
PR: Wait what did you call me?
LM: Here’s the lamp I promised now scram #Item-get:lamp
PR: Aww sweet, this is going directly to the crib
~ lamp_man_visit2=true
->END

=lamp_man_again2
LM: Appreciate the help kid

->END
