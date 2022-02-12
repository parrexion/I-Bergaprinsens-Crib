-> area3.elf_girl

=== area3 ===

= elf_girl
VAR elf_girl_visit=false
VAR Item_flask=false
VAR elf_girl_visit2=false
VAR elf_girl_visit3=false
VAR Item_shoe=false
VAR elf_girl_visit4=false
VAR elf_girl_visit5=false
VAR Item_rope=false
VAR elf_girl_visit6=false

{elf_girl_visit6: ->elf_girl_again6}

{elf_girl_visit5: ->elf_girl_again5}

{elf_girl_visit4: ->elf_girl_again4}

{elf_girl_visit3: ->elf_girl_again3}

{elf_girl_visit2: ->elf_girl_again2}

{elf_girl_visit: ->elf_girl_again1}

->elf_girl_intro

=elf_girl_intro
EG: Oh hello there you stallion! You’re looking like a snack today hihi
PR: What have I said about calling me weird names, knock it off
EG: Hihi calm down mr. handsome I’m just teasing with you
PR: And I’m just trying to live my life ok?
PR: ugh… I can’t believe I’m gonna ask you for help but I’m in a special situation here
EG: Oh no what have you done now naughty boy hihi
PR: I haven’t done anything and I’m very well behaved… when I need to at least
PR: Someone broke into my crib and stole everything…
PR: Even my 7.7" Boombox
EG: I’m very sorry to hear that, you want a hug? Maybe some snuggles to cheer you up?
PR: Oh god no. What I need is my things back. Have you seen anything?
EG: Aww, I wish I could help you but if ooonly I could share you my dirty little secrets.
EG: But maybe if you go buy me some party drink I could maybe share a secret or two hihi.
PR: Sometimes I wonder why I put up with this.
PR: Fine I’m gonna leave anyway so sure, I’ll help.
EG: Good boy hihi. Here’s some coin so you can pay that sweet nectar. #Item-get:coin
EG: Don’t go spend it on any of your desiiires now hihi
PR: AAAaaaand it’s time to go BYE
~ elf_girl_visit=true
->END

=elf_girl_again1
EG: Already gotten the beverage? Didn't expect any less from my cute hero.
{Item_flask: ->elf_girl_flaskgot}
PR: Well I haven't really gotten to it yet.
EG: Oh how cute, you stopped by just for me you sweetie.
PR: I better get out of here before she does something unexpected to me.
->END

=elf_girl_flaskgot
PR: I'm not glad you asked and somehow I actually got some.
EG: Oh yes, I like a man that isn't afraid to take action, rawr hihi. #Item-remove:flask
PR: Yikes this escalated quickly. Better run before it's too late.
EG: Wait before you go.
EG: Take this lovely bed as a gift of my appreciation! #Item-get:bed
EG: I wanted a change so I bought it and tried it out.
EG: But he bounce wasn't there, if you know what I mean hihi.
PR: I can just state she has no boundries. Thanks though!
EG: You're welcome hihi.
~ elf_girl_visit2=true
->END

=elf_girl_again2
EG: Oh hey darling how nice to see you again in your shining crown hihi.
PR: Yes yes hello hello.
EG: Oh how I am in need of a knight in shining armor, ooh.
PR: AAaaaah what now?
EG: I was simply taking a stroll by the lake when a big cat jumped at me.
EG: If only I had a strong and brave man by my side hihi.
PR: Get to the point miss overdramatic.
EG: I lost my shoe... my elegant high heel shoe... Can you please find it?
PR: I'll get right on it if you stop looking at me that way.
EG: I can't promise that hihi.
PR: You never learn do you?
EG: Don't be a stranger.
PR: Sometimes i wish I was.
~ elf_girl_visit3=true
-> END

=elf_girl_again3
EG: Aren't you a sight for sore eyes hihi.
EG: Maybe you're here to see if the shoe fits the princess hihi?
{Item_shoe: ->elf_girl_shoegot}
PR: Fun fact, apparently you can't find things if you don't look for them.
EG: Aww now you're the one teasing...
->END

=elf_girl_shoegot
PR: Wake up from your cinderella dreams.
PR: I did find your shoe but I ain't playing into any roleplay.
EG: You're so boring...
EG: Thank you so much though, even if I expected a little more effort. #Item-remove:shoe
PR: Amazing how you expected me to even do anything for you.
EG: I think I found your radio or what you call it. #Item-get:radio
EG: Found it at a second hand shop
EG: Well he told me it was second hand at least.
PR: I can't believe my eyes. It's actually my beautiful Boombox.
PR: I never knew someone could be this happy. Thanks!
EG: Any day love hihi!
~ elf_girl_visit4=true
->END

=elf_girl_again4
EG: Well isn't it my favourite errand boy?
PR: The fact that you even call me that is just flat out disrespectful.
EG: Oh don't be so overdramatic my little bobo hihi.
EG: Only thing missing in my bedroom chamber is a nice sturdy rope.
PR: Smart idea.
PR: Then if your house sets on fire you can escape through the window with the rope.
EG: Oh um right. That was totally my idea my innocent cutie hihi.
PR: Sure I can give it a go.
PR: Why stop now when I've gotten so far or something like that.
EG: I can allway rely on you my favourite prince hihi.
~ elf_girl_visit5=true
->END

=elf_girl_again5
EG: Oh hi I was just thinking of you.
PR: I can't say the say unfortunately
EG: You found a sturdy nice looking rope for me?
{Item_rope: ->elf_girl_ropegot}
PR: Right I forgot about that hehe.
EG: Sorry, you probably got lost in my eyes my tiny little suger cube hihi.
PR: That was a new one.
->END

=elf_girl_ropegot
PR: Long story short. Yepp!
EG: Wonderful my favourite honey bunny. #Item-remove:rope
EG: Care to test it out?
PR: I think I gave the lamp oil to that old man.
EG: Oh you silly. I will give you a break hihi.
PR: For once huh? Appreciated, thanks!
EG: Here take this poster, I've found a new idol now. #Item-get:poster
PR: Oh okey I mean that's a good looking guy if you're into it you know.
PR: I guess I'll put it on the wall
EG: If only I had a picture of you to replace that poster on the wall hihi.
PR: And that's my queue to leave.
PR: Thanks and bye!
~ elf_girl_visit6=true
->END

=elf_girl_again6
EG: Welcome back my dearest Prince. Here to pose for that photo I asked for?
PR: You need help and I need to fix my crib. Bye!
EG: Awww...
->END