using MISA.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interface
{
    public interface IEmployeeBL
    {
        //Lấy toàn bộ dữ liệu nhân viên
        IEnumerable<T> GetEmployees<T>();
        IEnumerable<T> GetEmployeeCodeM<T>();
        IEnumerable<T> GetDataPage<T>(int offset, int size);
        long GetCountData<T>();
        //lấy dữ liệu bằng id 
        IEnumerable<T> GetDataById<T>(string id);
        int InsertEmployee(Employee employee);
        //Cập nhật nhân viên
        int UpdateEmployee(Employee employee);
        //Xóa nhân viên
        int DeleteEmployee(Employee employee);
    }
}
