namespace Projekt;

class Player {

    public Player() {
        Name = "player";
        MaxHealth = 50;
        Health = 50;

        // Runes
        Runes = new Dictionary<RuneType, int>();
        Runes.Add(RuneType.Earth, 0);
        Runes.Add(RuneType.Moon, 0);
        Runes.Add(RuneType.Ocean, 0);
        Runes.Add(RuneType.Sky, 0);
        Runes.Add(RuneType.Stars, 0);
        Runes.Add(RuneType.Sun, 0);

        // Max Runes
        MaxRunes = new Dictionary<RuneType, int>();
        MaxRunes.Add(RuneType.Earth, 20);
        MaxRunes.Add(RuneType.Moon, 20);
        MaxRunes.Add(RuneType.Ocean, 20);
        MaxRunes.Add(RuneType.Sky, 20);
        MaxRunes.Add(RuneType.Stars, 20);
        MaxRunes.Add(RuneType.Sun, 20);
    }

    public void Damage(int damage) {
        Health -= damage;
        if (Health < 0) {
            Health = 0;
        }
    }

    public void Collect(RuneType type, int ammount) {
        if (type == RuneType.Dark) {
            return;
        }
        Runes[type] = Runes[type] + ammount < MaxRunes[type] ? Runes[type] + ammount : MaxRunes[type];
    }


    public string Name { get; protected set; }
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; }
    public Dictionary<RuneType, int> MaxRunes { get; protected set; }
    public Dictionary<RuneType, int> Runes { get; protected set; }
}