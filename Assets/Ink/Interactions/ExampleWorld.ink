
=== example_world ===


= example_sign
West to Northville <br> East to Southville
-> END




= mystery_man
VAR Item_RedPill = false
VAR Item_BluePill = false
VAR mystery_man_visited = false

{Item_RedPill && Item_BluePill : ->mystery_man_soldout } 
{not mystery_man_visited : ->mystery_man_intro}
->mystery_man_again

=mystery_man_intro
MM: Why, hello there. Wanna check out my wares?
+ [Why not?]
    PR: Why not? What are you selling?
    ~ mystery_man_visited = true
    -> mystery_man_sell
+ [Nah]
    PR: Nah
    -> mystery_man_goodbye
    
=mystery_man_again
MM: Hi again. Wanna check out my wares?
->mystery_man_sell

=mystery_man_soldout
MM: My favourite customer!
MM: I'm unfortunately sold out at the moment so come back later.
MM: See you around!
-> END

=mystery_man_sell
MM: I got some pills for sale. I got {not Item_RedPill:a red one{not Item_BluePill: and }}{not Item_BluePill:a blue one}. You're interested?
* [Get red pill]
    PR: I like red things so I'll take the red one.
    MM: Pleasure doing business with you! #Item-get:RedPill
    -> mystery_man_goodbye
* [Get blue pill]
    PR: I've heard blue are in fashion now so I'll take the blue one.
    MM: Pleasure doing business with you! #Item-get:BluePill
    -> mystery_man_goodbye
+ [Not interested]
        PR: I'm not really interested.
        -> mystery_man_goodbye

= mystery_man_goodbye
MM: Well goodbye then.
-> END



= col_mustard
VAR col_mustard_isfriend = false
{col_mustard_isfriend : -> col_mustard_friend}
{Item_RedPill : -> col_mustard_pill}
-> col_mustard_regular

= col_mustard_regular
PR: Yo yo!
YM: Hello there!
YM: Haven't seen you around these parts lately. Where have you been?
PR: Ah, you know. Here and there. Always busy.
YM: Can't say I do but good for you anyways, sonny.
PR: Later, gramps!
 -> END
 
= col_mustard_pill
YM: I saw that you got yourself some pills. You're sharing them?
+ [Sure]
    PR: Sure. Here take a red pill.
    YM: Cheers mate! #Item-remove:RedPill
    YM: Oh wow. That was some hot stuff.
    ~ col_mustard_isfriend = true
    PR: Oh yeah? Now I kinda regret not trying it myself.
+ [No]
    PR: No, I did not get them just to hand them away!
    YM: Tch, suit yourself then.
- -> END

= col_mustard_friend
YM: Why hello there, friend! Out doing business?
PR: You know how it is, always something that needs to be done.
YM: Sounds good. I'll see you around.
PR: Laters!
-> END


= building_sign
Closed for today.
A note hangs below the closing sign.
If those gosh darn kids would keep their hands to themselves.
Earlier this week when I went to get some more apples to restock the shelf
they snuck up and stole all my eggs.
And that's not all of it.
Yesterday all the eggs were returned to the store again.
I had to spend most of the day cleaning so I'm taking the day off today.
If only I wouldn't have to keep them in line all the time...
-> END