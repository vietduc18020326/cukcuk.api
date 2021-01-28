using MISA.Common;
using MISA.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class EmployeeDL:DbConnector, IEmployeeDL
    {
        DbConnector db = new DbConnector();
        
        //lấy toàn bộ danh sách dữ liệu nhân viên
        public IEnumerable<T> GetEmployees<T>()
        {
            return db.GetAllData<T>();
        }
        //lấy dữ liệu bằng id
        public IEnumerable<T> GetEmployeeById<T>(string id)
        {
            return db.GetDataById<T>(id);
        }
        public IEnumerable<T> GetDatapage<T>(int offset,int size)
        {
            return db.GetDataPage<T>(offset, size);
        }
        public long GetCountdata<T>()
        {
            return db.GetCountData<T>();
        }

        //thêm nhân viên 
        public int InsertEmployee(Employee employee)
        {
            return db.Insert<Employee>(employee);
        }
        //Cập nhật nhân viên 
        public int UpdateEmployee(Employee employee)
        {
            return db.Update<Employee>(employee);
        }
        public int DeleteEmployee(Employee employee)
        {
            return db.delete<Employee>(employee);
        }
        public bool CheckDuplicateEmployeeId(string id)
        {
            List<Employee> employees = db.GetDataById<Employee>(id);

            if (employees.Count() > 0)
                return true;

            return false;

        }

        //kiểm tra trùng indentify và phone khi insert
        public bool CheckDuplicatePhone(Employee employee)
        {
            List<Employee> employees = db.GetDataByPhone(employee);
            if (employees.Count() > 0)
                return true;
            return false;
        }
        public bool CheckDuplicateIndentify(Employee employee)
        {
            List<Employee> employees = db.GetDataByIndentify(employee);
            if (employees.Count() > 0)
                return true;
            return false;
        }

        //kiểm tra trùng khi update
        public bool CheckDuplicateIndentifyandId(Employee employee)
        {
            List<Employee> employees = db.GetDataByIndentifyandId(employee);
            if (employees.Count() > 0)
                return true;
            return false;
        }
        public bool CheckDuplicatePhoneandId(Employee employee)
        {
            List<Employee> employees = db.GetDataByPhoneandId(employee);
            if (employees.Count() > 0)
                return true;
            return false;
        }

        public IEnumerable<T> GetEmployeeCodeM<T>()
        {
            return db.GetEmployeeCodeMax<T>();
        }
    }
}
