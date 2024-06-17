using System;
using Class;

class Program
{
    static void Main()
    {
        var cowsay = new Cowsay();
        cowsay.Reply += OnReply;


        //loopster keep asking for input
        while (true)
        {
            Console.Write("Enter input (or 'exit' to quit): ");
            string? input = Console.ReadLine();

            if (input?.ToLower() == "exit")
            {
                break; // Exit the loop if the user types 'exit'
            }

            cowsay.Say(input);
        }
    }

    static void OnReply(object sender, string reply)
    {
        // Retrieves reply from terminal and displays it
        Console.WriteLine(reply);
    }
}
