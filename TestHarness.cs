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
        calcEngine.appendNum(i);
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
        calcEngine.appendNum(i);
      }
      Assert.Equals(9876543210, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    [Test]
    public void AppendDigit_decimal()
    {
      clearCalcEngine();
      calcEngine.appendNum(8);
      calcEngine.appendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.appendNum(2);
      calcEngine.appendNum(5);
      Assert.Equals(84.25, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    [Test]
    public void appendNum_multiple()
    {
      clearCalcEngine();
      calcEngine.appendNum(829);
      Assert.Equals(829, calcEngine.GetDisplay());
      calcEngine.appendNum(204);
      Assert.Equals(829204, calcEngine.GetDisplay());
      clearCalcEngine();
      calcEngine.appendNum(5392.634);
      Assert.Equals(5392.634, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    [Test]
    public void appendNum_decimal()
    {
      clearCalcEngine();
      //Try doing two decimals at once
      calcEngine.appendNum(84.523);
      Assert.Equals(84.523, calcEngine.GetDisplay());
      calcEngine.appendNum(678.935); //Should replae the other one
      Assert.Equals(678.935, calcEngine.GetDisplay());
      clearCalcEngine();
      //Now try agian after doing a normal append
      calcEngine.appendNum(8);
      calcEngine.appendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.appendNum(2);
      calcEngine.appendNum(5);
      Assert.Equals(84.25, calcEngine.GetDisplay());
      calcEngine.appendNum(678.935); //Should replae the other one
      Assert.Equals(678.935, calcEngine.GetDisplay());
      clearCalcEngine();
      //Now try doing before a normal append (should just append to decimal)
      calcEngine.appendNum(678.935); //Should replae the other one
      Assert.Equals(678.935, calcEngine.GetDisplay());
      calcEngine.appendNum(8);
      calcEngine.appendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.appendNum(2);
      calcEngine.appendNum(5);
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
      calcEngine.appendNum(34.223);
      calcEngine.prepareOperation("+");
      calcEngine.Other_fcns("switchSign");
      ClearCalcEngine();
      calcEngine.Other_fcns("open_paren");
      calcEngine.appendNum(34.223);
      calcEngine.prepareOperation("add");
      ClearCalcEngine();
    }

    [Test]
    public void StoreMemory()
    {
      ClearCalcEngine();
      calcEngine.appendNum(526.42);
      calcEngine.Memory("memStore");
      Assert.Equals(true, 526.42 - Math.Abs(Convert.ToDouble(calcEngine.m_memory)) < 0.000000001);
      ClearCalcEngine();
    }

    [Test]
    public void RecallMemory()
    {
      ClearCalcEngine();
      calcEngine.appendNum(526.42);
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
      calcEngine.appendNum(526.42);
      calcEngine.Memory("memStore");
      ClearCalcEngine();
      calcEngine.Memory("memRecall");
      Assert.Equals(true, 526.42 - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      ClearCalcEngine();
      calcEngine.appendNum(35.56);
      calcEngine.Memory("memAdd");
      Assert.Equals(true, (526.42 + 35.56) - (Convert.ToDouble(calcEngine.m_memory) + calcEngine.GetDisplay()) < 0.000000001);
      ClearCalcEngine();
    }

    [Test]
    public void ClearMemory()
    {
      ClearCalcEngine();
      calcEngine.appendNum(526.42);
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
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("sin");
      Assert.Equals(true, Math.Sin(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.Equals(false, calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("sin");
      Assert.Equals(true, Math.Sin(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }

    [Test]
    public void Trig_COS()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.Equals(true, calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("cos");
      Assert.Equals(true, Math.Cos(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.Equals(false, calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("cos");
      Assert.Equals(true, Math.Cos(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }

    [Test]
    public void Trig_TAN()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.Equals(true, calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("tan");
      Assert.Equals(true, Math.Tan(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.Equals(false, calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("tan");
      Assert.Equals(true, Math.Tan(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }

    [Test]
    public void Trig_PI()
    {
      clearCalcEngine();
      calcEngine.trig_fcns("pi");
      Assert.Equals(true, Math.PI - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      calcEngine.appendNum(234);
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
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(17);
      calcEngine.Solve();
      Assert.Equals(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(117, calcEngine.GetDisplay());
      //If you don't hit the add button again after hitting '=', then it assumes you're typing a new number
      calcEngine.prepareOperation("add");
      //Now you may append a numbers
      calcEngine.appendNum(3);
      calcEngine.prepareOperation("subtract");
      //Make sure display got updated after changing operation
      Assert.Equals(120, calcEngine.GetDisplay());
      ClearCalcEngine();
      //Now try every combination of negative values
      //-+
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(13);
      calcEngine.Solve();
      Assert.Equals(-70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-57, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(-13);
      calcEngine.Solve();
      Assert.Equals(70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(57, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(-17);
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
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(3);
      calcEngine.Solve();
      Assert.Equals(80, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(77, calcEngine.GetDisplay());
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(7);
      calcEngine.prepareOperation("add");
      Assert.Equals(70, calcEngine.GetDisplay());
      ClearCalcEngine();
      //-+
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(17);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-117, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(-17);
      calcEngine.Solve();
      Assert.Equals(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(117, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(-13);
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
      calcEngine.appendNum(10);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(5);
      calcEngine.Solve();
      Assert.Equals(50, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(250, calcEngine.GetDisplay());
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(2);
      calcEngine.prepareOperation("divide");
      Assert.Equals(500, calcEngine.GetDisplay());
      ClearCalcEngine();
      //-+
      calcEngine.appendNum(-20);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-500, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.appendNum(20);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(-5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(500, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.appendNum(-20);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(-5);
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
      calcEngine.appendNum(500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(2);
      calcEngine.Solve();
      Assert.Equals(250, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(125, calcEngine.GetDisplay());
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(0.5);
      calcEngine.prepareOperation("multiply");
      Assert.Equals(250, calcEngine.GetDisplay());
      ClearCalcEngine();
      //-+
      calcEngine.appendNum(-500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-20, calcEngine.GetDisplay());
      ClearCalcEngine();
      //+-
      calcEngine.appendNum(500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(-5);
      calcEngine.Solve();
      Assert.Equals(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(20, calcEngine.GetDisplay());
      ClearCalcEngine();
      //--
      calcEngine.appendNum(-500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(-5);
      calcEngine.Solve();
      Assert.Equals(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(-20, calcEngine.GetDisplay());
      ClearCalcEngine();
      //Check divide by Zero
      calcEngine.appendNum(5);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(0);
      Assert.Equals(false, calcEngine.Solve());
    }

    [Test]
    public void Math_DIF_OPERATIONS()
    {
      ClearCalcEngine();
      calcEngine.appendNum(3);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(5);
      calcEngine.prepareOperation("multiply");
      Assert.Equals(8, calcEngine.GetDisplay());
      calcEngine.appendNum(3);
      calcEngine.Solve();
      Assert.Equals(24, calcEngine.GetDisplay());
      calcEngine.prepareOperation("divide");
      Assert.Equals(24, calcEngine.GetDisplay());
      calcEngine.appendNum(6);
      calcEngine.prepareOperation("subtract");
      Assert.Equals(4, calcEngine.GetDisplay());
      calcEngine.appendNum(1.5);
      calcEngine.Solve();
      Assert.Equals(2.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(1, calcEngine.GetDisplay());
      calcEngine.prepareOperation("add");
      Assert.Equals(1, calcEngine.GetDisplay());
      calcEngine.appendNum(-3);
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
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("sqrt");
      Assert.Equals(5, calcEngine.GetDisplay());
      calcEngine.Other_fcns("sqrt");
      Assert.Equals(Math.Sqrt(5), calcEngine.GetDisplay());
      ClearCalcEngine();
      calcEngine.appendNum(-3);
      Assert.Equals(false, calcEngine.Other_fcns("sqrt"));
      ClearCalcEngine();
    }

    [Test]
    public void Misc_PERCENT()
    {
      //(+)
      ClearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.Equals(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(62.5, calcEngine.GetDisplay());
      //(-)
      ClearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.Equals(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(37.5, calcEngine.GetDisplay());
      //(*)
      ClearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.Equals(12.5, calcEngine.GetDisplay());
      calcEngine.Other_fcns("percent");
      Assert.Equals(6.25, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.Equals(312.5, calcEngine.GetDisplay());
      //(/)
      ClearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(25);
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
      calcEngine.appendNum(5);
      calcEngine.Other_fcns("inverse");
      Assert.Equals(1 / 5, calcEngine.GetDisplay());
      ClearCalcEngine();
      calcEngine.appendNum(0);
      Assert.Equals(false, calcEngine.Other_fcns("inverse"));
      ClearCalcEngine();
    }

    [Test]
    public void Misc_BACKSPACE()
    {
      ClearCalcEngine();
      calcEngine.appendNum(45352.743);
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
      calcEngine.appendNum(1);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(-2);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(88);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(54);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(36);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(21);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(2);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(-13);
      calcEngine.Other_fcns("close_paren");
      Assert.Equals(true, calcEngine.m_closeParen);
      Assert.Equals(false, calcEngine.m_openParen);
      Assert.Equals(-40, calcEngine.GetDisplay());
      //Try Again
      calcEngine.Other_fcns("open_paren");
      Assert.Equals(true, calcEngine.m_openParen);
      Assert.Equals(false, calcEngine.m_closeParen);
      calcEngine.appendNum(1);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(-2);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(88);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(54);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(36);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(21);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(2);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(-13);
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
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(17);
      calcEngine.clear();
      calcEngine.appendNum(27); //Chagne 17 to 27
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
