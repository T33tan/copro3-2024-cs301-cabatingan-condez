using System;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace customizeRacingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMenu game = new GameMenu();
            game.MainMenu();
        }
    }
    
    class GameMenu
    {
        private Car car;
        private CustomizeNewCar carCustomization;
        private InputHandler inputHandler;

        public GameMenu()
        {
            car = new Car();
            carCustomization = new CustomizeNewCar(car);
            inputHandler = new InputHandler();
        }

        public void MainMenu()
        {
            string choice = string.Empty;

            bool validChoice = false;

            while (!validChoice)
            {
                Console.Clear();
                Console.Write("\x1b[3J");
                Console.Write(
                    "================================================================" +
                    "\n              R a c e   M a s t e r   2 0 2 4" +
                    "\n================================================================" +
                    "\n[1] Create New Car" +
                    "\n[2] Access Car(s)" +
                    "\n[3] Campaign Mode" +
                    "\n[4] Credits" +
                    "\n[5] Exit Game\n");
                Console.WriteLine("----------------------------------------------------------------");
                selectAgain:
                Console.Write("Select an Option (1 to 5): ");
                choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        NewCar();
                        validChoice = true;
                        break;
                    case "2":
                        LoadCar();
                        validChoice = true;
                        break;
                    case "3":
                        Campaign();
                        validChoice = true;
                        break;
                    case "4":
                        Credits();
                        validChoice = true;
                        break;
                    case "5":
                        Exit();
                        validChoice = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice! Select Again.");
                        goto selectAgain;
                }
            }
        }

        public void NewCar()
        {
            Console.Clear();
            string shortIntro = 
                "  You're about to create the car of your dream, destined to " +
                "\n navigate the nights of Atlanta. Choose your preference and " +
                "\n     play with the adrenaline of speed and the dream of " +
                "\n            establishing the legacy as a racer.\n";

            Console.WriteLine(
                "================================================================" +
                "\n        > > >  C A R   C U S T O M I Z A T I O N   < < < " +
                "\n================================================================");
            WriteStory(shortIntro);
            static void WriteStory(string story)
            {
                int speed = 20;

                foreach (char c in story)
                {
                    Console.Write(c);
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                        speed = 0;
                    Thread.Sleep(speed);
                }
            }

            Console.WriteLine("----------------------------------------------------------------" +
                "\nDo You Want to Continue?" +
                "\n[1] Yes, Start Car Customization" +
                "\n[2] No, Go Back to Main Menu" +
                "\n----------------------------------------------------------------");
            enterAgain:
            Console.Write("Choice: ");
            string decision = Console.ReadLine();

            if (decision == "1")
            {
                carCustomization.StartCustomization();
                ReturnToMenu();
            }
            if (decision == "2")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Invalid Choice! Select Again.");
                goto enterAgain;
            }
        }

        public void LoadCar()
        {
            Console.Clear();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\USERS\CHRISTIAN\SOURCE\REPOS\RACINGGAME_FINALS_TP\RACINGGAME_FINALS_TP\CARDATABASE.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            car.ShowAllCarNames(connectionString);
            Console.WriteLine(
                "-----------------------------------------------------------------" +
                "\n[1] Load a Car Information" +
                "\n[2] Delete a Car" +
                "\n[3] Display ALL Car(s) Information" +
                "\n[4] Back to Main Menu" +
                "\n-----------------------------------------------------------------");

            while (true)
            {
                Console.Write("Choose an option (1 to 4): ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        while (true)
                        {
                            Console.Clear();
                            car.ShowAllCarNames(connectionString);

                            Console.WriteLine("-----------------------------------------------------------------");
                            enterAgain:
                            Console.Write("Enter the Name You Want to Load: ");
                            string carNameLoad = Console.ReadLine().Trim();

                            if (car.DoesCarExist(carNameLoad, connectionString))
                            {
                                car.DisplayCarInfo(carNameLoad, connectionString);
                                ReturnToMenu();
                                return;
                            }
                            else
                            {
                                Console.WriteLine($"This Car Does Not Exist!");
                                goto enterAgain;
                            }
                        }

                    case "2":
                        while (true)
                        {
                            Console.Clear();
                            car.ShowAllCarNames(connectionString);

                            Console.WriteLine("-----------------------------------------------------------------");
                            deleteAgain:
                            Console.Write("Enter the Name You Want to Delete: ");
                            string carNameDelete = Console.ReadLine().Trim();

                            if (car.DoesCarExist(carNameDelete, connectionString))
                            {
                                Console.Write("-----------------------------------------------------------------" +
                                "\nDo you want to delete this car?" +
                                "\n[1] Yes" +
                                "\n[2] No" +
                                "\n-----------------------------------------------------------------" +
                                "\nChoice: ");
                                string confirmDelete = Console.ReadLine();

                                if (confirmDelete == "1")
                                    car.DeleteCarByName(carNameDelete, connectionString);
                                    ReturnToMenu();
                            }
                            else
                            {
                                Console.WriteLine($"This Car Does Not Exist!");
                                goto deleteAgain;
                            }
                        }

                    case "3":
                        Console.Clear();
                        Console.WriteLine(
                                "================================================================" +
                                "\n        A L L   C A R S   I N   T H E   D A T A B A S E" +
                                "\n================================================================");
                        car.DisplayAllCarInfo(connectionString);
                        ReturnToMenu();
                        break;

                    case "4":
                        MainMenu();
                        break;

                    default:
                        Console.WriteLine("Invalid Input! Please Select Again.");
                        break;
                }
            }
        }

        public void Campaign()
        {
            Campaign campaign = new Campaign();
            campaign.Run();
            ReturnToMenu();
        }

        public void Credits()
        {
            Console.Clear();
            Credits credits = new Credits();
            credits.Run();
            ReturnToMenu();
        }

        public void Exit()  
        {
            Console.WriteLine("Exiting the Game...");
            Environment.Exit(0);
        }

        public void ReturnToMenu()
        {
            Console.Write(
                    "----------------------------------------------------------------" +
                    "\nGo Back to Main Menu?" +
                    "\n[1] Yes              " +
                    "\n[2] No, Close the Game" +
                    "\n----------------------------------------------------------------");
            bool validChoice = false;

            while (!validChoice)
            {
                Console.Write("\nChoice: ");
                string choice = Console.ReadLine().Trim().ToLower();

                if (choice == "1" || choice == "yes")
                {
                    MainMenu();
                    validChoice = true;
                }
                else if (choice == "2" || choice == "no")
                {
                    Exit();
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid Input! Select Again.");
                }
            }
        }

    }

    class Car
    {
        public string Name { get; set; }
        public string CarType { get; set; }
        public string Brand { get; set; }

        public int Traction { get; set; }
        public int Chassis { get; set; }
        public int Engine { get; set; }
        public int Transmission { get; set; }
        public int Brake { get; set; }
        public int Nitro { get; set; }

        public string BodyType { get; set; }
        public string WheelSize { get; set; }
        public string CarColor { get; set; }
        public string FogLight { get; set; }
        public string HoodType { get; set; }
        public string CarExhaust { get; set; }
        public string WindowTint { get; set; }
        public string CarSpoiler { get; set; }
        public string RoofWrap { get; set; }
        public string UnderGlowColor { get; set; }

        public void StoreCarToDatabase(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string INSERT_query = @"
                        INSERT INTO CarDatabase 
                        (Name, CarType, Brand, Traction, Chassis, Engine, Transmission, Brake, Nitro, 
                         BodyType, WheelSize, CarColor, FogLight, HoodType, CarExhaust, WindowTint, 
                         CarSpoiler, RoofWrap, UnderGlowColor) 
                        VALUES 
                        (@Name, @CarType, @Brand, @Traction, @Chassis, @Engine, @Transmission, @Brake, @Nitro, 
                         @BodyType, @WheelSize, @CarColor, @FogLight, @HoodType, @CarExhaust, @WindowTint, 
                         @CarSpoiler, @RoofWrap, @UnderGlowColor)";

                    using (SqlCommand command = new SqlCommand(INSERT_query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@CarType", CarType);
                        command.Parameters.AddWithValue("@Brand", Brand);
                        command.Parameters.AddWithValue("@Traction", Traction);
                        command.Parameters.AddWithValue("@Chassis", Chassis);
                        command.Parameters.AddWithValue("@Engine", Engine);
                        command.Parameters.AddWithValue("@Transmission", Transmission);
                        command.Parameters.AddWithValue("@Brake", Brake);
                        command.Parameters.AddWithValue("@Nitro", Nitro);
                        command.Parameters.AddWithValue("@BodyType", BodyType);
                        command.Parameters.AddWithValue("@WheelSize", WheelSize);
                        command.Parameters.AddWithValue("@CarColor", CarColor);
                        command.Parameters.AddWithValue("@FogLight", FogLight);
                        command.Parameters.AddWithValue("@HoodType", HoodType);
                        command.Parameters.AddWithValue("@CarExhaust", CarExhaust);
                        command.Parameters.AddWithValue("@WindowTint", WindowTint);
                        command.Parameters.AddWithValue("@CarSpoiler", CarSpoiler);
                        command.Parameters.AddWithValue("@RoofWrap", RoofWrap);
                        command.Parameters.AddWithValue("@UnderGlowColor", UnderGlowColor);

                        command.ExecuteNonQuery();
                        Console.Clear();
                        Console.WriteLine("----------------------------------------------------------------" +
                            "\nCar Saved to Database Successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while saving to the database: " + ex.Message);
            }
        }

        public void DisplayCarInfo(string carName, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string SELECT_query = @"SELECT * FROM CarDatabase WHERE Name = @Name";

                    using (SqlCommand command = new SqlCommand(SELECT_query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", carName);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.Clear();
                                Console.WriteLine(
                                    $"----------------------------------------------------------------" +
                                    $"\n          Y O U R   C A R ' S   I N F O R M A T I O N" +
                                    "\n----------------------------------------------------------------" +
                                    $"\nName: {reader["Name"]}" +
                                    $"\nCar Type: {reader["CarType"]}" +
                                    $"\nCar Brand: {reader["Brand"]}" +
                                    $"\n------------------ P E R F O R M A N C E -----------------------" +
                                    $"\nTire Traction: Level {reader["Traction"]}" +
                                    $"\nChassis: Level {reader["Chassis"]}" +
                                    $"\nEngine: Level {reader["Engine"]}" +
                                    $"\nTransmission: Level {reader["Transmission"]}" +
                                    $"\nBrake: Level {reader["Brake"]}" +
                                    $"\nNitro: Level {reader["Nitro"]}" +
                                    $"\n---------------------- - D E S I G N----------------------------" +
                                    $"\nBody Type: {reader["BodyType"]}" +
                                    $"\nWheel Size: {reader["WheelSize"]}" +
                                    $"\nCar Color: {reader["CarColor"]}" +
                                    $"\nFog Lights: {reader["FogLight"]}" +
                                    $"\nHood Type: {reader["HoodType"]}" +
                                    $"\nWindow Color: {reader["WindowTint"]}" +
                                    $"\nExhaust Brand: {reader["CarExhaust"]}" +
                                    $"\nCar Spoiler: {reader["CarSpoiler"]}" +
                                    $"\nRoof Wrap: {reader["RoofWrap"]}" +
                                    $"\nUnderglow Color: {reader["UnderGlowColor"]}");
                            }
                            else
                            {
                                Console.WriteLine($"{carName} Has Not Been Found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occured: " + ex.Message);
            }
        }

        public void DisplayAllCarInfo(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string SELECT_query = @"SELECT * FROM CarDatabase";

                    using (SqlCommand command = new SqlCommand(SELECT_query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No cars found in the database.");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    Console.Clear();
                                    Console.Write(
                                        "================================================================" +
                                        "\n        A L L   C A R S   I N   T H E   D A T A B A S E" +
                                        "\n================================================================" +
                                        "\n           Press Any Key to Proceed to the Next Car" +
                                        "\n----------------------------------------------------------------" +
                                        $"\nN A M E : {reader["Name"]}" +
                                        "\n================================================================" +
                                        $"\nCar Type: {reader["CarType"]}" +
                                        $"\nCar Brand: {reader["Brand"]}" +
                                        "\n------------------- P E R F O R M A N C E ----------------------" +
                                        $"\nTire Traction: Level {reader["Traction"]}" +
                                        $"\nChassis: Level {reader["Chassis"]}" +
                                        $"\nEngine: Level {reader["Engine"]}" +
                                        $"\nTransmission: Level {reader["Transmission"]}" +
                                        $"\nBrake: Level {reader["Brake"]}" +
                                        $"\nNitro: Level {reader["Nitro"]}" +
                                        "\n------------------------ D E S I G N ---------------------------" +
                                        $"\nBody Type: {reader["BodyType"]}" +
                                        $"\nWheel Size: {reader["WheelSize"]}" +
                                        $"\nCar Color: {reader["CarColor"]}" +
                                        $"\nFog Lights: {reader["FogLight"]}" +
                                        $"\nHood Type: {reader["HoodType"]}" +
                                        $"\nWindow Color: {reader["WindowTint"]}" +
                                        $"\nExhaust Brand: {reader["CarExhaust"]}" +
                                        $"\nCar Spoiler: {reader["CarSpoiler"]}" +
                                        $"\nRoof Wrap: {reader["RoofWrap"]}" +
                                        $"\nUnderglow Color: {reader["UnderGlowColor"]}");
                                    Console.WriteLine();
                                    Console.ReadKey();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occurred: " + ex.Message);
            }
        }

        public void DeleteCarByName(string carName, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string deleteQuery = "DELETE FROM CarDatabase WHERE Name = @Name";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        SqlParameter carNameParam = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
                        carNameParam.Value = carName;
                        command.Parameters.Add(carNameParam);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery(); 

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"-----------------------------------------------------------------" +
                                $"\n{carName} Has Been Deleted!");
                        }
                        else
                        {
                            Console.WriteLine($"{carName} Has Not Been Found!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occured: " + ex.Message);
            }
        }

        public void ShowAllCarNames(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Name FROM CarDatabase";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.Write("\x1b[3J");
                            Console.WriteLine(
                                "-----------------------------------------------------------------" +
                                "\n       L I S T   O F   C A R (S)   I N   D A T A B A S E :" +
                                "\n-----------------------------------------------------------------");

                            int counter = 1;
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{counter}. {reader["Name"].ToString()}");
                                    counter++;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Cars Have Been Found in the Database.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occured: " + ex.Message);
            }
        }

        public bool DoesCarExist(string carName, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM CarDatabase WHERE Name = @Name";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", carName);
                        connection.Open();

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
    }

    class CustomizeNewCar
    {
        private Car car;
        public CustomizeNewCar(Car car)
        {
            this.car = car;
        }

        public void StartCustomization()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=C:\USERS\CHRISTIAN\SOURCE\REPOS\RACINGGAME_FINALS_TP\RACINGGAME_FINALS_TP\CARDATABASE.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            car.CarType = ChooseCarType();
            car.Brand = ChooseCarBrand();

            car.Traction = SetTractionLvl();
            car.Chassis = SetChassisLvl();
            car.Engine = SetEngineLvl();
            car.Transmission = SetTransmissionLvl();
            car.Brake = SetBrakeLvl();
            car.Nitro = SetNitroLvl();

            car.BodyType = ChooseBodyType();
            car.WheelSize = ChooseWheelSize();
            car.CarColor = ChooseColor();
            car.FogLight = AddFogLights();
            car.HoodType = ChooseHoodType();
            car.WindowTint = ChooseWindowTint();
            car.CarExhaust = ChooseCarExhaust();
            car.CarSpoiler = ChooseCarSpoiler();
            car.RoofWrap = AddRoofWrap();
            car.UnderGlowColor = ChooseUnderglow();
            car.Name = EnterCarName(connectionString);
            car.StoreCarToDatabase(connectionString);
            
        }

        private string EnterCarName(string connectionString)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(
                        "----------------------------------------------------------------" +
                        "\nC A R   N A M E :" +
                        "\n----------------------------------------------------------------" +
                        "\n8 to 16 characters only. Must have at least one special character" +
                        "\n    and a number. Must ENTER a valid name to save progress." +
                        "\n----------------------------------------------------------------");
                    EnterCarName:
                    Console.Write("Enter Car Name: ");
                    string carName = Console.ReadLine();

                    string pattern = @"^(?=.*\d)(?=.*[\W_]).{8,16}$";

                    if (!Regex.IsMatch(carName, pattern))
                    {
                        Console.WriteLine("Name Does Not Satisfy! Please Try Again.");
                        goto EnterCarName;
                    }

                    if (car.DoesCarExist(carName, connectionString))
                    {
                        Console.WriteLine($"This Name Already Exist! Try Another!");
                        goto EnterCarName;
                    }

                    return carName;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input format error: {ex.Message}");
                return "InvalidCarName";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return "ErrorCarName";
            }
        }
        private string ChooseCarType()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nC A R   T Y P E :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Sports Car        [4] Hybrid" +
                "\n[2] F1 Car            [5] Pony Car" +
                "\n[3] Muscle Car" +
                "\n----------------------------------------------------------------");
            string type;
            while (true)
            {
                Console.Write("Choose a Car Type (1 to 5): ");
                type = Console.ReadLine().Trim();

                switch (type)
                {
                    case "1":
                        return "Sports Car";
                    case "2":
                        return "F1 Car";
                    case "3":
                        return "Hybrid";
                    case "4":
                        return "Pony Car";
                    case "5":
                        return "Muscle Car";
                    default:
                        Console.WriteLine("Invalid Input! Select Preferred Car Type Above.");
                        break;
                }
            }
        }
        private string ChooseCarBrand()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nC A R   B R A N D :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Ferrari           [4] McLaren             [7] Chevrolet" +
                "\n[2] Lamborghini       [5] Audi" +
                "\n[3] Aston Martin      [6] Mercedes Benz" +
                "\n----------------------------------------------------------------");
            string brand;
            while (true)
            {
                Console.Write("Choose a Brand (1 to 7): ");
                brand = Console.ReadLine().Trim();

                switch (brand)
                {
                    case "1":
                        return "Ferrari";
                    case "2":
                        return "Lamborghini";
                    case "3":
                        return "Aston Martin";
                    case "5":
                        return "Audi";
                    case "4":
                        return "McLaren";
                    case "6":
                        return "Mercedes Benz";
                    case "7":
                        return "Chevrolet";
                    default:
                        Console.WriteLine("Invalid Input! Select Your Brand Above.");
                        break;
                }
            }
        }

        private int SetTractionLvl()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nC A R   P E R F O R M A N C E :" +
                "\n----------------------------------------------------------------" +
                "\nLevel 1 = Beginner (RECOMMENDED)      Level 3 = Normal" +
                "\nLevel 2 = Novice                      Level 4 = Hard" +
                "\n----------------------------------------------------------------" +
                "\n “Each level specifies the car control. The higher the level," +
                "\n       the faster the car but more difficult to control”" +
                "\n----------------------------------------------------------------");
            int traction;

            while (true)
            {
                Console.Write("1. Enter Tire Traction Level (Sharpens the Turn): ");
                if (int.TryParse(Console.ReadLine(), out traction) && traction >= 1 && traction <= 4)
                {
                    return traction;
                }

                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private int SetChassisLvl()
        {
            int chassis;
            while (true)
            {
                Console.Write("2. Enter a Chassis Level (Stabilize the Control): ");
                if (int.TryParse(Console.ReadLine(), out chassis) && chassis >= 1 && chassis <= 4)
                {
                    return chassis;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private int SetEngineLvl()
        {
            int engine;
            while (true)
            {
                Console.Write("3. Enter an Engine Level (Increase Top Speed): ");
                if (int.TryParse(Console.ReadLine(), out engine) && engine >= 1 && engine <= 4)
                {
                    return engine;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private int SetTransmissionLvl()
        {
            int transmission;
            while (true)
            {
                Console.Write("4. Enter a Transmission Level (Improves Acceleration): ");
                if (int.TryParse(Console.ReadLine(), out transmission) && transmission >= 1 && transmission <= 4)
                {
                    return transmission;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private int SetBrakeLvl()
        {
            int brake;
            while (true)
            {
                Console.Write("5. Enter a Brake Level (Increase the Stopping Power): ");
                if (int.TryParse(Console.ReadLine(), out brake) && brake >= 1 && brake <= 4)
                {
                    return brake;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private int SetNitroLvl()
        {
            int nitro;
            while (true)
            {
                Console.Write("6. Enter a Nitro Level (Boost the Car): ");
                if (int.TryParse(Console.ReadLine(), out nitro) && nitro >= 1 && nitro <= 4)
                {
                    return nitro;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }

        private string ChooseBodyType()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nB O D Y    T Y P E :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Sedan           [3] Coupes" +
                "\n[2] Hatchback" +
                "\n----------------------------------------------------------------");
            string type;
            while (true)
            {
                Console.Write("Select a Body Type (1 to 3): ");
                type = Console.ReadLine().Trim();

                switch (type)
                {
                    case "1":
                        return "Sedan";
                    case "2":
                        return "Hatchback";
                    case "3":
                        return "Coupes";
                    default:
                        Console.WriteLine("Invalid Input! Please Select Again.");
                        break;
                }
            }
        }
        private string ChooseWheelSize()
        {
            Console.Clear();
            Console.WriteLine(
               "----------------------------------------------------------------" +
               "\nW H E E L    S I Z E :" +
               "\n----------------------------------------------------------------" +
               "\n[1] Small (14 Inch)         [3] Large (19 Inch)" +
               "\n[2] Medium (16 Inch)        [4] Extra Large (21 Inch)" +
               "\n----------------------------------------------------------------");
            string size;
            while (true)
            {
                Console.Write("Select Wheel Size: ");
                size = Console.ReadLine().Trim();

                switch (size)
                {
                    case "1":
                        return "Small (14 inch)";
                    case "2":
                        return "Medium (16 inch)";
                    case "3":
                        return "Large (19 inch)";
                    case "4":
                        return "Extra Large (21 inch)";
                    default:
                        Console.WriteLine("Invalid Input! Please Select Again.");
                        break;
                }
            }
        }
        private string ChooseColor()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nC A R    C O L O R :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Black           [5] White" +
                "\n[2] Red             [6] Violet" +
                "\n[3] Blue            [7] Pink" +
                "\n[4] Yellow          [8] Gold" +
                "\n----------------------------------------------------------------");
            string color;
            while (true)
            {
                Console.Write("Choose a Car Color (1 to 8): ");
                color = Console.ReadLine().Trim();

                switch (color)
                {
                    case "1":
                        return "Black";
                    case "2":
                        return "Red";
                    case "3":
                        return "Blue";
                    case "4":
                        return "Yellow";
                    case "5":
                        return "White";
                    case "6":
                        return "Violet";
                    case "7":
                        return "Pink";
                    case "8":
                        return "Gold";
                    default:
                        Console.WriteLine("Invalid Input! Please Select Again.");
                        break;
                }
            }
        }
        private string AddFogLights()
        {
            Console.Clear();
            Console.Write(
                "----------------------------------------------------------------" +
                "\nA D D   F O G L I G H T S ?" +
                "\n----------------------------------------------------------------" +
                "\n[1] Yes" +
                "\n[2] No" +
                "\n----------------------------------------------------------------\n");
            string choice;
            while (true)
            {
                Console.Write("Choice: ");
                choice = Console.ReadLine().ToLower();

                if (choice == "1" || choice == "yes")
                {
                    return "Added";
                }

                else if (choice == "2" || choice == "no")
                {
                    return "Not Added";
                }
            }
        }
        private string ChooseHoodType()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nH O O D   T Y P E :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Steel          [3] Fiberglass" +
                "\n[2] Aluminum       [4] Carbon Fiber" +
                "\n----------------------------------------------------------------");

            string hoodType;
            while (true)
            {
                Console.Write("Select Hood Type (1 to 4): ");
                hoodType = Console.ReadLine().ToLower();

                switch (hoodType)
                {
                    case "1":
                        return "Steel";
                    case "2":
                        return "Aluminum";
                    case "3":
                        return "Fiberglass";
                    case "4 fiber":
                        return "Carbon Fiber";
                    default:
                        Console.WriteLine("Invalid Input! Please Select Again.");
                        break;
                }
            }
        }
        private string ChooseWindowTint()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nW I N D O W    T I N T :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Black           [5] White" +
                "\n[2] Red             [6] Violet" +
                "\n[3] Blue            [7] Pink" +
                "\n[4] Yellow          [8] Clear" +
                "\n----------------------------------------------------------------");
            string color;
            while (true)
            {
                Console.Write("Enter Window Tint (1 to 8): ");
                color = Console.ReadLine().Trim();

                switch (color)
                {
                    case "1":
                        return "Black";
                    case "2":
                        return "Red";
                    case "3":
                        return "Blue";
                    case "4":
                        return "Yellow";
                    case "5":
                        return "White";
                    case "6":
                        return "Violet";
                    case "7":
                        return "Pink";
                    case "8":
                        return "Clear";
                    default:
                        Console.WriteLine("Invalid Input! Please Select Again.");
                        break;
                }
            }
        }
        private string ChooseCarExhaust()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nC A R   E X H A U S T :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Yoshimura        [4] Corsa" +
                "\n[2] Akrapovic        [5] Flowmaster" +
                "\n[3] MagnaFlow        [6] MBRP" +
                "\n----------------------------------------------------------------");
            while (true)
            {
                Console.Write("Select an Exhaust Brand (1 to 6): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            return "Yoshimura";
                        case 2:
                            return "Akrapovic";
                        case 3:
                            return "MagnaFlow";
                        case 4:
                            return "Corsa";
                        case 5:
                            return "Flowmaster";
                        case 6:
                            return "MBRP";
                        default:
                            Console.WriteLine("Invalid Input! Please Select Again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private string ChooseCarSpoiler()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nC A R   S P O I L E R :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Lip Spoiler" +
                "\n[2] Pedestal Spoiler" +
                "\n[3] Lighted Spoiler" +
                "\n----------------------------------------------------------------");

            while (true)
            {
                Console.Write("Pick a Car Spoiler (1 to 3): ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            return "Lip Spoiler";
                        case 2:
                            return "Pedestal Spoiler";
                        case 3:
                            return "Lighted Spoiler";
                        default:
                            Console.WriteLine("Invalid Input! Please Select Again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private string AddRoofWrap()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nA D D   R O O F W R A P ?" +
                "\n----------------------------------------------------------------" +
                "\n[1] Yes" +
                "\n[2] No" +
                "\n----------------------------------------------------------------");
            string choice;
            string roofWrap;

            while (true)
            {
                Console.Write("Choice: ");
                choice = Console.ReadLine().ToLower();

                if (choice == "1" || choice == "yes")
                {
                    Console.WriteLine(
                        "----------------------------------------------------------------" +
                        "\nS E L E C T   A   R O O F W R A P " +
                        "\n----------------------------------------------------------------" +
                        "\n[1] Glossy Black          [5] Leopard Print" +
                        "\n[2] Carbon Fiber          [6] Polka Dot" +
                        "\n[3] Chrome Finish         [7] Camouflage" +
                        "\n[4] Zebra Print           [8] Clear Protection" +
                        "\n----------------------------------------------------------------");

                    while (true)
                    {
                        Console.Write("Choose a Roof Wrap (1 to 8): ");
                        roofWrap = Console.ReadLine().Trim();

                        switch (roofWrap)
                        {
                            case "1":
                                return "Glossy Black";
                            case "2":
                                return "Carbon Fiber";
                            case "3":
                                return "Chrome Finish";
                            case "4":
                                return "Zebra Print";
                            case "5":
                                return "Leopard Print";
                            case "6":
                                return "Polka Dot";
                            case "7":
                                return "Camouflage";
                            case "8":
                                return "Clear Protection";
                            default:
                                Console.WriteLine("Invalid Input! Please Select Again.");
                                break;
                        }
                    }
                }
                else if (choice == "2" || choice == "no")
                {
                    return "Not Added";
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please Select Again.");
                }
            }
        }
        private string ChooseUnderglow()
        {
            Console.Clear();
            Console.WriteLine(
                "----------------------------------------------------------------" +
                "\nU N D E R G L O W    C O L O R :" +
                "\n----------------------------------------------------------------" +
                "\n[1] Black           [4] Yellow" +
                "\n[2] Red             [5] White" +
                "\n[3] Blue            [6] Violet" +
                "\n----------------------------------------------------------------");
            string color;
            while (true)
            {
                Console.Write("Choose Underglow Color (1 to 6): ");
                color = Console.ReadLine().Trim();

                switch (color)
                {
                    case "1":
                        return "Black";
                    case "2":
                        return "Red";
                    case "3":
                        return "Blue";
                    case "4":
                        return "Yellow";
                    case "5":
                        return "White";
                    case "6":
                        return "Violet";
                    default:
                        Console.WriteLine("Invalid Input! Please Select Again.");
                        break;
                }
            }
        }
    }

    public struct CarModification
    {
        public string ModificationType;
        public double Cost;
        public int PerformanceIncrease;

        public CarModification(string modificationType, double cost, int performanceIncrease)
        {
            ModificationType = modificationType;
            Cost = cost;
            PerformanceIncrease = performanceIncrease;
        }
    }

    public interface IInputHandler
    {
        string GetStringInput(string prompt, string pattern = null);
        int GetIntInput(string prompt, int min, int max);
    }

    class InputHandler : IInputHandler
    {
        public string GetStringInput(string prompt, string pattern = null)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty! Please try again.");
                    continue;
                }

                if (pattern != null && !Regex.IsMatch(input, pattern))
                {
                    Console.WriteLine("Invalid format! Please try again.");
                    continue;
                }

                return input;
            }
        }

        public string GetStringInput(string prompt, int maxLength)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty! Please try again.");
                    continue;
                }

                if (input.Length > maxLength)
                {
                    Console.WriteLine($"Input exceeds maximum length of {maxLength} characters! Please try again.");
                    continue;
                }

                return input;
            }
        }

        public int GetIntInput(string prompt, int min, int max)
        {
            int input;
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out input) && input >= min && input <= max)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine($"Invalid input! Please enter a number between {min} and {max}.");
                }
            }
        }

        public int GetIntInput(string prompt, int min, int max, int defaultValue)
        {
            int input;
            while (true)
            {
                Console.Write($"{prompt} (Press Enter to use default: {defaultValue}): ");

                string userInput = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(userInput))
                {
                    return defaultValue; // Return default value if no input
                }

                if (int.TryParse(userInput, out input) && input >= min && input <= max)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine($"Invalid input! Please enter a number between {min} and {max}.");
                }
            }
        }
    }

    abstract class OtherOptions
    {
        public abstract void Run();
    }

    class Campaign : OtherOptions
    {
        private string campaignName;

        public Campaign(string campaignName = "Default Campaign")
        {
            this.campaignName = campaignName;
        }

        public override void Run()
        {
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.WriteLine(
                "                  C A M P A I G N   M O D E :" +
                "\n-----------------------------------------------------------------" +
                "\nClick Spacebar to Skip..." +
                "\n-----------------------------------------------------------------");
            string campaignStory = 
                "In the shadows of Atlanta's skyline, an underground race thrives, " +
                "\nhidden from the city's eyes. A community of racers test each other " +
                "\nfor the thirst of risk and speed. An event that happens every night, " +
                "\nwith the promise of riches and a legendary car with a legacy of its " +
                "\nown. Rumors spread that this car holds the speed. The street will " +
                "\nbecome your battlefield, where neon lights up the sky and engine " +
                "\nroars in the quest for glory." +
                "\n\n" +
                "As you accelerate into the starry nights, familiar faces and rivals " +
                "\nemerge from the tracks. A leader of another racers syndicate controls " +
                "\nthe scene. They have the power to overthrow your trip as they eliminate " +
                "\nthreats through their path. As each race progress, the tension builds, " +
                "\nand grows higher as their bumper closed to yours from uncovering the " +
                "\nlegendary car. Your skills are tested as you navigate a dangerous curve, " +
                "\ndodge police patrols, and forge an alliance to other racers that never " +
                "\nsleeps." +
                "\n\n" +
                "The final race emerge, a very important showdown across the iconic " +
                "\nPeachtree Street, stretching through abandonded factory, neon-lit " +
                "\nboulevards, and old-rail scrapyard. The racer's heartbeat pulse in beat " +
                "\nwith the engine as spectator hype the crowd for the ultimate battle. " +
                "\nWith the finish line in sight, they race not just for the victory but " +
                "\nfor legacy, freedom, and taste of true adrenaline. As the sun rays " +
                "\npierce the horizon, only one racer will emerge as Atlanta's king.\n";

            WriteStory(campaignStory);
            static void WriteStory(string story)
            {
                int speed = 20;

                foreach (char c in story)
                {
                    Console.Write(c);
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                        speed = 0;
                    Thread.Sleep(speed);
                }
            }
        }
    }

    class Credits : OtherOptions
    {
        public override void Run()
        {
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.WriteLine(
                "                     C R E D I T S :" +
                "\n----------------------------------------------------------------" +
                "\nThis Game is Created by the Group of: (Click Spacebar to Skip)" +
                "\n----------------------------------------------------------------");
            string creditsMember = 
                "1. Cabatingan, Joseph Allen (Leader)" +
                "\n2. Condez, Christian P. (Programmer)" +
                "\n3. Aquino, Lance Reimar (Documentation)\n";

            WriteMember(creditsMember);
            static void WriteMember(string story)
            {
                int speed = 20;

                foreach (char c in story)
                {
                    Console.Write(c);
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                        speed = 0;
                    Thread.Sleep(speed);
                }
            }
        }
    }
}