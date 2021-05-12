using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighOrLow
{
    enum Color
    {
        Dimonds,
        Spades,
        Hearts,
        Clubs,
    }

    enum Value
    {
        Ace = 1, 
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }

    class Program
    {
        static void Main(string[] args)
        {

            User highscoreUser = new User("none", 0);
            bool flag = true;
            List<User> users = new List<User>();      
            Console.WriteLine("Welcome to this game called High or Low. The aim is to guess whether the next card is higher or lower than the previous.\n");
            while (flag) {
                int score = 0;
                User currentUser = AskNewOrOldUser(users);
                List<Card> cards = new List<Card>();
                GenerateCards(cards);
                for (int i = 0; i < 4; i++)
                {
                    List<Card> cards13 = Get13Cards(cards);
                    score = GuessCards(cards13, score, i + 1, highscoreUser, currentUser.GetName());
                }
                currentUser.SetHighestScore(score);
                users = users.OrderBy(x => x.GetHighestScore()).Reverse().ToList();
                if (score > highscoreUser.GetHighestScore())
                {
                    Console.Write($"WOW {currentUser.GetName()}, you got the new highscore of {score} points.");
                    if (highscoreUser.GetName() != "none")
                    {
                        Console.Write($" That is {score - highscoreUser.GetHighestScore()} more than {ManipulatedName(highscoreUser.GetName())} previuos highscore.\n");
                    }
                    
                    highscoreUser = currentUser;
                }
                else
                {
                    Console.WriteLine($"Your score was {score} points. That is {highscoreUser.GetHighestScore() - score} points from {ManipulatedName(highscoreUser.GetName())} highscore.");
                }
                Console.WriteLine("\nHere is the current highscore list:\n");
                ShowUsers(users);
                flag = AskYN();
            }
            
            Console.ReadLine();
        }

        static void GenerateCards(List<Card> cards)
        {
            foreach (Color color in Enum.GetValues(typeof(Color)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    cards.Add(new Card(color, value));
                }
            }

        }

        static List<Card> Get13Cards(List<Card> cards)
        {
            Random random = new Random();
            List<Card> newCards = new List<Card>();
            for (int i = 0; i < 13; i++)
            {
                int index = random.Next(0, cards.Count() - 1);
                newCards.Add(cards[index]);
                cards.RemoveAt(index);
            }
            return newCards;
        }

        static int GuessCards(List<Card> cards, int score, int round, User highscoreUser, string name)
        {
            int currentIndex = 0; 
            
            while (currentIndex < 13)
            {
                if(currentIndex == 12)
                {
                    Console.WriteLine($"Congrats, you guessed correct on 12 cards in a row, therfore you are rewarded with 50 extra points\n");
                    Console.WriteLine("Press any key to countinue.");
                    Console.ReadKey();
                    break;
                } else
                {
                    Console.WriteLine($"\nName: {name}");
                    Console.WriteLine($"Round: {round}");
                    Console.WriteLine($"Score: {score}");
                    Console.WriteLine($"High Score: {highscoreUser.GetHighestScore()}({highscoreUser.GetName()})\n");
                    for (int i = 0; i < 13; i++)
                    {
                        if (i <= currentIndex)
                        {
                            cards[i].ShowCard();
                        }
                        else
                        {
                            cards[i].ShowBackCard();
                        }
                    }
                    Card currentCard = cards[currentIndex];
                    Card nextCard = cards[currentIndex + 1];

                    bool isLower = AskHigherOrLower();
                    Console.Clear();
                    if (((!isLower && currentCard.GetValue() < nextCard.GetValue()) || (isLower && currentCard.GetValue() > nextCard.GetValue()) || (currentCard.GetValue() == Value.Ace || nextCard.GetValue() == Value.Ace)) && currentCard.GetValue() != nextCard.GetValue())
                    {
                        Console.WriteLine("\nYour guess was correct\n");
                        score++;
                        if (currentIndex == 11)
                        {
                            score += 50;
                        }
                    }
                    else
                    {

                        Console.WriteLine($"\nYour guess was incorrect. The next card was {nextCard.SpecificCard()}\n");

                        break;
                    }
                    currentIndex++;
                }
              
            }
            return score;
        }

        static void ShowUsers(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                users[i].ShowUser(i + 1);
            }
        }

        static bool AskYN()
        {
            bool flag = true;
            while (true)
            {
                Console.Write("\nWould You Like to try again(Y/N): ");
                string input = Console.ReadLine();
                if (input == "Y" || input == "y")
                {
                    Console.WriteLine("\nGreat");
                    break;
                }
                else if (input == "N" || input == "n")
                {
                    Console.WriteLine("\nOkey, press any key to exit.");
                    flag = false;
                    break;
                }
                else
                {
                    Console.WriteLine("\nThat is an invalid input. Please try again.");
                }
            }
            return flag;
        }

        static User AskNewOrOldUser(List<User> users)
        {
            User currentUser;
            while (true)
            { 
                try
                {
                    
                    if (users.Count == 0)
                    {
                        return AnswerNo();
                    } 
                    Console.Write("\nHave you played before(Y/N): ");
                    string input = Console.ReadLine();
                    if (input == "Y" || input == "y")
                    {
                        ShowUsers(users);
                        Console.Write("\nChoose your user(enter a number): ");
                        int position = int.Parse(Console.ReadLine());
                        currentUser = users[position - 1];
                        return currentUser;
                        
                    }
                    else if (input == "N" || input == "n")
                    {
                        return AnswerNo();
                    }
                    else
                    {
                        Console.WriteLine("\nThat is an invalid input. Please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please Try Again\n");
                }   
            }

            User AnswerNo()
            {
                Console.Write("\nOkey, please enter your name: ");
                string name = Console.ReadLine();
                currentUser = new User(name, 0);
                users.Add(currentUser);
                return currentUser;
            }
        }

        static bool AskHigherOrLower()
        {
            bool isLower;
            while (true)
            {
                Console.Write("\n\nDo you think the next card is higher or lower. Enter H or L: ");
                string input = Console.ReadLine();
                if (input == "H" || input == "h")
                {
                    isLower = false;
                    break;
                }
                else if (input == "L" || input == "l")
                {
                    isLower = true;
                    break;
                }
                else
                {
                    Console.WriteLine("\nThat is an invalid input. Please try again.");
                }
            }
            return isLower;
        }
        static string ManipulatedName(string name)
        {
            if (!name.EndsWith("s"))
            {
                name += "s";
            }
            return name;
        }
    }    
}
