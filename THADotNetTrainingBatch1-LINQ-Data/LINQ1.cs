namespace THADotNetTrainingBatch1_LINQ_Data;

public  class LINQ1
{
    public static List<int> GenerateRandomNumsV1(int count = 10 , int min = 0, int max = 100)
    {
        Random random = new Random();
        List<int> randomNums = new List<int>();
        for (int i = 0; i < count; i++)
        {
            randomNums.Add(random.Next(min, max));
        }
        return randomNums;
    }

    public static List<int> GenerateRandomNumsV2(int count = 10 , int min = 0 , int max = 100)
    {
        Random random = new Random();
        return Enumerable.Range(0,  count).Select( x => random.Next(min, max)).ToList();
    }

}
