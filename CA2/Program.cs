using System;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;  // library file handling

/*******************************************************************************************************
* Jessica Henry                                                                                        *  
* CA3                                                                                                  *
* Lecturer  Vivion                                                                                     *                                                                                         
* Create Date: 26/03/2021                                                                              *
********************************************************************************************************/
namespace CA2
{
    class Program
    {
        static Player[] players = new Player[5];
        const string TABLE_FORMAT = "{0,-5}{1,0}";
        const string TABLE_FORMAT_INPUT = "{0,-30}{1,-5}";
        const string TABLE_OUTPUT = "{0,-9}{1,-11}{2,-11}{3,-11}{4,-11}{5,-11}";
        const string TABLE_OUTPUT_ = "{0,-9}{1,-11}{2,-11}{3,-11}{4,-11}";
        static int count = 0;
        static int position = 0;
        static void Main(string[] args)
        {
            players = GetPlayersDetailsFromUser();
        }
        /// <summary>
        /// Read prompt from the user 
        /// </summary>
        /// <returns></returns>
        public static Player[] GetPlayersDetailsFromUser()
        {
            string choice = "";
            do
            {
                choice = PrintMenu();
                switch (choice)
                {
                    case "1":
                        players = AddPlayers();
                        break;
                    case "2":
                        ModifyDetails();
                        break;
                    case "3":
                        PrintAllPlayers();
                        break;
                    case "4":
                        PrintPlayerDetails();
                        break;
                    case "5":
                        PrintPlayerBonus();
                        break;
                    case "6":
                        PrintHighestScoring();
                        break;
                    case "7":
                        PrintAmountBonus();
                        break;
                    case "8":
                        ReadPlayers();        //read players from the file - folder bin
                        break;
                    case "9":
                        OutputTable();        //print table/console
                        break;
                    case "10":
                        OutputTableToFile(); //print out a file
                        break;
                    case "11":
                        break;
                    default:
                        Console.WriteLine("*** Wrong value, please try again");
                        Thread.Sleep(1500);
                        Console.Clear();
                        break;
                }
            } while (choice != "11");
            return players;
        }
        private static void OutputTableToFile()
        {
            StreamWriter sw = null;
            // Temp is in the bin folder of solution, so we dont need to specify location
            string path = @"temp12.txt";
            int numfields = 0;
            // After I change from Open to Append I don't need a loop anymore, because if I can't find the folder, the system is creating a new one.
            try
            {
                Console.WriteLine("\n Connecting to file \n");
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");
                for (int i = 0; i < count; i++)
                {
                    sw.WriteLine(players[i].ToString());
                    numfields++;
                }

                Console.WriteLine("{0} players was recorded", numfields);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }//end try 
            catch (FormatException e) //just in case
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("The file could not be opened {0}", ex.Message);
                Console.ReadKey();
            }

            finally
            {
                sw?.Close();
            }
        }
        public static Player[] AddPlayers()
        {
            string choice = "";
            bool result = false;
            string PlayerType = "";
            do
            {
                do
                {
                    Console.WriteLine("\n\n\t\tAdding a new player");
                    Console.WriteLine("-------------------------------------------------------------");
                    Console.WriteLine("Select your option");
                    Console.WriteLine("P - Player \nJ - Junior\nM - Menu");
                    PlayerType = Console.ReadLine().ToLower();
                    if (PlayerType == "m" || PlayerType == "menu")
                        break;
                    result = CheckFormat(PlayerType, "p", "j", "player", "junior", "Player Type");
                } while (!result);

                if (PlayerType == "m" || PlayerType == "menu")
                    break;

                if (PlayerType == "j" || PlayerType == "junior")
                    players[count] = new JuniorPlayer();
                else
                    players[count] = new Player();
                do
                {
                    Console.Write(TABLE_FORMAT_INPUT, "Insert Player's name", ":");
                    string name = Console.ReadLine();
                    result = IsPresent(name, "Player's name") && CheckString(name, "Player's name");
                    players[count].PlayerName = name;

                } while (!result);
                do
                {
                    int goalsScoreAux = 0;
                    Console.Write(TABLE_FORMAT_INPUT, "Goals Scored", ":");
                    string goalsScore = Console.ReadLine();
                    result = IsPresent(goalsScore, "Goals Scored") && IsInteger(goalsScore, "Goals Scored", ref goalsScoreAux);
                    players[count].GoalsScored = goalsScoreAux;

                } while (!result);
                do
                {
                    int matchesPlayedAux = 0;
                    Console.Write(TABLE_FORMAT_INPUT, "Maches Played", ":");
                    string matchesPlayed = Console.ReadLine();
                    result = IsPresent(matchesPlayed, "Maches Played") && IsInteger(matchesPlayed, "Maches Played", ref matchesPlayedAux);
                    players[count].MatchesPlayed = matchesPlayedAux;

                } while (!result);
                if (PlayerType == "j" || PlayerType == "junior")
                {
                    do
                    {
                        int ageTypeInt = 0;
                        Console.Write(TABLE_FORMAT_INPUT, "Enter Age of the Player Junior", ":");
                        string age = Console.ReadLine();
                        result = IsPresent(age, "Player Age") && IsInteger(age, "Maches Played", ref ageTypeInt) && CheckPositive(ageTypeInt, "Age") && CheckRange("Junior Age", 3, 17, ageTypeInt);
                        ((JuniorPlayer)players[count]).Age = ageTypeInt;
                    } while (!result);
                }
                //players[count] = player; ------> this part is for second option, in case you would like to insert in the array in the end 
                count++;
                if (count < 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("[SUCCESS] Player added!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(1500);
                    Console.Clear();
                    UserRedoAction("Add a new Player", out choice, out result);
                }
                else
                {
                    Console.WriteLine("Maximum 5 Players cadastrated, main menu");
                    Thread.Sleep(2000);
                }
            } while (count < 5 && (choice == "yes" || choice == "y"));
            return players;
        }

        /// <summary>
        /// This method reads the file and create the object Players and Juniors
        /// </summary>
        private static void ReadPlayers()
        {
            bool isValid = false;
            int countPlayersAdd = 0;
            string lineIn;

            // players.csv is in the bin folder of solution, so we dont need to specify location
            string path = @"players.txt";
           
            StreamReader sr = null;
            do
            {
                try
                {
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs);

                    Console.WriteLine("\n Reading all lines from CSV file players.csv ...\n");
                    // string[] playersInfo = new string[3];
                    bool result = false;
                    int numFields = 0;
                    int playerGoalsInt = 0; //when I am using ref i cant convert in line so for this reason I made this integer
                    int playerMachesInt = 0;

                    while ((lineIn = sr.ReadLine()) != null)
                    {
                        string[] playersInfo = lineIn.Split(',');
                        numFields = playersInfo.Length;

                        result = IsPresent(playersInfo[0], "Player's name") && CheckString(playersInfo[0], "Player's name") &&
                                 IsPresent(playersInfo[1], "Goals Scored") && IsInteger(playersInfo[1], "Goals Scored", ref playerGoalsInt) &&
                                 IsPresent(playersInfo[2], "Maches Played") && IsInteger(playersInfo[2], "Maches Played", ref playerMachesInt);

                        if (!result)
                            Console.WriteLine("Error data format {0} object not created", lineIn);
                        else
                        {
                            if (numFields==4)
                                players[count] = new JuniorPlayer(playersInfo[0], playerGoalsInt, playerMachesInt, int.Parse(playersInfo[3]));
                            else
                                players[count] = new Player(playersInfo[0], playerGoalsInt, playerMachesInt);

                            count++;
                            countPlayersAdd++;
                        }
                    }
                    isValid = true;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File {0} Not found ", path);
                    Console.WriteLine("\nPlease insert a new path for the file or press E to exit : ");
                    path = Console.ReadLine().ToLower();
                    if (path == "e" || path == "") //if the user chooses to exit
                        GetPlayersDetailsFromUser();
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                catch (FormatException e)  
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                catch (IOException ex)
                {
                    Console.WriteLine("The file could not be opened {0}", ex.Message);
                    break;
                }
                finally
                {
                    sr?.Close();
                }
            } while (!isValid);

            Console.WriteLine("\nTotal of players Add {0}", countPlayersAdd);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void OutputTable()
        {
            Console.WriteLine("\n All Players: ");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("                      Sligo Rovers  - 2021                        ");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine(TABLE_OUTPUT_, "Id", "Name", "Goals", "Matches", "Bonus");

            foreach (var playersInfo in players)
            {
                Console.WriteLine(playersInfo);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
        public static string PrintMenu()
        {
            string choice = "";
            Console.Clear();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("                    Sligo Rovers System                      ");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(TABLE_FORMAT, " 1.", "Add a player to the team");
            Console.WriteLine(TABLE_FORMAT, " 2.", "Modify a players details");
            Console.WriteLine(TABLE_FORMAT, " 3.", "Print all player details including their bonus");
            Console.WriteLine(TABLE_FORMAT, " 4.", "Print the details of a particular player ");
            Console.WriteLine(TABLE_FORMAT, " 5.", "Print a player’s bonus");
            Console.WriteLine(TABLE_FORMAT, " 6.", "Print the details of the highest scoring player");
            Console.WriteLine(TABLE_FORMAT, " 7.", "Print the total amount of bonus to be paid");
            Console.WriteLine(TABLE_FORMAT, " 8.", "Add Players from the File");
            Console.WriteLine(TABLE_FORMAT, " 9.", "Print Out all Players");
            Console.WriteLine(TABLE_FORMAT, " 10.", "Print Out File of all players");
            Console.WriteLine(TABLE_FORMAT, " 11.", "Exit");
            Console.WriteLine("-------------------------------------------------------------");
            choice = CheckUserMenuInput();
            return choice;
        }
        /// <summary>
        /// 
        /// </summary>
        private static void PrintAmountBonus()
        {
            int amountBonus = 0;
            for (int i = 0; i < count; i++)
            {
                amountBonus += players[i].CalcBonus();
            }
            Console.WriteLine("\nTotal Amount of bonus for all players is {0:C}", amountBonus);
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
        private static void PrintHighestScoring()
        {
            Console.WriteLine("* Option 6: ");
            int max = 0;
            int l_position = 0;

            for (int i = 0; i < count; i++)
            {
                if (players[i].GoalsScored > max)
                {
                    max = players[i].GoalsScored;
                    l_position = i;
                }
            }
            Console.WriteLine("Highest Scoring is ");
            PrintHeader(l_position);
            Console.WriteLine(players[l_position].ToString());
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
        private static void PrintPlayerDetails()
        {
            string l_choice = "";
            bool result = false;
            int playerNumID = 0;
            Console.WriteLine("\n\n\t\tDetails of Player");
            Console.WriteLine("-------------------------------------------------------------");
            do
            {
                do
                {
                    Console.Write("\nEnter soccer player ID: ");
                    string userInput = Console.ReadLine().ToLower();
                    result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "Code", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");

                } while (!result);

                for (int i = 0; i < count; i++)
                {
                    if (players[i].PlayerID == playerNumID)
                    {
                        Console.WriteLine("\n");
                        PrintHeader(i);
                        Console.WriteLine(players[i].ToString());
                        break;
                    }
                }
                UserRedoAction("\nWould you like to consult another player? ", out l_choice, out result);
            } while ((l_choice == "yes" || l_choice == "y"));

        }
        private static void PrintAllPlayers()
        {
            Console.WriteLine("\n All Players: ");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("                      Sligo Rovers  - 2021                        ");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine(TABLE_OUTPUT_, "Id", "Name", "Goals", "Matches", "Bonus");

            foreach (var playersInfo in players)
            {
                Console.WriteLine(playersInfo);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void ModifyDetails()
        {
            string l_choice = "";
            bool result = true;
            int playerNumID = 0;
            int EXIT = 0;
            string userChoice = "";
            int inputTypeInt = 0;
            string userInput;

            do
            {
                bool userMadeChanges = false;
                Console.WriteLine("\n\n\t\tModifying Player Information");
                Console.WriteLine("-------------------------------------------------------------");
                do
                {
                    Console.Write("Enter soccer player ID or press 'M' to Main Menu : ");
                    userInput = Console.ReadLine().ToLower();
                    if (userInput == "m" || userInput == "main") //I could have used a loop to control the exit,but for a quick solution and without much impact I found it easier to insert the if break.
                        break;

                    result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "ID Number", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");
                } while (!result);

                if (userInput == "m" || userInput == "main")
                    break;
                for (int i = 0; i < count; i++)
                {
                    if (players[i].PlayerID == playerNumID)
                    {
                        PrintHeader(i);
                        Console.WriteLine(players[i].ToString());
                        do
                        {
                            EXIT = 4;
                            do
                            {
                                Console.WriteLine("\n\nWhat would you like to change? ");
                                Console.WriteLine("1.  Name \n2.  Goals Scored  \n3.  Matches Played");
                                if (players[i].GetType() == typeof(JuniorPlayer))
                                {
                                    Console.WriteLine("4.  Age");
                                    EXIT = 5;
                                }
                                Console.WriteLine("{0}.  Exit", EXIT);
                                userChoice = Console.ReadLine();
                                result = IsPresent(userChoice, "Input Choice") && IsInteger(userChoice, "Input Choice", ref inputTypeInt) && CheckRange("User Input", 1, EXIT, inputTypeInt);
                            } while (!result);

                            if (userChoice == "1")
                            {
                                do
                                {
                                    Console.Write("Insira a new name for the user: ");
                                    string newName = Console.ReadLine();
                                    result = IsPresent(newName, "Player's name") && CheckString(newName, "Player's name");
                                    players[i].PlayerName = players[i].ModifyPlayersName(newName);

                                    userMadeChanges = true;
                                } while (!result);
                            }
                            else if (userChoice == "2")
                            {
                                int newGoalsScored = 0;
                                do
                                {
                                    Console.Write("Insira a new Goals Scored: ");
                                    string goalsScore = Console.ReadLine();
                                    result = IsPresent(goalsScore, "Goals Scored") && IsInteger(goalsScore, "Goals Scored", ref newGoalsScored);
                                } while (!result);

                                players[i].GoalsScored = players[i].ModifyGoalsScored(newGoalsScored);
                                userMadeChanges = true;
                            }
                            else if (userChoice == "3")
                            {
                                int newMatchesPlayed = 0;
                                do
                                {
                                    Console.Write("Insert a New Numbers of matches Played: ");
                                    string matchesPlayed = Console.ReadLine();
                                    result = IsPresent(matchesPlayed, "Matches Played") && IsInteger(matchesPlayed, "Matches Played", ref newMatchesPlayed);

                                } while (!result);
                                players[i].MatchesPlayed = players[i].ModifyMatchesPlayed(newMatchesPlayed);
                                userMadeChanges = true;
                            }
                            else if ((players[i].GetType() == typeof(JuniorPlayer) && userChoice == "4"))
                            {
                                int newAgeJuniorPlayer = 0;
                                do
                                {
                                    Console.Write("Enter Age of the Junior Player: ");
                                    string age = Console.ReadLine();
                                    result = IsPresent(age, "Junior Player Age") && IsInteger(age, "Junior Player Age", ref newAgeJuniorPlayer) && CheckPositive(newAgeJuniorPlayer, "Age") && CheckRange("Junior Age", 3, 17, newAgeJuniorPlayer);

                                } while (!result);
                                ((JuniorPlayer)players[i]).Age = ((JuniorPlayer)players[i]).ModifyJuniorPlayerAge(newAgeJuniorPlayer);


                                userMadeChanges = true;
                            }
                            if (userMadeChanges)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("[SUCCESS] Player details modified!\n");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                PrintHeader(i);
                                Console.WriteLine(players[i].ToString());
                                UserRedoAction("\nWould you like to modify another information about the player ?", out l_choice, out result);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("No information has been modified!");
                                Thread.Sleep(2000);
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }

                        } while (inputTypeInt != EXIT && (l_choice == "yes" || l_choice == "y"));
                    }
                }
                UserRedoAction("\nWould you like to modify another player ?", out l_choice, out result);
            } while (l_choice == "yes" || l_choice == "y");
        }
        private static void UserRedoAction(string questionIn, out string l_choice, out bool result)
        {
            do
            {
                Console.Write(questionIn + " (y) - Yes or (n) - No ");
                l_choice = Console.ReadLine().ToLower();
                result = CheckFormat(l_choice, "yes", "no", "y", "n", "Option");
            } while (!result);
        }
        private static void PrintPlayerBonus()
        {
            bool result = false;
            int playerNumID = 0;
            do
            {
                Console.Write("\nEnter Player ID : ");
                string userInput = Console.ReadLine();
                result = IsPresent(userInput, "Player ID") && IsInteger(userInput, "Code", ref playerNumID) && CheckPlayerID(playerNumID, "ID Number");

            } while (!result);
            for (int i = 0; i < count; i++)
            {
                if (players[i].PlayerID == playerNumID)
                {
                    Console.WriteLine("Player's bonus is {0:C}", players[i].CalcBonus());
                    break;
                }
            }
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }
        /// <summary>
        /// Check Validate the user Inputs
        /// </summary>
        /// <param name="textIn"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static bool IsPresent(string textIn, string name)
        {
            if (textIn == "")
            {
                Console.WriteLine(" {0} must be present", name);
                return false;
            }
            else
                return true;
        }
        static bool IsInteger(string textIn, string name, ref int userInt)
        {
            if (int.TryParse(textIn, out userInt))
                return true;
            else
            {
                Console.WriteLine("{0} must be of type integer", name);
                return false;
            }
        }
        static bool CheckPlayerID(int textIn, string message)
        {
            bool match = false;
            for (int i = 0; i < count; i++)
            {
                if (players[i].PlayerID == textIn)
                {
                    match = true;
                    position = i;
                    return match;
                }
            }
            Console.WriteLine(" {0}  {1} was not found", message, textIn);
            return match;
        }
        static bool CheckNoRegisteredPlayer()
        {
            if (players[0] == null)
            {
                Console.WriteLine(" | X | No player is registered, you must register it first");
                return false;
            }
            else
                return true;
        }
        static bool CheckFormat(string textIn, string value1, string value2, string value3, string value4, string name)
        {
            if (textIn == value1 || textIn == value2 || textIn == value3 || textIn == value4)
                return true;
            else
            {
                Console.WriteLine("{0} Must to be {1} ({3})  or  {2} ({4})", name, value1, value2, value3, value4);
                return false;
            }
        }
        public static string CheckUserMenuInput()
        {
            string choice;
            bool result = false;
            int temporary = 0; //I'm not using this variable to sent values, created just as mandatory Ref in my method IsInteger.
            do
            {
                Console.Write("\nSelect an option from the menu above: ");
                result = true;
                choice = Console.ReadLine();
                if (choice != "1")
                    result = IsPresent(choice, "Option") && IsInteger(choice, "Option", ref temporary); //&& CheckNoRegisteredPlayer();
            } while (!result);
            return choice;
        }
        public static bool CheckPositive(int num, string name)
        {
            if (num >= 0)
                return true;
            else
            {
                Console.WriteLine("{0} must be positive", name);
                return false;
            }
        }
        public static void PrintHeader(int index)
        {
            if (players[index].GetType() == typeof(JuniorPlayer))
                Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "Junior Age");
            else
                Console.WriteLine(TABLE_OUTPUT, "Id", "Name", "Goals", "Matches", "Bonus", "");
        }
        private static bool CheckRange(string textIn, int min, int max, int UserIn)
        {
            if (UserIn >= min && UserIn <= max)
                return true;
            else
            {
                Console.WriteLine("{0} has to be between {1} and {2} ", textIn, min, max);
                return false;
            }
        }
        private static bool CheckString(string inputIn, string messageIn)
        {

            if (!Regex.IsMatch(inputIn, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("{0} must contain only letters ", messageIn);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}


