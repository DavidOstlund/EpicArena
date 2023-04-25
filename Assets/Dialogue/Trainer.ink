INCLUDE globals.ink

VAR send_to_arena = false

-> main

=== main ===
What do you want to do?
    + [Go to the Arena]
        ~ send_to_arena = true
        -> arenaDialogue
    + [Go the the Shop]
        Sorry, this is not implemented yet
        ->endOfDialogue
    + [Nothing]
        ->endOfDialogue

=== endOfDialogue ===
Bye
-> END

=== arenaDialogue ===
Good luck!
-> END