INCLUDE globals.ink

{ global_variable == false: -> main | -> already_chose }

-> main

=== main ===
Which pokemon do you choose?
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        ->chosen("Bulbasaur")
    + [Squirtle]
        -> chosen("Squirtle")

=== chosen(pokemon) ===
~ global_variable = true
You chose {pokemon}!
-> END

=== already_chose ===
You already chose {global_variable}!
-> END