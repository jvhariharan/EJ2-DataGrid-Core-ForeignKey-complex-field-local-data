using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EJ2Grid.Models
{
    public class Country
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryName { get; set; }
        public int OrderID { get; set; }
    }
    public class Complex
    {
        public int EmployeeID { get; set; }
        public Country Country { get; set; }
        public static List<Complex> GetData()
        {
            List<Complex> Data = new List<Complex>();
            Data.Add(new Complex() { EmployeeID = 10001, Country = new Country() { CountryName = "Australia", FirstName = "ANATR", LastName = "HANAR", OrderID = 10248 } });
            Data.Add(new Complex() { EmployeeID = 10002, Country = new Country() { CountryName = "Bermuda", FirstName = "VINET", LastName = "CHOPS", OrderID = 10249 } });
            Data.Add(new Complex() { EmployeeID = 10003, Country = new Country() { CountryName = "Canada", FirstName = "TOMSP", LastName = "RICSU", OrderID = 10250 } });
            Data.Add(new Complex() { EmployeeID = 10004, Country = new Country() { CountryName = "Cameroon", FirstName = "VICTE", LastName = "ANATR", OrderID = 10251 } });
            Data.Add(new Complex() { EmployeeID = 10005, Country = new Country() { CountryName = "Denmark", FirstName = "SUPRD", LastName = "WELLI", OrderID = 10252 } });
            Data.Add(new Complex() { EmployeeID = 10006, Country = new Country() { CountryName = "France", FirstName = "CENTC", LastName = "HILAA", OrderID = 10253 } });
            return Data;
        }

    }
}