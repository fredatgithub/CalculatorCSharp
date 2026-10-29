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
        Assert.AreEqual(i, calcEngine.GetDisplay());
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
      Assert.AreEqual(9876543210, calcEngine.GetDisplay());
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
      Assert.AreEqual(84.25, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    [Test]
    public void appendNum_multiple()
    {
      clearCalcEngine();
      calcEngine.appendNum(829);
      Assert.AreEqual(829, calcEngine.GetDisplay());
      calcEngine.appendNum(204);
      Assert.AreEqual(829204, calcEngine.GetDisplay());
      clearCalcEngine();
      calcEngine.appendNum(5392.634);
      Assert.AreEqual(5392.634, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    [Test]
    public void appendNum_decimal()
    {
      clearCalcEngine();
      //Try doing two decimals at once
      calcEngine.appendNum(84.523);
      Assert.AreEqual(84.523, calcEngine.GetDisplay());
      calcEngine.appendNum(678.935); //Should replae the other one
      Assert.AreEqual(678.935, calcEngine.GetDisplay());
      clearCalcEngine();
      //Now try agian after doing a normal append
      calcEngine.appendNum(8);
      calcEngine.appendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.appendNum(2);
      calcEngine.appendNum(5);
      Assert.AreEqual(84.25, calcEngine.GetDisplay());
      calcEngine.appendNum(678.935); //Should replae the other one
      Assert.AreEqual(678.935, calcEngine.GetDisplay());
      clearCalcEngine();
      //Now try doing before a normal append (should just append to decimal)
      calcEngine.appendNum(678.935); //Should replae the other one
      Assert.AreEqual(678.935, calcEngine.GetDisplay());
      calcEngine.appendNum(8);
      calcEngine.appendNum(4);
      calcEngine.Other_fcns("decimal");
      calcEngine.appendNum(2);
      calcEngine.appendNum(5);
      Assert.AreEqual(678.9358425, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    public void clearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.IsTrue(calcEngine.AssureCleared());
    }
  }
  [TestFixture]
  public class Test_Memory
  {
    [Test]
    public void clearAll()
    {
      clearCalcEngine();
      calcEngine.appendNum(34.223);
      calcEngine.prepareOperation("+");
      calcEngine.Other_fcns("switchSign");
      clearCalcEngine();
      calcEngine.Other_fcns("open_paren");
      calcEngine.appendNum(34.223);
      calcEngine.prepareOperation("add");
      clearCalcEngine();
    }
    [Test]
    public void storeMemory()
    {
      clearCalcEngine();
      calcEngine.appendNum(526.42);
      calcEngine.Memory("memStore");
      Assert.IsTrue(526.42 - Math.Abs(Convert.ToDouble(calcEngine.m_memory)) < 0.000000001);
      clearCalcEngine();
    }
    [Test]
    public void recallMemory()
    {
      clearCalcEngine();
      calcEngine.appendNum(526.42);
      calcEngine.Memory("memStore");
      clearCalcEngine();
      calcEngine.Memory("memRecall");
      Assert.IsTrue(526.42 - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }
    [Test]
    public void sumToMemory()
    {
      clearCalcEngine();
      calcEngine.appendNum(526.42);
      calcEngine.Memory("memStore");
      clearCalcEngine();
      calcEngine.Memory("memRecall");
      Assert.IsTrue(526.42 - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      calcEngine.appendNum(35.56);
      calcEngine.Memory("memAdd");
      Assert.IsTrue((526.42 + 35.56) - (Convert.ToDouble(calcEngine.m_memory) + calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }
    [Test]
    public void clearMemory()
    {
      clearCalcEngine();
      calcEngine.appendNum(526.42);
      calcEngine.Memory("memStore");
      clearCalcEngine();
      Assert.IsTrue(526.42 - Math.Abs(Convert.ToDouble(calcEngine.m_memory)) < 0.000000001);
      calcEngine.Memory("memClear");
      Assert.IsNull(calcEngine.m_memory);
      clearCalcEngine();
    }
    public void clearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.IsTrue(calcEngine.AssureCleared());
    }
  }
  [TestFixture]
  public class Test_TrigFunctions
  {

    [Test]
    public void trig_SIN()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.IsTrue(calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("sin");
      Assert.IsTrue(Math.Sin(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.IsFalse(calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("sin");
      Assert.IsTrue(Math.Sin(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }
    [Test]
    public void trig_COS()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.IsTrue(calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("cos");
      Assert.IsTrue(Math.Cos(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.IsFalse(calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("cos");
      Assert.IsTrue(Math.Cos(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }
    [Test]
    public void trig_TAN()
    {
      clearCalcEngine();
      calcEngine.m_useRadians = true;
      Assert.IsTrue(calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("tan");
      Assert.IsTrue(Math.Tan(55) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      clearCalcEngine();
      calcEngine.m_useRadians = false;
      Assert.IsFalse(calcEngine.m_useRadians);
      calcEngine.appendNum(55);
      calcEngine.trig_fcns("tan");
      Assert.IsTrue(Math.Tan(55 * Math.PI / 180) - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }
    [Test]
    public void trig_PI()
    {
      clearCalcEngine();
      calcEngine.trig_fcns("pi");
      Assert.IsTrue(Math.PI - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
      calcEngine.appendNum(234);
      Assert.IsTrue(Math.PI - Math.Abs(calcEngine.GetDisplay()) < 0.000000001);
      clearCalcEngine();
    }
    public void clearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.IsTrue(calcEngine.AssureCleared());
    }
  }
  [TestFixture]
  public class Test_MathFunctions
  {
    //Tests both postive and negative numbers for math functions
    //Tests consecutive presses of the solve button
    //Tests a change of operation to anything besides current one
    [Test]
    public void math_ADD()
    {
      clearCalcEngine();
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(17);
      calcEngine.Solve();
      Assert.AreEqual(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(117, calcEngine.GetDisplay());
      //If you don't hit the add button again after hitting '=', then it assumes you're typing a new number
      calcEngine.prepareOperation("add");
      //Now you may append a numbers
      calcEngine.appendNum(3);
      calcEngine.prepareOperation("subtract");
      //Make sure display got updated after changing operation
      Assert.AreEqual(120, calcEngine.GetDisplay());
      clearCalcEngine();
      //Now try every combination of negative values
      //-+
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(13);
      calcEngine.Solve();
      Assert.AreEqual(-70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-57, calcEngine.GetDisplay());
      clearCalcEngine();
      //+-
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(-13);
      calcEngine.Solve();
      Assert.AreEqual(70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(57, calcEngine.GetDisplay());
      clearCalcEngine();
      //--
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(-17);
      calcEngine.Solve();
      Assert.AreEqual(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-117, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    [Test]
    public void math_SUBTRACT()
    {
      clearCalcEngine();
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(3);
      calcEngine.Solve();
      Assert.AreEqual(80, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(77, calcEngine.GetDisplay());
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(7);
      calcEngine.prepareOperation("add");
      Assert.AreEqual(70, calcEngine.GetDisplay());
      clearCalcEngine();
      //-+
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(17);
      calcEngine.Solve();
      Assert.AreEqual(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-117, calcEngine.GetDisplay());
      clearCalcEngine();
      //+-
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(-17);
      calcEngine.Solve();
      Assert.AreEqual(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(117, calcEngine.GetDisplay());
      clearCalcEngine();
      //--
      calcEngine.appendNum(-83);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(-13);
      calcEngine.Solve();
      Assert.AreEqual(-70, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-57, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    [Test]
    public void math_MULTIPLY()
    {
      clearCalcEngine();
      calcEngine.appendNum(10);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(5);
      calcEngine.Solve();
      Assert.AreEqual(50, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(250, calcEngine.GetDisplay());
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(2);
      calcEngine.prepareOperation("divide");
      Assert.AreEqual(500, calcEngine.GetDisplay());
      clearCalcEngine();
      //-+
      calcEngine.appendNum(-20);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(5);
      calcEngine.Solve();
      Assert.AreEqual(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-500, calcEngine.GetDisplay());
      clearCalcEngine();
      //+-
      calcEngine.appendNum(20);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(-5);
      calcEngine.Solve();
      Assert.AreEqual(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(500, calcEngine.GetDisplay());
      clearCalcEngine();
      //--
      calcEngine.appendNum(-20);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(-5);
      calcEngine.Solve();
      Assert.AreEqual(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-500, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    [Test]
    public void math_DIVIDE()
    {
      clearCalcEngine();
      calcEngine.appendNum(500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(2);
      calcEngine.Solve();
      Assert.AreEqual(250, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(125, calcEngine.GetDisplay());
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(0.5);
      calcEngine.prepareOperation("multiply");
      Assert.AreEqual(250, calcEngine.GetDisplay());
      clearCalcEngine();
      //-+
      calcEngine.appendNum(-500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(5);
      calcEngine.Solve();
      Assert.AreEqual(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-20, calcEngine.GetDisplay());
      clearCalcEngine();
      //+-
      calcEngine.appendNum(500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(-5);
      calcEngine.Solve();
      Assert.AreEqual(-100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(20, calcEngine.GetDisplay());
      clearCalcEngine();
      //--
      calcEngine.appendNum(-500);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(-5);
      calcEngine.Solve();
      Assert.AreEqual(100, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-20, calcEngine.GetDisplay());
      clearCalcEngine();
      //Check divide by Zero
      calcEngine.appendNum(5);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(0);
      Assert.IsFalse(calcEngine.Solve());
    }
    [Test]
    public void math_DIF_OPERATIONS()
    {
      clearCalcEngine();
      calcEngine.appendNum(3);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(5);
      calcEngine.prepareOperation("multiply");
      Assert.AreEqual(8, calcEngine.GetDisplay());
      calcEngine.appendNum(3);
      calcEngine.Solve();
      Assert.AreEqual(24, calcEngine.GetDisplay());
      calcEngine.prepareOperation("divide");
      Assert.AreEqual(24, calcEngine.GetDisplay());
      calcEngine.appendNum(6);
      calcEngine.prepareOperation("subtract");
      Assert.AreEqual(4, calcEngine.GetDisplay());
      calcEngine.appendNum(1.5);
      calcEngine.Solve();
      Assert.AreEqual(2.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(1, calcEngine.GetDisplay());
      calcEngine.prepareOperation("add");
      Assert.AreEqual(1, calcEngine.GetDisplay());
      calcEngine.appendNum(-3);
      calcEngine.Solve();
      Assert.AreEqual(-2, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(-5, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    public void clearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.IsTrue(calcEngine.AssureCleared());
    }
  }
  [TestFixture]
  public class Test_MiscFunctions
  {
    [Test]
    public void misc_SQRT()
    {
      clearCalcEngine();
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("sqrt");
      Assert.IsTrue(5 == calcEngine.GetDisplay());
      calcEngine.Other_fcns("sqrt");
      Assert.IsTrue(Math.Sqrt(5) - Math.Abs(calcEngine.GetDisplay()) < 0.0000001);
      clearCalcEngine();
      calcEngine.appendNum(-3);
      Assert.IsFalse(calcEngine.Other_fcns("sqrt"));
      clearCalcEngine();
    }
    [Test]
    public void misc_PERCENT()
    {
      //(+)
      clearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.AreEqual(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(62.5, calcEngine.GetDisplay());
      //(-)
      clearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("subtract");
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.AreEqual(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(37.5, calcEngine.GetDisplay());
      //(*)
      clearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("multiply");
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.AreEqual(12.5, calcEngine.GetDisplay());
      calcEngine.Other_fcns("percent");
      Assert.AreEqual(6.25, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(312.5, calcEngine.GetDisplay());
      //(/)
      clearCalcEngine();
      calcEngine.appendNum(50);
      calcEngine.prepareOperation("divide");
      calcEngine.appendNum(25);
      calcEngine.Other_fcns("percent");
      Assert.AreEqual(12.5, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(4, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    [Test]
    public void misc_INVERSE()
    {
      clearCalcEngine();
      calcEngine.appendNum(5);
      calcEngine.Other_fcns("inverse");
      Assert.IsTrue(1 / 5 - Math.Abs(calcEngine.GetDisplay()) < 0.0000001);
      clearCalcEngine();
      calcEngine.appendNum(0);
      Assert.IsFalse(calcEngine.Other_fcns("inverse"));
      clearCalcEngine();
    }
    [Test]
    public void misc_BACKSPACE()
    {
      clearCalcEngine();
      calcEngine.appendNum(45352.743);
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(45352.74, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(45352.7, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(45352, calcEngine.GetDisplay());
      Assert.IsTrue(calcEngine.m_decimal);
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(45352, calcEngine.GetDisplay());
      Assert.IsFalse(calcEngine.m_decimal);
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(4535, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(453, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(45, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(4, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.AreEqual(0, calcEngine.GetDisplay());
      calcEngine.Other_fcns("backspace");
      Assert.IsFalse(calcEngine.Other_fcns("backspace"));
      clearCalcEngine();
    }
    [Test]
    public void misc_PARENTHESES()
    {
      clearCalcEngine();
      calcEngine.Other_fcns("open_paren");
      Assert.IsTrue(calcEngine.m_openParen);
      Assert.IsFalse(calcEngine.m_closeParen);
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
      Assert.IsTrue(calcEngine.m_closeParen);
      Assert.IsFalse(calcEngine.m_openParen);
      Assert.AreEqual(-40, calcEngine.GetDisplay());
      //Try Again
      calcEngine.Other_fcns("open_paren");
      Assert.IsTrue(calcEngine.m_openParen);
      Assert.IsFalse(calcEngine.m_closeParen);
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
      Assert.IsTrue(calcEngine.m_closeParen);
      Assert.IsFalse(calcEngine.m_openParen);
      Assert.AreEqual(-40, calcEngine.GetDisplay());
      clearCalcEngine();
    }
    [Test]
    public void misc_clearEntry()
    {
      clearCalcEngine();
      calcEngine.appendNum(83);
      calcEngine.prepareOperation("add");
      calcEngine.appendNum(17);
      calcEngine.clear();
      calcEngine.appendNum(27); //Chagne 17 to 27
      calcEngine.Solve();
      Assert.AreEqual(110, calcEngine.GetDisplay());
      calcEngine.Solve();
      Assert.AreEqual(137, calcEngine.GetDisplay());
      clearCalcEngine();
    }

    public void clearCalcEngine()
    {
      calcEngine.clearAll();
      Assert.IsTrue(calcEngine.AssureCleared());
    }
  }
}
