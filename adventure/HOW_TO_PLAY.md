# How to Play

Welcome to the text adventure!  
In this game you will explore a world made up of **rooms**. Each room may contain **items** (things you can pick up, drop, or use) and **features** (parts of the environment you can interact with, such as a door, a chest, or a lever).  

Your goal is to interact with the world, solve puzzles, and discover what lies ahead.  

---

## 🗺 World Structure
- The world is divided into **rooms**.
- Rooms are connected by **exits** (north, south, east, west, up, down).
- Each room may contain:
  - **Items** — which can be picked up, carried in your inventory, dropped, or used.
  - **Features** — which are fixed objects in the room. Some may be unlocked or changed by using items on them.

---

## 🎮 Player Actions
You interact with the game by typing **commands**.  
Commands are usually one word (like `north`), or two words when interacting with objects (like `take key`).  

You can use **aliases** (shortcuts) for most commands.

---

## 📜 Command List

### Movement
| Command   | Aliases  | Description |
|-----------|----------|-------------|
| `north`   | `n`      | Move north. |
| `south`   | `s`      | Move south. |
| `east`    | `e`      | Move east.  |
| `west`    | `w`      | Move west.  |
| `up`      | `u`      | Move up.    |
| `down`    | `d`      | Move down.  |

---

### Looking Around
| Command   | Aliases                | Description |
|-----------|------------------------|-------------|
| `look`    | `l`, `x`, `examine`, `insp` | Look at a feature or item. Example: `look chest`. |

---

### Inventory
| Command      | Aliases  | Description |
|--------------|----------|-------------|
| `inventory`  | `i`, `inv` | List items you are carrying. |

---

### Item Interaction
| Command   | Aliases                  | Description |
|-----------|--------------------------|-------------|
| `take`    | `t`, `tk`, `get`, `grab` | Pick up an item from the room. Example: `take key`. |
| `drop`    | `dr`, `rm`               | Drop an item from your inventory into the current room. |
| `use`     | *(none)*                 | Use an item on a feature. Example: `use key on door`. |

---

### Other Commands
| Command     | Aliases      | Description |
|-------------|--------------|-------------|
| `help`      | `h`          | Show all commands and their aliases. |
| `tutorial`  | `tutor`      | Show a short tutorial about how the world works. |
| `exit`      | `quit`, `bye` | Exit the game. |

---

## 💡 Examples
- `n` → moves north  
- `take lantern` → picks up the lantern from the current room  
- `use lantern on altar` → attempts to use the lantern on the altar feature  
- `drop key` → drops the key into the current room  
- `i` → shows your inventory  

---

## 🔑 Tips
- Experiment! Some features may unlock new items, exits, or change the room when you use the right item on them.
- Don’t forget to check your **inventory** often.
- Use `look` frequently to inspect items and features closely.
- If stuck, type `help` to see available commands.
- All naming is case insensitive.
- Items and features may have several aliases. The item "basement key" might have "bkey" and "key" as aliases.
- If an item has a space in it, you can either find an alias, or wrap the name in quotes.

---

Happy exploring!