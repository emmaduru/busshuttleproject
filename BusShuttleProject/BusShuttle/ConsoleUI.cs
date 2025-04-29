namespace BusShuttle;
using Spectre.Console;

public class ConsoleUI {
    DataManager dataManager;
    
    public ConsoleUI () {
        dataManager = new DataManager();
    }

    public void Show () {

        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select mode")
                .AddChoices(new[] {
                    "driver", "manager"
                }));


        if (mode == "driver") {
            Driver selectedDriver = AnsiConsole.Prompt(
                new SelectionPrompt<Driver>()
                    .Title("Select a Driver")
                    .AddChoices(dataManager.Drivers));

            Console.WriteLine("Now you are driving as " + selectedDriver.Name);

            Loop selectedLoop = AnsiConsole.Prompt(
                new SelectionPrompt<Loop>()
                    .Title("Select a loop")
                    .AddChoices(dataManager.Loops));

            Console.WriteLine("You selected " + selectedLoop.Name);

            string command;

            do {
                Stop selectedStop = AnsiConsole.Prompt(
                    new SelectionPrompt<Stop>()
                        .Title("Select a stop")
                        .AddChoices(selectedLoop.Stops));

                Console.WriteLine("You selected " + selectedStop.Name);

                int boarded = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter the number of boarded passengers"));

                PassengerData data = new PassengerData(boarded, selectedStop, selectedLoop, selectedDriver);

                dataManager.AddNewPassengerData(data);

                command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What's next?")
                        .AddChoices(new[] {
                            "continue", "end"
                        }));
            } while (command != "end");
            
        } else if (mode == "manager") {
            string command;

            do {
                command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What do you want to do?")
                        .AddChoices(new[] {
                            "add stop", "delete a stop", "list stops", "end"
                        }));

                if (command == "add stop") {
                    string newStopName = AnsiConsole.Prompt(
                        new TextPrompt<string>("Enter the a new stop name: "));
                    
                    dataManager.AddStop(new Stop(newStopName));
                } else if (command == "delete a stop") {
                    Stop selectedStop = AnsiConsole.Prompt(
                        new SelectionPrompt<Stop>()
                            .Title("Select a stop")
                            .AddChoices(dataManager.Stops));
                    dataManager.RemoveStop(selectedStop);
                } else if (command == "list stops") {
                    var table = new Table();
                    table.AddColumn("Stop Name");

                    foreach(var stop in dataManager.Stops) {
                        table.AddRow(stop.Name);
                    }
                    AnsiConsole.Write(table);
                }
            } while (command != "end");
        }
    }
    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}