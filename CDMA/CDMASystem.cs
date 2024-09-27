using System.Text.Json;
using System.Text.Json.Serialization;

namespace CDMA;

class StationModel
{
    public string Name { get; set; }
    public string Message { get; set; }

    public StationModel(string name, string message)
    {
        Name = name;
        Message = message;
    }
}

public class CDMASystem
{
    private List<Station> stations = new List<Station>();
    private int[] transmittedSignal;

    public CDMASystem(string jsonFilePath)
    {
        LoadStations(jsonFilePath);
        
        int[][] walshCodes = WalshCodeGenerator.GenerateWalshCodes(stations.Count);
        
        for (int i = 0; i < stations.Count; i++)
        {
            stations[i].WalshCode = walshCodes[i];
        }
    }
    
    // Загрузка станций из JSON
    private void LoadStations(string jsonFilePath)
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFilePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            
            stations = JsonSerializer.Deserialize<List<Station>>(jsonString, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при чтении JSON-файла: " + ex.Message);
        }
    }
    
    // Передача закодированных сигналов от всех станций
    public void Transmit()
    {
        int messageLength = stations[0].ConvertMessageToBinary().Length / 8;
        int codeLength = stations[0].WalshCode.Length; 
        transmittedSignal = new int[messageLength * codeLength * 8];

        foreach (var station in stations)
        {
            int[] encodedMessage = station.EncodeMessage();
            for (int i = 0; i < transmittedSignal.Length; i++)
            {
                transmittedSignal[i] += encodedMessage[i];
            }
        }
    }

    // Приём и декодирование сообщений
    public void Receive()
    {
        Receiver receiver = new Receiver();
        foreach (var station in stations)
        {
            string decodedMessage = receiver.DecodeMessage(transmittedSignal, station.WalshCode, station.ConvertMessageToBinary().Length / 8);
            Console.WriteLine($"{station.Name} передала сообщение: {decodedMessage}");
        }
    }
}