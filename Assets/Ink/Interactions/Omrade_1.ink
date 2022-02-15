-> area1.shop_man

=== area1 ===

= area1_sign
Field <br>  North to the Lake <br>  East to the Forest
->END

= shop_man
VAR Item_lampoil=false
VAR Item_axe=false
VAR shop_man_visit1=false
VAR shop_man_visit2=false
VAR Item_coin=false
VAR Item_plant=false
VAR lampoil_not_bought=true
VAR axe_not_bought=true

{not lampoil_not_bought && not axe_not_bought: ->shop_man_soldout}
{shop_man_visit2: ->shop_man_again}
{shop_man_visit1: ->shop_man_intro2}
->shop_man_intro1

=shop_man_intro1
PR: Hey man, you won’t believe what just happened to me.
SM: Happened you say?
SM: I don’t believe in anything, so what’s wrong?
PR: Someone broke into my crib and stole all of my stuff.
SM: Stuff? Like your belongings you say?
PR: Yeah it’s all gone and I have no idea who did it.
PR: Did you see anything?
SM: See anything you say? I see many things, but not anyone else.
PR: Darn it…
PR: What do I do now?
SM: What to do you say? Well go look for them.
SM: They can’t have gone far.
PR: True, I can’t give up now.
PR: But what makes you so certain?
SM: Certain you say? I mean your bed is heavy as a mother tr….
SM: I MEAN, a lot of things yes.
PR: oookey?
PR: You know where to start?
SM: Start you say? Maybe ask the others that live here?
PR: Right, that I will do then, thanks!
SM: No problem.
SM: Stop by anytime if you want to ask or buy something, I got nice wares.
PR: Will do! Peace!
~ shop_man_visit1=true
->shop_man_goodluck

=shop_man_intro2
SM: Why, hello there. Want to buy my wares?
+ [Sure]
    PR: Sure! What you got?
     ~ shop_man_visit2=true
    -> shop_man_sell
* [Credit card]
    PR: Do you accept credit card?
    SM: Accept what?
    PR: Ugh, nevermind.
    ->shop_man_intro3
+ [Nah]
    PR: Nah I'm good.
    -> shop_man_goodbye
    
=shop_man_intro3
SM: So want to buy my wares?
+ [Sure]
    PR: Sure! What you got?
     ~ shop_man_visit2=true
    -> shop_man_sell
+ [Nah]
    PR: Nah I'm good.
    -> shop_man_goodbye

=shop_man_again
SM: Hi again, interested in my wares?
->shop_man_sell

= shop_man_sell
{not lampoil_not_bought && not axe_not_bought: ->shop_man_soldout}
SM: What do you need? I've got {lampoil_not_bought:lamp oil{axe_not_bought: and }}{axe_not_bought:an axe}.
{lampoil_not_bought:
+ [Lamp oil]
    {not Item_coin:
    SM: You need coin to buy that.
    ->shop_man_sell
    }
    PR: This might come in handy, I'll buy it!
     ~ lampoil_not_bought=false
    SM: Excellent choice! #Item-get:lampoil #Item-remove:coin
    -> shop_man_sell
    }
{axe_not_bought:
+ [Axe]
    {not Item_plant:
    SM: I could trade it for a plant maybe.
    ->shop_man_sell
    }
    PR: This is always useful, I'll buy it!
     ~ axe_not_bought=false
    SM: Murderous choice! #Item-get:axe #Item-remove:plant
    -> shop_man_sell
    }
+ [I'm good]
    -> shop_man_goodbye

=shop_man_goodluck
SM: Good luck!

->END

= shop_man_goodbye
SM: Goodbye! {Item_lampoil:don't set anything on fire{Item_axe: and }}{Item_axe:don't cut yourself, or anyone else}
-> END

=shop_man_soldout
SM: Hey, I'm all sold out but thanks for stopping by.
->END