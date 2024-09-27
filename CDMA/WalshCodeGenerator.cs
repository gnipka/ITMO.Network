namespace CDMA;

public class WalshCodeGenerator
{
    public static int[][] GenerateWalshCodes(int n)
    {
        int lenght = 1;
        while (lenght < n) lenght *= 2;
        
        int[][] walshMatrix = new int[lenght][];
        for (int i = 0; i < lenght; i++)
        {
            walshMatrix[i] = new int[lenght];
        }

        walshMatrix[0][0] = 1;
        
        for (int size = 1; size < lenght; size *= 2)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    walshMatrix[i + size][j] = walshMatrix[i][j];
                    walshMatrix[i][j + size] = walshMatrix[i][j];
                    walshMatrix[i + size][j + size] = -walshMatrix[i][j];
                }
            }
        }
        
        return walshMatrix.Take(n).ToArray();
    }
}