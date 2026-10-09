namespace CatAndMouse
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Game.InputFile = "ChaseData.txt";
            Game.OutFile = "PursuitLog.txt";

           
            Game game = new Game(26);
            game.Run();
        }
    }
}