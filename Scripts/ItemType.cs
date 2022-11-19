using System.Collections.Generic;

public class ItemType
{
    public static List<ItemType> ItemTypes = new()
    {

        new () { Cost = 3, Name = "Bread", Level = 1 },
        new() { Cost = 2, Name = "Apple", Level = 1 },

        new() { Cost = 20, Name = "Hat", Level = 2 },
        new() { Cost = 15, Name = "Shirt", Level = 2 },

        new() { Cost = 70, Name = "Headphones", Level = 3 },
        new() { Cost = 120, Name = "Printer", Level = 3 },

        new() { Cost = 400, Name = "Laptop", Level = 4 },
        new() { Cost = 250, Name = "Television", Level = 4 },
    };

    public int Cost { get; set; }
    public string Name { get; set; } = "";
    public int Level { get; set; } = 1;
    public string IconPath
    {
        get
        {
            return $"res://Art/Icons/{Name}.png";
        }
    }
}