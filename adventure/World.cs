Room hall = new Room(
    "Hall",
    "You are in a grand hall with marble floors.",
    [
        new Item("Study key", "A heavy iron hey", ["key", "skey"]),
        new Item("Pantry key", "A plain small key", ["key", "pkey"])
    ],
    [
        new Feature("Study door", "A large heavy wooden door. It's locked", "The door creaks open to uncover a study", "Study key", ["door", "sdoor"])
    ]
);

Room kitchen = new Room(
    "Kitchen",
    "A dark, dusty kitchen. The smell is awful."
);

hall.Exits = new Dictionary<Direction, Room> { [Direction.North] = kitchen };
kitchen.Exits = new Dictionary<Direction, Room> { [Direction.South] = hall };