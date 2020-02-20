using System.Collections.Generic;
using System.Net.Http;
using EmployeeDemo.Models;
using System.Text.Json;

namespace EmployeeDemo
{
    public class DataAccess
    {
        private static readonly string url = "http://dummy.restapiexample.com/api/v1/employees";
        private static List<EmployeeModel> _employees = null;
                
        public static List<EmployeeModel> GetEmployees()
        {
            if(_employees == null)
            {
                HttpClient client = new HttpClient();
                string jsonStr = client.GetStringAsync(url).Result;

                var jDoc = JsonDocument.Parse(jsonStr);
                var jElements = jDoc.RootElement.GetProperty("data");

                _employees = new List<EmployeeModel>();
                for(int i=0; i<jElements.GetArrayLength(); i++)
                {
                    var o = JsonSerializer.Deserialize<EmployeeModel>(jElements[i].ToString());
                    _employees.Add(o);
                }
            }

            return _employees;
        }
    }
}
