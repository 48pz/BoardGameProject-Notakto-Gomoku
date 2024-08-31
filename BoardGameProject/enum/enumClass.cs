
namespace BoardGameProject
{
    public enum GameType
    {
        Notakto = 1,
        Gomoku = 2
    }

    public enum GameMode
    {
        HumanVSHuman = 1,
        ComputerVSHuman = 2
    }

    public enum Command
    {
        save = 999,
        load = 998,
        undo = 997,
        redo = 996,
    }
}
