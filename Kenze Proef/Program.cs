public class Program
{
    public static String PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt");
    public static StreamReader reader = new StreamReader(PATH);
    public static List<String> input = new List<String>();
    public static List<String> sixLetterWordList = new List<String>();
    public static List<String> smallerWords = new List<String>();

    public static void Main()
    {
        ReadInput();
        sixLetterWordList = FindSixLetterWord(input);
        FindWordCombinations(smallerWords, sixLetterWordList[1]);
        for (int i = 0; i < sixLetterWordList.Count; i++)
        {
            PrintCombinations(smallerWords, sixLetterWordList[i]);
        }
    }

    public static void ReadInput()
    {
        while (!reader.EndOfStream)
        {
            input.Add(reader.ReadLine());
        }
        reader.Close();
    }

    public static List<String> FindSixLetterWord(List<String> wordList)
    {
        List<String> tempSixLetterWordList = new List<String>();

        foreach (String word in wordList)
        {
            if (word.Length == 6)
            {
                tempSixLetterWordList.Add(word);
            }
            else if (word.Length < 6)
            {
                smallerWords.Add(word);
            }
        }
        return tempSixLetterWordList;
    }
    public static List<String> FindWordCombinations(List<String> smallerWords, String toFindWord)
    {
        List<String> currentCombination = new List<String>();
        bool found = FindCombinations(smallerWords, toFindWord, 0, currentCombination);

        return found ? currentCombination : new List<String>();
    }

    private static bool FindCombinations(List<String> smallerWords, String toFindWord, int startIndex, List<String> currentCombination)
    {
        if (startIndex == toFindWord.Length)
        {
            return true;
        }

        foreach (var word in smallerWords)
        {
            if (toFindWord.Substring(startIndex).StartsWith(word))
            {
                currentCombination.Add(word);
                if (FindCombinations(smallerWords, toFindWord, startIndex + word.Length, currentCombination))
                {
                    return true;
                }
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
        return false;
    }

    public static void PrintCombinations(List<String> smallwords, String toFindWord)
    {
        List<String> combinations = FindWordCombinations(smallerWords, toFindWord);
        String result = "";
        for (int i = 0; i < combinations.Count; i++)
        {
            if (i == combinations.Count - 1)
            {
                result += combinations[i];
                break;
            }
            result += combinations[i] + "+";
        }
        Console.WriteLine(result+"="+toFindWord);
    }

}