using CDMA;

string jsonFilePath = Console.ReadLine();

CDMASystem cdmaSystem = new CDMASystem(jsonFilePath);

cdmaSystem.Transmit();

cdmaSystem.Receive();