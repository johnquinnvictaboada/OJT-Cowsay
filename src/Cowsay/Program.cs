using System;
using Class;

class Program
{
    static void Main()
    {
        var cowsay = new Cowsay();
        cowsay.Reply += OnReply;


        Console.Write("-> Tell me what you want to say: ");
        string? input = Console.ReadLine();

        cowsay.Say(input);
    }

    static void OnReply(object sender, string reply)
    {
        // Retrieves reply from terminal and displays it
        Console.WriteLine(reply);
    }
}
