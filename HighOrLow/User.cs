using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLow
{
    class User
    {
        string name;
        int highestScore;

        public User(string name, int highestScore)
        {
            this.name = name;
            this.highestScore = highestScore;
        }

        public void SetHighestScore(int highestScore)
        {
            if(highestScore > this.highestScore)
            {
                this.highestScore = highestScore;
            }
        }

        public string GetName()
        {
            return name;
        }

        public int GetHighestScore()
        {
            return highestScore;
        }

        public void ShowUser(int i)
        {
            Console.WriteLine($"{i}: {name}({highestScore})");
        }


    }
}
