namespace Day13;

/*
If both values are integers, the lower integer should come first.
    If the left integer is lower than the right integer,the inputs are in the right order.
    If the left integer is higher than the right integer, the inputs are not in the right order.
    Otherwise, the inputs are the same integer; continue checking the next part of the input.
 
If both values are lists, compare the first value of each list, then the second value, and so on.
    If the left list runs out of items first, the inputs are in the right order.
    If the right list runs out of items first, the inputs are not in the right order.
    If the lists are the same length and no comparison makes a decision about the order,
    continue checking the next part of the input.

If exactly one value is an integer, convert the integer to a list which contains that integer as its only value,
    then retry the comparison.
    For example, if comparing [0,0,0] and 2, convert the right value to [2] (a list containing 2);
    the result is then found by instead comparing [0,0,0] and [2].
 */
public class PacketData
{
    public record Value
    {
        public int Compare(Value another)
        {
            switch (this, another)
            {
                case (Integer left, Integer right):
                    return left.Value.CompareTo(right.Value);
                case (List left, List right):
                    for (var i = 0; i < left.Count && i < right.Count; i++)
                    {
                        var comparison = left[i].Compare(right[i]);
                        if (comparison != 0)
                        {
                            return comparison;
                        }
                    }

                    return left.Count.CompareTo(right.Count);
                case (Integer left, List right):
                    return new List(new List<Value> { left }).Compare(right);
                case (List left, Integer right):
                    return left.Compare(new List(new List<Value> { right }));
            }

            return 0;
        }

        public static Value Parse(string input)
        {
            Stack<List> stack = new();
            var digits = string.Empty;
            foreach (var symbol in input)
            {
                switch (symbol)
                {
                    case '[':
                        stack.Push(new List(new()));
                        break;
                    case ']':
                        if (!string.IsNullOrEmpty(digits))
                        {
                            stack.Peek().Values.Add(new Integer(int.Parse(digits)));
                            digits = string.Empty;
                        }

                        if (stack.Count > 1)
                        {
                            var list = stack.Pop();
                            stack.Peek().Values.Add(list);
                        }

                        break;
                    case { } when char.IsDigit(symbol):
                        digits += symbol;
                        break;
                    default:
                        if (!string.IsNullOrEmpty(digits))
                        {
                            stack.Peek().Values.Add(new Integer(int.Parse(digits)));
                            digits = string.Empty;
                        }

                        break;
                }
            }

            return stack.Pop();
        }
    }

    public record Integer(int Value) : Value;

    public record List(List<Value> Values) : Value
    {
        public int Count => Values.Count;

        public Value this[int index] => Values[index];
    }
}