using System.Text.Json.Serialization;

namespace CDMA;

public class Station(string name, string message)
{
    public string Name { get; set; } = name;
    public string Message { get; set; } = message;
    public int[] WalshCode { get; set; }

    // Преобразование сообщения в бинарный ASCII код (8-битный)
    public string ConvertMessageToBinary()
    {
        return string.Join("", Message.Select(c => Convert.ToString((int)c, 2).PadLeft(8, '0')));
    }

    // Кодирование сообщения (смешивание с кодом Уолша)
    public int[] EncodeMessage()
    {
        string binaryMessage = ConvertMessageToBinary();
        int[] encodedMessage = new int[binaryMessage.Length * WalshCode.Length];

        for (int i = 0; i < binaryMessage.Length; i++)
        {
            int bit = binaryMessage[i] == '1' ? 1 : -1;
            for (int j = 0; j < WalshCode.Length; j++)
            {
                encodedMessage[i * WalshCode.Length + j] = bit * WalshCode[j];
            }
        }
        return encodedMessage;
    }
}