using System;
using System.Collections;

using FluorineFx;

[RemotingService]
public class Simple
{

    public double addNumbers(double n1, double n2)
    {
        return n1 + n2;
    }

    public object echoData(object data)
    {
        return data;
    }

    public object echoData2(ArrayList data)
    {
        return data;
    }

}

public class TestDataTypes
{
    public TestDataTypes()
    {
    }

    public bool testTrue()
    {
        return true;
    }

    public bool testFalse()
    {
        return false;
    }

	public string[] testArray()
	{
		return new string[]{"It", "works"};
	}

	public object[] testNestedArray()
	{
        return new object[] { "It", new string[] { "also" }, "works" };
	}

    public int testInteger()
    {
        return 42;
    }


    public double testFloat()
    {
        return 42.12345;
    }

    public string testString()
    {
        return "It works!";
    }

	public Hashtable testAssociativeArray()
	{
        Hashtable result = new Hashtable();
        result.Add("it", "works");
        result.Add("number", 42);
		return result;
	}

    public TestTO testObject()
    {
        return new TestTO();
    }

    public object testNull()
    {
        return null;
    }
}

public class TestTO
{
	public string prop1 = "A string";
	public int prop2 = 42;
}
