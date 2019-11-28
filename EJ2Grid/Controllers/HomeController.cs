using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EJ2Grid.Models;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace EJ2Grid.Controllers
{
    public class HomeController : Controller
    {
        public static List<Orders> order = new List<Orders>();
        public static List<Employee> employee = new List<Employee>();
        public class EmploeeDetails
        {
            public string EmployeeDesignation { get; set; }
            public int EmployeeCode { get; set; }
            public string CityID { get; set; }
        }
        public class Complex
        {
            public int EmployeeID { get; set; }
            public string FirstName { get; set; }
            public EmploeeDetails EmpDetails { get; set; }
            public static List<Complex> GetData()
            {
                List<Complex> Data = new List<Complex>();
                Data.Add(new Complex() { EmployeeID = 0, FirstName = "Ulrich", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Developer", EmployeeCode = 0, CityID = "city0" } });
                Data.Add(new Complex() { EmployeeID = 1, FirstName = "Michael", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "CFO", EmployeeCode = 1, CityID = "city1" } });
                Data.Add(new Complex() { EmployeeID = 2, FirstName = "Anne", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Manager", EmployeeCode = 2, CityID = "city2" } });
                Data.Add(new Complex() { EmployeeID = 3, FirstName = "Janet", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Project Lead", EmployeeCode = 3, CityID = "city3" } });
                Data.Add(new Complex() { EmployeeID = 4, FirstName = "Andrew", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Developer", EmployeeCode = 4, CityID = "city4" } });
                Data.Add(new Complex() { EmployeeID = 5, FirstName = "Margaret", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Program Directory", EmployeeCode = 5, CityID = "city5" } });
                Data.Add(new Complex() { EmployeeID = 6, FirstName = "Nancy", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Designer", EmployeeCode = 6, CityID = "city6" } });
                Data.Add(new Complex() { EmployeeID = 7, FirstName = "Robert", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "System Analyst", EmployeeCode = 7, CityID = "city6" } });
                Data.Add(new Complex() { EmployeeID = 8, FirstName = "Laura", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Sales Representative", EmployeeCode = 8, CityID = "city7" } });
                Data.Add(new Complex() { EmployeeID = 9, FirstName = "Steven", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Sales Manager", EmployeeCode = 9, CityID = "city8" } });
                Data.Add(new Complex() { EmployeeID = 10, FirstName = "James", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Program Directory", EmployeeCode = 10, CityID = "city9" } });
                Data.Add(new Complex() { EmployeeID = 11, FirstName = "Smith", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Manager", EmployeeCode = 11, CityID = "city10" } });
                Data.Add(new Complex() { EmployeeID = 12, FirstName = "Jhonson", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Organizer", EmployeeCode = 12, CityID = "city11" } });
                Data.Add(new Complex() { EmployeeID = 13, FirstName = "George", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "PO", EmployeeCode = 13, CityID = "city12" } });
                Data.Add(new Complex() { EmployeeID = 14, FirstName = "Claire", EmpDetails = new EmploeeDetails() { EmployeeDesignation = "Developer", EmployeeCode = 14, CityID = "city13" } });

                return Data;
            }

        }
        public IActionResult Index()
        {
            if (order.Count == 0)
                BindDataSource();
            ViewBag.employee = Complex.GetData().ToList();

            return View();
        }

        public IActionResult UrlDatasource([FromBody]DataManagerRequest dm)
        {
            IEnumerable DataSource = order;
            DataOperations operation = new DataOperations();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<Orders>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        private void BindDataSource()
        {
            int code = 10000;
            int j = 0;
            for (int i = 1; i < 4; i++)
            {
                order.Add(new Orders(code + 1, "ALFKI", "city" + (j), j + 0, 2.3 * i, new DateTime(1991, 05, 15), "Berlin", "Denmark"));
                order.Add(new Orders(code + 2, "ANATR", "city" + (j + 1), j + 1, 3.3 * i, new DateTime(1990, 04, 04), "Madrid", "Brazil"));
                order.Add(new Orders(code + 3, "ANTON", "city" + (j + 2), j + 2, 4.3 * i, new DateTime(1957, 11, 30), "Cholchester", "Germany"));
                order.Add(new Orders(code + 4, "BLONP", "city" + (j + 3), j + 3, 5.3 * i, new DateTime(1930, 10, 22), "Marseille", "Austria"));
                order.Add(new Orders(code + 5, "BOLID", "city" + (j + 4), j + 4, 6.3 * i, new DateTime(1953, 02, 18), "Tsawassen", "Switzerland"));
                code += 5;
                j += 5;
            }
        }
        public class Orders
        {
            public Orders()
            {

            }
            public Orders(long OrderId, string CustomerId, string CityID, int EmployeeId, double Freight, DateTime OrderDate, string ShipCity, string ShipCountry)
            {
                this.OrderID = OrderId;
                this.CustomerID = CustomerId;
                this.CityID = CityID;
                this.EmployeeID = EmployeeId;
                this.Freight = Freight;
                this.OrderDate = OrderDate;
                this.ShipCity = ShipCity;
                this.ShipCountry = ShipCountry;
            }

            public long OrderID { get; set; }
            [Required]
            public string CustomerID { get; set; }

            public string CityID { get; set; }
            public int EmployeeID { get; set; }
            public double Freight { get; set; }
            public DateTime OrderDate { get; set; }
            public string ShipCity { get; set; }
            public string ShipCountry { get; set; }
        }

        [Serializable]
        public class Employee
        {
            public Employee()
            {

            }
            public Employee(int EmployeeId, string FirstName, EmploeeDetails empDetails)
            {
                this.EmployeeID = EmployeeId;
                this.FirstName = FirstName;
                this.EmpDetails = EmpDetails;
            }
            public int EmployeeID { get; set; }
            public string FirstName { get; set; }
            public EmploeeDetails EmpDetails { get; set; }

        }

    }
}
