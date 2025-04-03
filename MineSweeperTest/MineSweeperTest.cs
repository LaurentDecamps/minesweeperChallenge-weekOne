namespace MineSweeperTest;

// 1/ only one line to make appear between 0 and 2 number of bombs
// Test 1 : "" => ""
// Test 2 : "." => "0"
// Test 3 : "*" => "*"
// Test 4 : ".*" => "1*"
// Test 5 : ".." => "00"
// Test 6 : ".*." => "1*1"
// Test 7 : "..*" => "01*" real need ?
// Test 8 : ".*.*" => "1*2*"
// 2/ second line to make appear between 0 and 5 number of bombs
// Test 9 : "..* => "01*
//           ."      0"
// Test 10 : "..* => "02*
//            .*"     1*"
// Test 11 : ".* => "2*
//            *"     *"
// Test 12 : ".* => "3*
//            **"    **"
// Test 13 : "*.* => "*4*
//            **"     **"
// Test 14 : "*.* => "*5*
//            ***"    ***"
// Test 15 : "*.*. => "*5*2  real need ?
//            ***."    ***2"
// 3/ third line to make appear between 0 and 8 number of bombs
// Test 16 : "..*. => "13*2  
//            *.*.     *6*3"
//            ***."    ***2"
// Test 17 : "*.*. => "*4*2  
//            *.*.     *7*3"
//            ***."    ***2"
// Test 18 : "***. => "***2  
//            *.*.     *8*3"
//            ***."    ***2"

public class MineSweeper
{
    public static string GetFieldSolution(string field)
    {
        const char mineChar = '*';
        const int noBombsNumber = 0;
        if (string.IsNullOrEmpty(field)) return string.Empty;
        char[] solutionArray = field.Replace('.', '0').ToCharArray();

        if (!field.Contains('*') || field.Length <= 1) return new string(solutionArray);
        
        // pour toutes les mines trouvées je veux ajouter 1 à l'index précédent et à l'index suivant
        for (int i = 0; i < field.Length; i++)
        {
            if (field[i] != mineChar) continue;

            var previousIndex = i - 1;
            if (i > 0 && solutionArray[previousIndex] != mineChar)
            {
                solutionArray[previousIndex] = (char)(getNumericValueAtIndex(solutionArray, previousIndex) + 1 + '0');
            }

            var nextIndex = i + 1;
            if (i < field.Length - 1 && solutionArray[nextIndex] != mineChar)
            {
                solutionArray[nextIndex] = (char)(getNumericValueAtIndex(solutionArray, nextIndex) + 1 + '0');
            }
        }

        return new string(solutionArray);
    }

    private static int getNumericValueAtIndex(char[] solutionArray, int index)
    {
        return solutionArray[index] - '0';
    }
}

public class MineSweeperTest
{
    [Fact]
    public void EmptyField_Should_Return_Empty_Solution()
    {
        var field = string.Empty;

        Assert.Equal(string.Empty, MineSweeper.GetFieldSolution(field));
    }

    [Fact]
    public void Test2()
    {
        var field = ".";

        Assert.Equal("0", MineSweeper.GetFieldSolution(field));
    }

    [Fact]
    public void Test3()
    {
        var field = "*";

        Assert.Equal("*", MineSweeper.GetFieldSolution(field));
    }

    [Fact]
    public void Test4()
    {
        var field = ".*";

        Assert.Equal("1*", MineSweeper.GetFieldSolution(field));
    }

    [Fact]
    public void Test5()
    {
        var field = "..";

        Assert.Equal("00", MineSweeper.GetFieldSolution(field));
    }

    [Fact]
    public void Test6()
    {
        var field = ".*.";

        Assert.Equal("1*1", MineSweeper.GetFieldSolution(field));
    }

    [Fact]
    public void Test7()
    {
        var field = "..*";

        Assert.Equal("01*", MineSweeper.GetFieldSolution(field));
    }
}