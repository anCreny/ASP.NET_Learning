namespace WebIsland;

public class MathematicalCalculations
{
    public int Id { get; set; }
    public int FirstNumber { get; set; }
    public int SecondNumber { get; set; }
    public int Sign { get; set; }
    
    public int Answer { get; set; }

    public MathematicalCalculations(int id, int firstNumber, int secondNumber, int sign, int answer)
    {
        Id = id;
        FirstNumber = firstNumber;
        SecondNumber = secondNumber;
        Sign = sign;
        Answer = answer;
    }
}