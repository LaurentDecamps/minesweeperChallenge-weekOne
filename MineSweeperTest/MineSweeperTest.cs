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
// Test 10 : "..* => "12*
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
    [InlineData("..*\n.*", "12*\n1*")]
    [InlineData(".*\n*", "2*\n*")]
    [InlineData(".*\n**", "3*\n**")]
    [InlineData("*.*\n**", "*4*\n**")]
    [InlineData("*.*\n***", "*5*\n***")]
    [InlineData("*.*.\n***.", "*5*2\n***2")]
    public void TestTwoLine(string field, string solution)
    {
        MineSweeper.GetSolution(field).Should().Be(solution);
    }
}

public class MineSweeper
{
    private static int _currentIndex;
    private static List<char[]> _rowSolutionList;

    public static string GetSolution(string field) 
    {
        if (string.IsNullOrEmpty(field)) return string.Empty;
        var rowsField = field.Split('\n');
        _rowSolutionList = [];
        for (var index = 0; index < rowsField.Length; index++)
        {
            _currentIndex = index;
            
            _rowSolutionList.Add(GetOneLineSolution(rowsField[index]));
        }
        
        var solution = string.Join("\n", 
            _rowSolutionList.Select(charArray => new string(charArray)));

        return solution;
    }

    private static char[] GetOneLineSolution(string field)
    {
        var solution = field.Replace('.', '0').ToCharArray();

        for (int currentIndex = 0; currentIndex < solution.Length; currentIndex++)
        {
            if (solution[currentIndex] != '*')
            {
                if (_currentIndex > 0 && 
                    currentIndex > 0 &&
                    _rowSolutionList[_currentIndex - 1][currentIndex-1] == '*')
                {
                    solution[currentIndex] =
                        AddANeighboringMineCount(solution, currentIndex);
                }
                continue;
            }
            
            UpdatePreviousRow(currentIndex);
            
            // Miss a test with "**"
            HandleNextSquare(currentIndex, solution);
            HandlePreviousSquare(currentIndex, solution);
        }

        return solution;
    }

    private static void HandlePreviousSquare(int currentIndex, char[] solution)
    {
        if (currentIndex > 0 && solution[currentIndex - 1] != '*')
            solution[currentIndex - 1]  = AddANeighboringMineCount(solution, currentIndex-1);
    }

    private static void HandleNextSquare(int currentIndex, char[] solution)
    {
        if (currentIndex < solution.Length - 1 && solution[currentIndex + 1] != '*') 
            solution[currentIndex + 1] = '1';
    }

    private static void UpdatePreviousRow(int currentIndex)
    {
        if (_currentIndex == 0) return;
        var previousRowSolution = _rowSolutionList[_currentIndex -1];
        HandleDiagonalSquareUpperRight(currentIndex, previousRowSolution);
        HandleTopSquare(currentIndex, previousRowSolution);
        HandleDiagonalSquareUpperLeft(currentIndex, previousRowSolution);
    }

    private static void HandleDiagonalSquareUpperLeft(int currentIndex, char[] previousRowSolution)
    {
        if (currentIndex <= 0) return;
        if (previousRowSolution[currentIndex - 1] != '*')
        {
            previousRowSolution[currentIndex - 1] = 
                AddANeighboringMineCount(previousRowSolution, currentIndex - 1);
        }
    }

    private static void HandleTopSquare(int currentIndex, char[] previousRowSolution)
    {
        if (currentIndex <= previousRowSolution?.Length && previousRowSolution[currentIndex] != '*') 
            previousRowSolution[currentIndex] = 
                AddANeighboringMineCount(previousRowSolution, currentIndex);
    }

    private static void HandleDiagonalSquareUpperRight(int currentIndex, char[] previousRowSolution)
    {
        if (currentIndex + 1 < previousRowSolution?.Length && previousRowSolution[currentIndex + 1] != '*') 
            previousRowSolution[currentIndex + 1] = 
                AddANeighboringMineCount(previousRowSolution, currentIndex + 1);
    }

    private static char AddANeighboringMineCount(char[] previousRowSolution, int index)
    {
        return (char)(previousRowSolution[index] + 1);
    }
}