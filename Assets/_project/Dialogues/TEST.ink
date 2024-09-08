INCLUDE Globals.ink

~ speed = 0.1
~ speaker = "NPC"
~ sprite = "NPC"
~ audio = "NPC1"
first

~ speed = 0.2
~ speaker = "Player"
~ sprite = "player"
~ audio = "player1"
second 

+ [third] -> third
+ [end] -> end


== third ==
~ speed = 0.1
~ speaker = "NPC"
~ sprite = "NPC"
~ audio = "NPC2"
third
-> DONE

==end==
~ speed = 0.2
~ speaker = "Player"
~ sprite = "player"
~ audio = "player2"
end
-> DONE


