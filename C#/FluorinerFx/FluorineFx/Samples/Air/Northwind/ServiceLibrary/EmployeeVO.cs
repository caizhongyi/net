using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary
{
    public class EmployeeVO
    {
        int _employeeID;

        public int id
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }
        string _lastName;

        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        string _firstName;

        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        string _title;

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
        string _titleOfCourtesy;

        public string titleOfCourtesy
        {
            get { return _titleOfCourtesy; }
            set { _titleOfCourtesy = value; }
        }
        DateTime? _birthDate;

        public DateTime? birthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }
        DateTime? _hireDate;

        public DateTime? hireDate
        {
            get { return _hireDate; }
            set { _hireDate = value; }
        }
        string _address;

        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        string _city;

        public string city
        {
            get { return _city; }
            set { _city = value; }
        }
        string _region;

        public string region
        {
            get { return _region; }
            set { _region = value; }
        }
        string _postalCode;

        public string postalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }
        string _country;

        public string country
        {
            get { return _country; }
            set { _country = value; }
        }
        string _homePhone;

        public string phone
        {
            get { return _homePhone; }
            set { _homePhone = value; }
        }
        string _extension;

        public string extension
        {
            get { return _extension; }
            set { _extension = value; }
        }
        string _photo;

        public string photo
        {
            get { return _photo; }
            set { _photo = value; }
        }
        string _notes;

        public string notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        int? _reportsTo;

        public int? reportsTo
        {
            get { return _reportsTo; }
            set { _reportsTo = value; }
        }
    }
}
