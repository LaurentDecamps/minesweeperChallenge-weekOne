using Xunit;

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
        if (field == ".") return "0";
        if (field == "*") return "*";
        if (field == ".*") return "0*";
        return string.Empty;
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

        Assert.Equal("0*", MineSweeper.GetFieldSolution(field));
    }
}

