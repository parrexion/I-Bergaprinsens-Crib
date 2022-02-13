
=== crib ===

= crib_item_check
VAR Furniture_got = 0
VAR Item_bed = false
VAR Item_carpet = false
VAR Item_chair = false
VAR Item_drawer = false
VAR Item_lamp = false
VAR Item_poster = false
VAR Item_radio = false

{Furniture_got >= 7: -> crib_complete}
{Item_bed: -> crib_bed}
{Item_carpet: -> crib_carpet}
{Item_chair: -> crib_chair}
{Item_drawer: -> crib_drawer}
{Item_lamp: -> crib_lamp}
{Item_poster: -> crib_poster}
{Item_radio: -> crib_radio}
->crib_no_update

= crib_complete
PR: It's finally complete!
PR: Man, that took some time to get ready.
PR: It's gonna be nice to finally get some rest after this.#Event:endgame
-> END 

= crib_bed
PR: My favourite belonging is back at last. #Item-place:bed
PR: Who doesn't like to sleep right?
-> crib_item_check

= crib_carpet
PR: I like my carpet. #Item-place:carpet
PR: Especially when I lie on it and question my existence.
-> crib_item_check

= crib_chair
PR: My lovely chair is finally back. #Item-place:chair
PR: I was starting to wonder where I would put all of my not-dirty-enough clothes.
-> crib_item_check

= crib_drawer
PR: Well it's a drawer. #Item-place:drawer
PR: Very practical.
PR: All my clothes are still there.
-> crib_item_check

= crib_lamp
PR: Let there be light! #Item-place:lamp
PR: Maybe another window would make it even brighter.
-> crib_item_check

= crib_poster
PR: This poster isn't the one I had before the break in. #Item-place:poster
PR: Oh well it's better than nothing.
-> crib_item_check

= crib_radio
PR: Aaah my dearest posession. #Item-place:radio
PR: My beasty Boombox finally back to its rightful owner.
-> crib_item_check

= crib_no_update
PR: There is still a lot of stuff missing.
->crib_leave


= crib_leave
PR: Well, I better get back to get new stuff for mah crib.
->END


= crib_startgame
PR: Ah, it was fun going on that trip but it's gonna be nice to get back home to my crib.
-> END

= crib_startgame2
PR: What!? Where is all my stuff?
PR: Someone must've stolen them! I need to find out who.
PR: Someone around here's gotta know what happened.
-> END

= crib_endgame
PR: Ugh, I didn't sleep that well. The bed was crazy hard. Almost like lying on the ground.
PR: What the...!
PR: Where did all my stuff go?
PR: Huh? A hat?
PR: Wait a minute... I recognize that hat.
PR: Damn it! It was him all along! #Event:credits
->END