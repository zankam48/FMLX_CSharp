using System;
using System.Collections.Generic;
using System.Text;

class Program {
  static void Main(string[] args) {
    GeneratorLogic gl = new GeneratorLogic(50);
    gl.AddRule(3, "rizz");
    gl.AddRule(5, "sigma");
    gl.AddRule(7, "skibidi");
    gl.AddRule(8, "ohio");
    gl.AddRule(41, "fanum tax");
    gl.PrintLogic();
  }
}

public class GeneratorLogic {
  private int _iterations;
  public Dictionary < int, string > rules = new Dictionary < int, string > ();
  public GeneratorLogic(int iter) {
    _iterations = iter;
  }
  public void AddRule(int input, string output) {
    rules.Add(input, output);
  }
  public void PrintLogic() {

    for (int i = 1; i <= _iterations; i++) {
      StringBuilder result = new StringBuilder("");
      foreach(var rule in rules) {
        if (i % rule.Key == 0) result.Append(rule.Value);
      }
      if (result.Length == 0) result.Append(i);
      Console.Write(result);
      if (i < _iterations) Console.Write(" ,");
    }
  }
}
