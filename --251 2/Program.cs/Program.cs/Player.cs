using System;

namespace CatAndMouse
{
    
    public enum State
    {
        Winner,
        Loser,
        Playing,
        NotInGame
    }

    
    public class Player
    {
        public string name;              
        public int location;          
        public State state = State.NotInGame; 
        public int distanceTraveled = 0; 

        public Player(string name)
        {
            this.name = name;
            this.location = -1; 
        }

     
        public void Move(int steps, int fieldSize)
        {
            if (state == State.NotInGame)
            {
              
                location = NormalizeLocation(steps, fieldSize);
                state = State.Playing;
                distanceTraveled = 0;
            }
            else if (state == State.Playing)
            {
                if (steps == 0) return;

              
                location = NormalizeLocation(location + steps, fieldSize);
            
                distanceTraveled += Math.Abs(steps);
            }
        }

    
        private int NormalizeLocation(int pos, int fieldSize)
        {
            int loc = (pos - 1) % fieldSize;
            if (loc < 0)
            {
                loc += fieldSize;
            }
            return loc + 1;
        }
    }
}