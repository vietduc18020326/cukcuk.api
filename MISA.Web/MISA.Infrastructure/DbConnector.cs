using Dapper;
using MISA.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class DbConnector
    {
        protected string connectionString = "User Id=nvmanh;Host=103.124.92.43;Database=MS4_22_PNVD_CukCuk;port=3306;password=12345678;Character Set=utf8";
        protected IDbConnection db;

        public DbConnector()
        {
            db = new MySqlConnection(connectionString);
        }

        public IEnumerable<T> GetAllData<T>()
        {
            var tableName = typeof(T).Name;
            var list = db.Query<T>($"SELECT * FROM {tableName}").ToList();

            return list;
        }

        // LẤY SỐ LƯỢNG DỮ LIỆU TRONG BẢNG
        public long GetCountData<T>()
        {
            var tableName = typeof(T).Name;
            var storeName = $"pro_GetCount{tableName}";

            var count = db.ExecuteScalar(storeName, commandType: CommandType.StoredProcedure);
            return (long)count;
        }
        public IEnumerable<T> GetDataPage<T>(int offset, int size)
        {
            var tableName = typeof(T).Name;
            var storeName = $"pro_Get{tableName}OfPage";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@offset", offset);
            dynamicParameters.Add("@size", size);

            var list = db.Query<T>(storeName, dynamicParameters, commandType: CommandType.StoredProcedure);
            return list;
        }

        public IEnumerable<T> GetEmployeeCodeMax<T>()
        {
            var tableName = typeof(T).Name;
            var list = db.Query<T>($"SELECT MAX(EmployeeCode) as EmployeeCode FROM {tableName}");

            return list;
        }

        public List<T> GetDataById<T>(string id)
        {
            var tableName = typeof(T).Name;
            //var storeName = $"pro_Get{tableName}ById";
            //DynamicParameters dynamicParameters = new DynamicParameters();
            //dynamicParameters.Add(@$"{tableName}Id", id);
            //var list = db.Query<T>(storeName, dynamicParameters, commandType: CommandType.StoredProcedure);
            var list = db.Query<T>($"SELECT * FROM {tableName} WHERE {tableName}Id = '{id}'").ToList();
            return list;
        }

        //kiểm tra trùng khi insert
        public List<Employee> GetDataByPhone(Employee employee)
        {
            var phone = employee.PhoneNumber;
            var list = db.Query<Employee>($"SELECT * FROM Employee WHERE PhoneNumber = '{phone}'").ToList();
            return list;
        }
        public List<Employee> GetDataByIndentify(Employee employee)
        {
            var indentify = employee.IndentifyNumber;
            var list = db.Query<Employee>($"SELECT * FROM Employee WHERE IndentifyNumber = '{indentify}'").ToList();
            return list;
        }

        // kiểm tra trùng khi update
        public List<Employee> GetDataByIndentifyandId(Employee employee)
        {
            var indentify = employee.IndentifyNumber;
            var id = employee.EmployeeId.ToString();
            var list = db.Query<Employee>($"SELECT * FROM Employee WHERE IndentifyNumber = '{indentify}' && EmployeeId != '{id}'").ToList();
            return list;
        }
        public List<Employee> GetDataByPhoneandId(Employee employee)
        {
            var phone = employee.PhoneNumber;
            var id = employee.EmployeeId.ToString();
            var list = db.Query<Employee>($"SELECT * FROM Employee WHERE PhoneNumber = '{phone}' && EmployeeId != '{id}'").ToList();
            return list;
        }

        public int Insert<T>(T entity)
        {
            var tableName = typeof(T).Name;
            var storeName = $"pro_Insert{tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if(propertyType == typeof(Guid))
                {
                    dynamicParameters.Add($"@{propertyName}", propertyValue.ToString());
                }
                else
                {
                    dynamicParameters.Add($"@{propertyName}", propertyValue);
                }
            }
            var list = db.Execute(storeName, dynamicParameters, commandType: CommandType.StoredProcedure);
            return list;
        }
        //Cập nhật 
        public int Update<T>(T entity)
        {
            var tableName = typeof(T).Name;
            var storeName = $"pro_Update{tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid))
                {
                    dynamicParameters.Add($"@{propertyName}", propertyValue.ToString());
                }
                else
                {
                    dynamicParameters.Add($"@{propertyName}", propertyValue);
                }
            }
            var list = db.Execute(storeName, dynamicParameters, commandType: CommandType.StoredProcedure);
            return list;
        }
        //Xóa nhân viên
        public int delete<T>(T entity)
        {
            var tableName = typeof(T).Name;
            var storeName = $"pro_Delete{tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid))
                {
                    dynamicParameters.Add($"@{propertyName}", propertyValue.ToString());
                }
                else
                {
                    dynamicParameters.Add($"@{propertyName}", propertyValue);
                }
            }
            var list = db.Execute(storeName, dynamicParameters, commandType: CommandType.StoredProcedure);
            return list;
        }
    }
}
