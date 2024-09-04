using System;

public class GameManager
{
    private static readonly Lazy<GameManager> _instance = new Lazy<GameManager>(() => new GameManager());

    public static GameManager Instance
    {
        get
        {
            return _instance.Value;
        }
    }
    

    public string SomeMethod()
    {
        return "Worked";
    }
}