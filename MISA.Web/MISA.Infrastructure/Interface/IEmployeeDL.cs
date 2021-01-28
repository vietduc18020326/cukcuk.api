using MISA.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure.Interface
{
    public interface IEmployeeDL
    {
        //lấy toàn bộ danh sách dữ liệu nhân viên
        IEnumerable<T> GetEmployees<T>();
        IEnumerable<T> GetEmployeeCodeM<T>();
        IEnumerable<T> GetDatapage<T>(int offset,int size);
        long GetCountdata<T>();
        //lấy dữ liệu bằng id
        IEnumerable<T> GetEmployeeById<T>(string id);

        //thêm nhân viên 
        int InsertEmployee(Employee employee);
        //Cập nhật nhân viên 
        int UpdateEmployee(Employee employee);
        int DeleteEmployee(Employee employee);
        bool CheckDuplicateEmployeeId(string id);

        // Kiểm tra trùng khi insert
        bool CheckDuplicatePhone(Employee employee);
        bool CheckDuplicateIndentify(Employee employee);

        // kiểm tra trùng khi update
        bool CheckDuplicateIndentifyandId(Employee employee);
        bool CheckDuplicatePhoneandId(Employee employee);
    }
}
