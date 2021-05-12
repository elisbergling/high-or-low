using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLow
{
    class Card
    {
        Color color;
        Value value;

        public Card(Color color, Value value)
        {
            this.color = color;
            this.value = value;
        }

        public string SpecificCard()
        {
            return color.ToString() + " " + EnumToValue();
        }
        private string EnumToValue()
        {
            switch (value)
            {
                case Value.Ace: return "A";
                case Value.Two: return "2";
                case Value.Three: return "3";
                case Value.Four: return "4";
                case Value.Five: return "5";
                case Value.Six: return "6";
                case Value.Seven: return "7";
                case Value.Eight: return "8";
                case Value.Nine: return "9";
                case Value.Ten: return "10";
                case Value.Jack: return "J";
                case Value.Queen: return "Q";
                case Value.King: return "K";
                default: return "something went wrong";
            }
        }

        public void ShowCard()
        {    
            Console.Write($"{SpecificCard()}, ");
        }

        public void ShowBackCard()
        {
            
            Console.Write("[BACKSIDE], ");
        }

        public Value GetValue()
        {
            return value;
        }
    }
}
