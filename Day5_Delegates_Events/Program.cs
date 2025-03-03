// declare delegatenya paling bawah grgr error top level statement
int Add(int x, int y) => x + y;
int Substract(int x, int y) => x - y;
int Mul(int x, int y) => x * y;
double Divide(double x, double y) => x / y;

Operator add1 = Add;
Operator sub1 = Substract;; 
Operator mul = Mul;
OperatorDouble div1 = Divide;

int result1 = add1(5,10);
int result2 = sub1(5,10);
int result3 = mul(5,10);
double result4 = div1(5,10);
int result1_class = Operation.Operate(5, 10, add1);
int result2_class = Operation.Operate(5, 10, sub1);
int result3_class = Operation.Operate(5, 10, mul);
double result4_class = Operation.OperateDouble(5, 10, div1);

Console.WriteLine(
    $"{result1}, {result2}, {result3}, {result4}\n" + 
    $"Pakai class: {result1_class}, {result2_class}, {result3_class}, {result4_class}");

public class Operation
{
    public static int Operate(int x, int y, Operator opp)
    {
        return opp(x, y);
    }

    public static double OperateDouble(double x, double y, OperatorDouble opp)
    {
        return opp(x, y);
    }
}

public delegate int Operator(int x, int y);
public delegate double OperatorDouble(double x, double y);


