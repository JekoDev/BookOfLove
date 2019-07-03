//This Code is used for storing information between scenes

using System.Collections.Generic;

public static class GameM
{
    private static float PlayerX;
    private static List<Item> Itemlist;
    private static Item Selected;
    private static Dictionary<string, string> Save = new Dictionary<string, string>();

    public static Dictionary<string, string> save
    {
        get{
            return Save;
        }
        set{
            Save = new Dictionary<string, string>(value);
        }
    }

    public static float playerX
    {
        get
        {
            return PlayerX;
        }
        set
        {
            PlayerX = value;
        }
    }

    public static Item selected
    {
        get
        {
            return Selected;
        }
        set
        {
            Selected = value;
        }
    }

    public static List<Item> itemlist
    {
        get
        {
            return Itemlist;
        }
        set
        {
            Itemlist = value;
        }
    }

}