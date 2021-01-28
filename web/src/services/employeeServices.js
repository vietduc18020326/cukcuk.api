import axios from 'axios'
import {apiUrl} from '../config.json'


const getEmployee = async () => {
    const employee = axios.get(apiUrl).then(res => {return res;});
    let res = await employee;
    return res.data;
}

const updateEmployee = (employee) => {
    axios.put(apiUrl,employee);
}

const insertEmployee = (employee) => {
    axios.post(apiUrl,employee);
}

const GetCountEmployee = async () => {
    const count = axios.get(apiUrl +'count').then(res => {return res;});
    let res = await count;
    return res.data;
}

const GetEmployeePage = async (offset,size) => {
    const employees = axios.get(apiUrl +'page/'+offset+'&'+size).then(res => {return res;});
    let res = await employees;
    return res.data;
}

const deleteEmployee = async (id) => {
    await axios.delete(apiUrl + id);
}

const getEmployeeMax = async () => {
    const employeeMax = await axios.get(apiUrl+'max').then(res => {return res;});
    console.log(employeeMax);
    return employeeMax.data[0].EmployeeCode;
}


export default {
    getEmployee,
    updateEmployee,
    insertEmployee,
    getEmployeeMax,
    deleteEmployee,
    GetCountEmployee,
    GetEmployeePage,
};
