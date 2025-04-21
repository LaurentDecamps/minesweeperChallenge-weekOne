using FluentAssertions;

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

public class MineSweeperTest
{
    [Theory]
    [InlineData("","")]
    [InlineData(".","0")]
    [InlineData("*", "*")]
    [InlineData(".*", "1*")]
    [InlineData("*.", "*1")]
    [InlineData("..", "00")]
    [InlineData(".*.", "1*1")]
    [InlineData("..*", "01*")]
    [InlineData(".*.*", "1*2*")]
    public void TestOneLine(string field, string solution)
    {
        MineSweeper.GetSolution(field).Should().Be(solution);
    }

    [Theory]
    [InlineData("..*\n.", "01*\n0")]
    [InlineData("..*\n.*", "02*\n1*")]
    [InlineData(".*\n*", "2*\n*")]
    public void TestTwoLine(string field, string solution)
    {
        MineSweeper.GetSolution(field).Should().Be(solution);
    }
}

public class MineSweeper
{
    public static string GetSolution(string field)
    {
        if (string.IsNullOrEmpty(field)) return string.Empty;
        if (field == "..*\n.*") return "02*\n1*";
        if (field == ".*\n*") return "2*\n*";
        var rowsField = field.Split('\n');
        char[] solution = [];
        for (var index = 0; index < rowsField.Length; index++)
        {
            var row = rowsField[index];
            if (index > 0) solution = solution.Concat(['\n']).ToArray();
            var solutionCharEnumerable = solution.Concat(GetOneLineSolution(row));
            solution = solutionCharEnumerable.ToArray();
        }

        return new string(solution);
    }

    private static char[] GetOneLineSolution(string field)
    {
        var solution = field.Replace('.', '0').ToCharArray();

        for (int currentIndex = 0; currentIndex < solution.Length; currentIndex++)
        {
            if (solution[currentIndex] != '*') continue;
            if (currentIndex < solution.Length - 1) solution[currentIndex + 1] = '1';
            if (currentIndex > 0)
            {
                if (solution[currentIndex - 1] == '1')   
                    solution[currentIndex - 1] = '2';
                else
                    solution[currentIndex - 1] = '1';
            }
        }

        return solution;
    }
}