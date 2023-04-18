using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Card
{
    public int Value { get; private set; }
    public char Operator { get; private set; }

    public Card(int value, int suit)
    {
        Value = (value % 13) + 1;
        Operator = GetOperator(suit);
    }

    private char GetOperator(int suit)
    {
        switch (suit % 4)
        {
            case 0:
                return '+';
            case 1:
                return '-';
            case 2:
                return '*';
            case 3:
                return '/';
            default:
                throw new ArgumentException("Invalid suit.");
        }
    }

    public override string ToString()
    {
        return $"[{Value}, {Operator}]";
    }
}

