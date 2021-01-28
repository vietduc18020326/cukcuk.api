using MISA.ApplicationCore.Interface;
using MISA.Common;
using MISA.Infrastructure;
using MISA.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore
{
    public class EmployeeBL: IEmployeeBL
    {
        IEmployeeDL _employeeDL;
        public EmployeeBL(IEmployeeDL employeeDL)
        {
            _employeeDL = employeeDL;
        }
        public IEnumerable<T> GetEmployeeCodeM<T>() {
            return _employeeDL.GetEmployeeCodeM<T>();
        }

        //Lấy toàn bộ dữ liệu nhân viên
        public IEnumerable<T> GetEmployees<T>()
        {
            return _employeeDL.GetEmployees<T>();
        }
        public IEnumerable<T> GetDataPage<T>(int offset,int size)
        {
            return _employeeDL.GetDatapage<T>(offset, size);
        }
        public long GetCountData<T>()
        {
            return _employeeDL.GetCountdata<T>();
        }
        //lấy dữ liệu bằng id 
        public IEnumerable<T> GetDataById<T>(string id)
        {
            return _employeeDL.GetEmployeeById<T>(id);
        }
        public int InsertEmployee(Employee employee)
        {
            if (_employeeDL.CheckDuplicateIndentify(employee))
            {
                return -2;
            }

            if (_employeeDL.CheckDuplicatePhone(employee))
            {
                return -1;
            }
   
            return _employeeDL.InsertEmployee(employee);
        }
        //Cập nhật nhân viên
        public int UpdateEmployee(Employee employee)
        {
            if (_employeeDL.CheckDuplicateIndentifyandId(employee))
            {
                return -2;
            }

            if (_employeeDL.CheckDuplicatePhoneandId(employee))
            {
                return -1;
            }
            return _employeeDL.UpdateEmployee(employee);
        }
        //Xóa nhân viên
        public int DeleteEmployee(Employee employee)
        {
            if (!_employeeDL.CheckDuplicateEmployeeId(employee.EmployeeId.ToString()))
                return -1;
            return _employeeDL.DeleteEmployee(employee);
        }
        

    }
}
