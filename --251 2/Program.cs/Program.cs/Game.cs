using System;
using System.IO;
using System.Text;

namespace CatAndMouse
{
    
    public enum GameState
    {
        Start,
        End
    }

    
    public class Game
    {
        public static string InputFile = "ChaseData.txt";
        public static string OutFile = "PursuitLog.txt";

        public int size;          
        public Player cat;
        public Player mouse;
        public GameState state;

        private StringBuilder logBuilder;
        private int caughtCell = -1;

        public Game(int size)
        {
            this.size = size;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
            state = GameState.Start;
            logBuilder = new StringBuilder();
        }

        public void Run()
        {
            if (!File.Exists(InputFile))
            {
                Console.WriteLine($"Ошибка: Входной файл {InputFile} не найден.");
                return;
            }

            string[] lines = File.ReadAllLines(InputFile);
            if (lines.Length == 0) return;

            
            if (int.TryParse(lines[0].Trim(), out int parsedSize))
            {
                this.size = parsedSize;
            }

            
            logBuilder.AppendLine("Cat and Mouse");
            logBuilder.AppendLine();
            logBuilder.AppendLine("Cat   Mouse   Distance");
            logBuilder.AppendLine("------------------");

            int lineIndex = 1;

            while (state != GameState.End && lineIndex < lines.Length)
            {
                string rawLine = lines[lineIndex].Trim();
                lineIndex++;

                if (string.IsNullOrWhiteSpace(rawLine)) continue;

                string[] parts = rawLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                char command = parts[0][0];

                if (command == 'M' || command == 'C')
                {
                    if (parts.Length > 1 && int.TryParse(parts[1], out int steps))
                    {
                        DoMoveCommand(command, steps);
                    }
                }
                else if (command == 'P')
                {
                    DoPrintCommand();
                }

               
                if (cat.state == State.Playing && mouse.state == State.Playing && cat.location == mouse.location)
                {
                    cat.state = State.Winner;
                    mouse.state = State.Loser;
                    caughtCell = cat.location;
                    state = GameState.End;
                }
            }

            if (state != GameState.End)
            {
                state = GameState.End;
                if (cat.state != State.Winner)
                {
                    mouse.state = State.Winner;
                    cat.state = State.Loser;
                }
            }

          
            logBuilder.AppendLine("------------------");
            logBuilder.AppendLine();
            logBuilder.AppendLine();
            logBuilder.AppendLine("Distance traveled:    Mouse       Cat");
            logBuilder.AppendLine($"                      {mouse.distanceTraveled,5}     {cat.distanceTraveled,5}");
            logBuilder.AppendLine();

            if (cat.state == State.Winner)
            {
                logBuilder.AppendLine($"Mouse caught at:  {caughtCell}");
            }
            else
            {
                logBuilder.AppendLine("Mouse evaded Cat");
            }

           
            File.WriteAllText(OutFile, logBuilder.ToString());
            Console.WriteLine($"Игра завершена. Результат сохранен в файл {OutFile}");
        }

        private void DoMoveCommand(char command, int steps)
        {
            switch (command)
            {
                case 'M':
                    mouse.Move(steps, size);
                    break;
                case 'C':
                    cat.Move(steps, size);
                    break;
            }
        }

        private void DoPrintCommand()
        {
            string catLocStr = (cat.state == State.NotInGame) ? "??" : cat.location.ToString();
            string mouseLocStr = (mouse.state == State.NotInGame) ? "??" : mouse.location.ToString();
            string distStr = "";

            if (cat.state == State.Playing && mouse.state == State.Playing)
            {
                distStr = GetDistance().ToString();
            }

            if (string.IsNullOrEmpty(distStr))
            {
                logBuilder.AppendLine($"{catLocStr,3}     {mouseLocStr,3}");
            }
            else
            {
                logBuilder.AppendLine($"{catLocStr,3}     {mouseLocStr,3}        {distStr,2}");
            }
        }

        private int GetDistance()
        {
            int directDist = Math.Abs(cat.location - mouse.location);
            return Math.Min(directDist, size - directDist);
        }
    }
}