namespace LinearAlgebra.Classes;

public static class ExpressionHelpers
{
    public static Fraction EvaluateAsFraction(string expression, Dictionary<string, Fraction> variables)
    {
        string replaced = ReplaceVariables(expression, variables);
        List<string> postFix = InfixToPostfix(replaced);
        return CalCulate(postFix);
    }

    private static Fraction CalCulate(List<string> exp)
    {
        Stack<Fraction> stack = new Stack<Fraction>();
        for (int i = 0; i < exp.Count; i++)
        {
            if (IsOperator(exp[i]))
            {
                Fraction y = stack.Pop();
                Fraction x = stack.Pop();
                if (exp[i][0] == '*') x *= y;
                else if (exp[i][0] == '/') x /= y;
                else if (exp[i][0] == '+') x += y;
                else x -= y;
                stack.Push(x);
            }
            else
            {
                stack.Push(new Fraction(Convert.ToDouble(exp[i])));
            }
        }
        return stack.Pop();
    }
    private static string ReplaceVariables(string expression, Dictionary<string, Fraction> variables)
    {
        expression = expression.Replace("(", "( ");
        expression = expression.Replace(")", " )");
        expression = expression.Replace(" / ", "/");
        expression = expression.Replace("/", " / ");
        foreach (var variable in variables)
        {
            if (variable.Value.IsDecimal())
            {
                expression = expression.Replace(variable.Key.ToString(), $"( {variable.Value.Numerator} / {variable.Value.Denominator} )");
            }
            else
            {
                expression = expression.Replace(variable.Key.ToString(), $"( {variable.Value.Quotient} )");
            }
        }
        return expression;
    }

    public static List<string> InfixToPostfix(string expression)
    {
        List<string> result = new List<string>();
        Stack<char> stack = new Stack<char>();
        foreach (var item in expression.Split(' '))
        {
            string current = item.Trim();
            if (current[0] == '(')
            {
                stack.Push(current[0]);
            }
            else if (current[0] == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    result.Add(stack.Pop().ToString());
                }
                stack.Pop();
            }
            else if (IsOperator(current))
            {
                while (stack.Count > 0 && Prec(stack.Peek()) >= Prec(current[0]))
                {
                    result.Add(stack.Pop().ToString());
                }
                stack.Push(current[0]);
            }
            else
            {
                result.Add(current.ToString());
            }
        }

        while (stack.Count > 0)
        {
            result.Add(stack.Pop().ToString());
        }

        return result;
    }

    private static bool IsOperator(string c)
    {
        return c == "+" || c == "-" || c == "*" || c == "/";
    }

    private static int Prec(char c)
    {
        if (c == '-' || c == '+') return 1;
        if (c == '*' || c == '/') return 2;
        return -1;
    }
}