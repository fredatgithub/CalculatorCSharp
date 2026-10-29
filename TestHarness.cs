using System;
using NUnit.Framework;

namespace Calculator
{
  //This class contains all the test cases I made for NUnit
  //If you have NUnit installed you can debug easily by seeing if any test cases become broken

  [TestFixture]
  public class Test_Appending
  {
    [Test]
    public void AppendDigit_0_9()
    {
      clearCalcEngine();
      for (int i = 9; i >= 0; i--)
      {
        calcEngine.AppendNum(i);
        Assert.Equals(i, calcEngine.GetDisplay());
        clearCalcEngine();
      }
    }

    [Test]
    public void AppendDigit_multiple()
    {
      clearCalcEngine();
      for (int i = 9; i >= 0; i--)
      {
        calcEngine.AppendNum(i);
      }
      Assert.Equals(9876543210, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    [Test]
    public void AppendDigit_decimal()
    {
      clearCalcEngine();
      calcEngine.AppendNum(8);
      calcEngine.AppendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.AppendNum(2);
      calcEngine.AppendNum(5);
      Assert.Equals(84.25, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    [Test]
    public void appendNum_multiple()
    {
      clearCalcEngine();
      calcEngine.AppendNum(829);
      Assert.Equals(829, calcEngine.GetDisplay());
      calcEngine.AppendNum(204);
      Assert.Equals(829204, calcEngine.GetDisplay());
      clearCalcEngine();
      calcEngine.AppendNum(5392.634);
      Assert.Equals(5392.634, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    [Test]
    public void appendNum_decimal()
    {
      clearCalcEngine();
      //Try doing two decimals at once
      calcEngine.AppendNum(84.523);
      Assert.Equals(84.523, calcEngine.GetDisplay());
      calcEngine.AppendNum(678.935); //Should replae the other one
      Assert.Equals(678.935, calcEngine.GetDisplay());
      clearCalcEngine();
      //Now try agian after doing a normal append
      calcEngine.AppendNum(8);
      calcEngine.AppendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.AppendNum(2);
      calcEngine.AppendNum(5);
      Assert.Equals(84.25, calcEngine.GetDisplay());
      calcEngine.AppendNum(678.935); //Should replae the other one
      Assert.Equals(678.935, calcEngine.GetDisplay());
      clearCalcEngine();
      //Now try doing before a normal append (should just append to decimal)
      calcEngine.AppendNum(678.935); //Should replae the other one
      Assert.Equals(678.935, calcEngine.GetDisplay());
      calcEngine.AppendNum(8);
      calcEngine.AppendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.AppendNum(2);
      calcEngine.AppendNum(5);
      Assert.Equals(678.9358425, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    public void clearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.Equals(true, calcEngine.AssureCleared());
    }
  }

  [TestFixture]
  public class Test_Memory
  {
    [Test]
    public void ClearAll()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(34.223);
      calcEngine.PrepareOperation("+");
      calcEngine.Other_fcns("switchSign");
      ClearCalcEngine();
      calcEngine.Other_fcns("open_paren");
      calcEngine.AppendNum(34.223);
      calcEngine.PrepareOperation("add");
      ClearCalcEngine();
    }

    [Test]
    public void StoreMemory()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(526.42);
      calcEngine.Memory("memStore");
      Assert.Equals(true, 526.42 - Math.Abs(Convert.ToDouble(calcEngine.m_memory)) < 0.000000001);
      ClearCalcEngine();
    }

    [Test]
    public void RecallMemory()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(526.42);
      calcEngine.Memory("memStore");
      ClearCalcEngine();
      calcEngine.Memory("memRecall");
      Assert.Equals(true, 526.42 - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      ClearCalcEngine();
    }

    [Test]
    public void SumToMemory()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(526.42);
      calcEngine.Memory("memStore");
      ClearCalcEngine();
      calcEngine.Memory("memRecall");
      Assert.Equals(true, 526.42 - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      ClearCalcEngine();
      calcEngine.AppendNum(35.56);
      calcEngine.Memory("memAdd");
      Assert.Equals(true, (526.42 + 35.56) - (Convert.ToDouble(calcEngine.m_memory) + calcEngine.GetDisplay()) < 0.000000001);
      ClearCalcEngine();
    }

    [Test]
    public void ClearMemory()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(526.42);
      calcEngine.Memory("memStore");
      ClearCalcEngine();
      Assert.Equals(true, 526.42 - Math.Abs(Convert.ToDouble(calcEngine.m_memory)) < 0.000000001);
      calcEngine.Memory("memClear");
      Assert.Equals(true, calcEngine.m_memory == null);
      ClearCalcEngine();
    }

    public void ClearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.Equals(true, calcEngine.AssureCleared());
    }
  }

  [TestFixture]
  public class Test_TrigFunctions
  {
    [Test]
    public void Trig_SIN()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.Equals(true, calcEngine.m_useRadians);
      calcEngine.AppendNum(55);
      calcEngine.Trig_fcns("sin");
      Assert.Equals(true, Math.Sin(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.Equals(false, calcEngine.m_useRadians);
      calcEngine.AppendNum(55);
      calcEngine.Trig_fcns("sin");
      Assert.Equals(true, Math.Sin(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }

    [Test]
    public void Trig_COS()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.Equals(true, calcEngine.m_useRadians);
      calcEngine.AppendNum(55);
      calcEngine.Trig_fcns("cos");
      Assert.Equals(true, Math.Cos(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.Equals(false, calcEngine.m_useRadians);
      calcEngine.AppendNum(55);
      calcEngine.Trig_fcns("cos");
      Assert.Equals(true, Math.Cos(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }

    [Test]
    public void Trig_TAN()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.Equals(true, calcEngine.m_useRadians);
      calcEngine.AppendNum(55);
      calcEngine.Trig_fcns("tan");
      Assert.Equals(true, Math.Tan(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.Equals(false, calcEngine.m_useRadians);
      calcEngine.AppendNum(55);
      calcEngine.Trig_fcns("tan");
      Assert.Equals(true, Math.Tan(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }

    [Test]
    public void Trig_PI()
    {
      clearCalcEngine();
      calcEngine.Trig_fcns("pi");
      Assert.Equals(true, Math.PI - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      calcEngine.AppendNum(234);
      Assert.Equals(true, Math.PI - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }

    public void clearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.Equals(true, calcEngine.AssureCleared());
    }
  }

  [TestFixture]
  public class Test_MathFunctions
  {
    //Tests both postive and negative numbers for math functions
    //Tests consecutive presses of the solve button
    //Tests a change of operation to anything besides current one
    [Test]
    public void Math_ADD()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(83);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(17);
      calcEngine.Solve();
      Assert.Equals(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(117, calcEngine.GetDisplay());
      //If you don't hit the add button again after hitting '=', then it assumes you're typing a new number
      calcEngine.PrepareOperation("add");
      //Now you may append a numbers
      calcEngine.AppendNum(3);
      calcEngine.PrepareOperation("subtract");
      //Make sure display got updated after changing operation
      Assert.Equals(120, calcEngine.GetDisplay());
      ClearCalcEngine();
      //Now try every combination of negative values
      //-+
      calcEngine.AppendNum(-83);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(13);
      calcEngine.Solve();
      Assert.Equals(-70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-57, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.AppendNum(83);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(-13);
      calcEngine.Solve();
      Assert.Equals(70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(57, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.AppendNum(-83);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(-17);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-117, calcEngine.GetDisplay());
      ClearCalcEngine();
    }

    [Test]
    public void Math_SUBTRACT()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(83);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(3);
      calcEngine.Solve();
      Assert.Equals(80, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(77, calcEngine.GetDisplay());
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(7);
      calcEngine.PrepareOperation("add");
      Assert.Equals(70, calcEngine.GetDisplay());
      ClearCalcEngine();
      //-+
      calcEngine.AppendNum(-83);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(17);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-117, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.AppendNum(83);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(-17);
      calcEngine.Solve();
      Assert.Equals(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(117, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.AppendNum(-83);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(-13);
      calcEngine.Solve();
      Assert.Equals(-70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-57, calcEngine.GetDisplay());
      ClearCalcEngine();
    }

    [Test]
    public void math_MULTIPLY()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(10);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(5);
      calcEngine.Solve();
      Assert.Equals(50, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(250, calcEngine.GetDisplay());
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(2);
      calcEngine.PrepareOperation("divide");
      Assert.Equals(500, calcEngine.GetDisplay());
      ClearCalcEngine();
      //-+
      calcEngine.AppendNum(-20);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-500, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.AppendNum(20);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(-5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(500, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.AppendNum(-20);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(-5);
      calcEngine.Solve();
      Assert.Equals(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-500, calcEngine.GetDisplay());
      ClearCalcEngine();
    }

    [Test]
    public void Math_DIVIDE()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(500);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(2);
      calcEngine.Solve();
      Assert.Equals(250, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(125, calcEngine.GetDisplay());
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(0.5);
      calcEngine.PrepareOperation("multiply");
      Assert.Equals(250, calcEngine.GetDisplay());
      ClearCalcEngine();
      //-+
      calcEngine.AppendNum(-500);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-20, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.AppendNum(500);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(-5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(20, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.AppendNum(-500);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(-5);
      calcEngine.Solve();
      Assert.Equals(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-20, calcEngine.GetDisplay());
      ClearCalcEngine();
      //Check divide by Zero
      calcEngine.AppendNum(5);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(0);
      Assert.Equals(false, calcEngine.Solve());
    }

    [Test]
    public void Math_DIF_OPERATIONS()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(3);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(5);
      calcEngine.PrepareOperation("multiply");
      Assert.Equals(8, calcEngine.GetDisplay());
      calcEngine.AppendNum(3);
      calcEngine.Solve();
      Assert.Equals(24, calcEngine.GetDisplay());
      calcEngine.PrepareOperation("divide");
      Assert.Equals(24, calcEngine.GetDisplay());
      calcEngine.AppendNum(6);
      calcEngine.PrepareOperation("subtract");
      Assert.Equals(4, calcEngine.GetDisplay());
      calcEngine.AppendNum(1.5);
      calcEngine.Solve();
      Assert.Equals(2.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(1, calcEngine.GetDisplay());
      calcEngine.PrepareOperation("add");
      Assert.Equals(1, calcEngine.GetDisplay());
      calcEngine.AppendNum(-3);
      calcEngine.Solve();
      Assert.Equals(-2, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-5, calcEngine.GetDisplay());
      ClearCalcEngine();
    }

    public void ClearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.Equals(true, calcEngine.AssureCleared());
    }
  }

  [TestFixture]
  public class Test_MiscFunctions
  {
    [Test]
    public void Misc_SQRT()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(25);
      calcEngine.Other_fcns("sqrt");
      Assert.Equals(5, calcEngine.GetDisplay());
      calcEngine.Other_fcns("sqrt");
      Assert.Equals(Math.Sqrt(5), calcEngine.GetDisplay());
      ClearCalcEngine();
      calcEngine.AppendNum(-3);
      Assert.Equals(false, calcEngine.Other_fcns("sqrt"));
      ClearCalcEngine();
    }

    [Test]
    public void Misc_PERCENT()
    {
      //(+)
      ClearCalcEngine();
      calcEngine.AppendNum(50);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.Equals(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(62.5, calcEngine.GetDisplay());
      //(-)
      ClearCalcEngine();
      calcEngine.AppendNum(50);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.Equals(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(37.5, calcEngine.GetDisplay());
      //(*)
      ClearCalcEngine();
      calcEngine.AppendNum(50);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.Equals(12.5, calcEngine.GetDisplay());
      calcEngine.Other_fcns("percent");
      Assert.Equals(6.25, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(312.5, calcEngine.GetDisplay());
      //(/)
      ClearCalcEngine();
      calcEngine.AppendNum(50);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.Equals(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(4, calcEngine.GetDisplay());
      ClearCalcEngine();
    }

    [Test]
    public void Misc_INVERSE()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(5);
      calcEngine.Other_fcns("inverse");
      Assert.Equals(1 / 5, calcEngine.GetDisplay());
      ClearCalcEngine();
      calcEngine.AppendNum(0);
      Assert.Equals(false, calcEngine.Other_fcns("inverse"));
      ClearCalcEngine();
    }

    [Test]
    public void Misc_BACKSPACE()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(45352.743);
      calcEngine.Other_fcns("backspace");
      Assert.Equals(45352.74, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.Equals(45352.7, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.Equals(45352, calcEngine.GetDisplay());
      Assert.Equals(true, calcEngine.m_decimal);
      calcEngine.Other_fcns("backspace");
      Assert.Equals(45352, calcEngine.GetDisplay());
      Assert.Equals(false, calcEngine.m_decimal);
      calcEngine.Other_fcns("backspace");
      Assert.Equals(4535, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.Equals(453, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.Equals(45, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.Equals(4, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.Equals(0, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.Equals(false, calcEngine.Other_fcns("backspace"));
      ClearCalcEngine();
    }

    [Test]
    public void Misc_PARENTHESES()
    {
      ClearCalcEngine();
      calcEngine.Other_fcns("open_paren");
      Assert.Equals(true, calcEngine.m_openParen);
      Assert.Equals(false, calcEngine.m_closeParen);
      calcEngine.AppendNum(1);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(-2);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(88);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(54);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(36);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(21);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(2);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(-13);
      calcEngine.Other_fcns("close_paren");
      Assert.Equals(true, calcEngine.m_closeParen);
      Assert.Equals(false, calcEngine.m_openParen);
      Assert.Equals(-40, calcEngine.GetDisplay());
      //Try Again
      calcEngine.Other_fcns("open_paren");
      Assert.Equals(true, calcEngine.m_openParen);
      Assert.Equals(false, calcEngine.m_closeParen);
      calcEngine.AppendNum(1);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(-2);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(88);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(54);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(36);
      calcEngine.PrepareOperation("subtract");
      calcEngine.AppendNum(21);
      calcEngine.PrepareOperation("divide");
      calcEngine.AppendNum(2);
      calcEngine.PrepareOperation("multiply");
      calcEngine.AppendNum(-13);
      calcEngine.Other_fcns("close_paren");
      Assert.Equals(true, calcEngine.m_closeParen);
      Assert.Equals(false, calcEngine.m_openParen);
      Assert.Equals(-40, calcEngine.GetDisplay());
      ClearCalcEngine();
    }

    [Test]
    public void Misc_clearEntry()
    {
      ClearCalcEngine();
      calcEngine.AppendNum(83);
      calcEngine.PrepareOperation("add");
      calcEngine.AppendNum(17);
      calcEngine.clear();
      calcEngine.AppendNum(27); //Chagne 17 to 27
      calcEngine.Solve();
      Assert.Equals(110, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(137, calcEngine.GetDisplay());
      ClearCalcEngine();
    }

    public void ClearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.Equals(true, calcEngine.AssureCleared());
    }
  }
}
