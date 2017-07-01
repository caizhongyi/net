using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using FluorineFx;
using FluorineFx.AMF3;

namespace ServiceLibrary
{
    public enum PrintEnum
    {
        Portrait = 0,
        Landscape
    }

    public class TestVO
    {
        public int intField1;
        
        private int _intField2;
        public int intField2
        {
            get { return _intField2; }
            set { _intField2 = value; }
        }
        public short shortField1;
        public short shortField2;
        public double doubleField1;
        public decimal decimalField1;
        public byte byteField1;
        public bool boolField1;
        public char charField1;
        public PrintEnum enumField1;
        public PrintEnum enumField2;
        public DateTime dateField1;
        public DateTime dateField2;
        public string stringField1;
        public string stringField2;
        public Guid guidField1;
        public XmlDocument xmlField1;
        public XmlDocument xmlField2;
        public double[] arrayField1;
        public double[] arrayField2;
        public string[] arrayField3;
        public Decimal[] arrayField4;
        public object[] arrayField5;
        public byte[] arrayField6;
        public uint[] arrayField7;
        public ArrayList arrayListField1;
        public List<int> listField1;
        public Hashtable dictionaryField1;
        public ASObject asoField1;
        public Dictionary<string, int> dictionaryField2;
        public ArrayCollection arrayCollectionField1;

        //These fields would make the class not optimizable
        //public int? intField3;
        //public int?[] arrayField8;
        //public List<int?> listField2;

    }
}