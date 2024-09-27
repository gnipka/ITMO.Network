namespace CDMA;

public class Receiver
{
    public string DecodeMessage(int[] transmittedSignal, int[] walshCode, int messageLength)
    {
        int[] decodedMessage = new int[messageLength * 8];

        // Декодирование сообщения с помощью корреляции
        for (int i = 0; i < messageLength * 8; i++)
        {
            int sum = 0;
            for (int j = 0; j < walshCode.Length; j++)
            {
                sum += transmittedSignal[i * walshCode.Length + j] * walshCode[j];
            }
            decodedMessage[i] = sum > 0 ? 1 : 0;
        }
        
        return BinaryToText(decodedMessage);
    }

    // Преобразование бинарного массива обратно в строку ASCII
    private string BinaryToText(int[] binaryMessage)
    {
        string result = "";
        for (int i = 0; i < binaryMessage.Length; i += 8)
        {
            string byteString = string.Join("", binaryMessage.Skip(i).Take(8).Select(b => b.ToString()));
            int asciiValue = Convert.ToInt32(byteString, 2);
            result += (char)asciiValue;
        }
        return result;
    }
}