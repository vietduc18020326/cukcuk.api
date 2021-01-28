<template>
    <div class="content">
            <HeaderEvent v-on:open-dialog="openDialog" v-on:searchTable="handleSearch"/>
            <div class="content-body">
                
                <div class="c-table" id="style-scroll">
                    <table id="listdata">
                        <thead class="table-head">
                            <tr>
                                <th><button id="btn-delete" @click="deleteemployees">Xóa</button></th>
                                <th fieldName="EmployeeCode">Mã nhân viên</th>
                                <th fieldName="FullName">Họ và tên</th>
                                <th fieldName="Gender" formatType="GenderName">Giới tính</th>
                                <th fieldName="DateOfBirth" formatType="date">Ngày sinh</th>
                                <th fieldName="EmployeePositionId" formatType="position">Chức vụ</th>
                                <th fieldName="PhoneNumber">Điện thoại</th>
                                <th fieldName="Email">Email</th>
                                <th fieldName="EmployeeTaxCode">Mã số thuế cá nhân</th>
                                <th fieldName="EmployeeDepartmentId" formatType="department">Phòng ban</th>
                                <th fieldName="Salary">Mức lương cơ bản</th>
                                <th fieldName="StatusJob" formatType="statusJob">Tình trạng công việc</th>
                            </tr>
                        </thead>
                        <tbody class="table-body" id="table-body">
                            <tr 
                                v-for="(employee,index) in Employees" 
                                :key="index"
                                @dblclick="openDialogInsert(index)"
                                :class="{c_table: index%2}"
                            >
                                <td><input type="checkbox" :id="employee.EmployeeCode" @click="handlCheckbox(index)"/></td>
                                <td>{{employee.EmployeeCode}}</td>
                                <td>{{employee.FullName}}</td>
                                <td>{{employee.Gender | GenderName}}</td>
                                <td>{{formDate(employee.DateOfBirth)}}</td>
                                <td>{{positionName(employee.EmployeePositionId)}}</td>
                                <td>{{employee.PhoneNumber}}</td>
                                <td>{{employee.Email}}</td>
                                <td>{{employee.EmployeeTaxCode}}</td>
                                <td>{{departmentName(employee.EmployeeDepartmentId)}}</td>
                                <td>{{employee.Salary}}</td>
                                <td>{{employee.StatusJob | StatusJob}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <Pagination 
                v-on:loadData="loadEmployee" 
                :totalemployee="amountEmployee"
                :amountEmployeePage="Employees.length"
            />
        </div>
</template>

<script>
import Pagination from './common/Pagination.vue'
import HeaderEvent from './common/HeaderEvent.vue'
import employeeServices from '../services/employeeServices.js'
import departmentService from '../services/departmentService.js'
import positionService from '../services/positionService.js'

export default {
    name: 'Employee',
    components: {
        Pagination,
        HeaderEvent
    },
    data() {
        return{
            departments: [],
            positions: [],
            search: '',
            Employees: [],
            pagi: 10,
            deleteEmployees: [],
            amountEmployee: 0,
            employeePage: 0,
        }
    },
    async mounted(){
        this.loadData();

    },
    updated() {
        for(let i=0;i<this.Employees.length;i++){
                var box = document.getElementById(this.Employees[i].EmployeeCode);
                if(this.deleteEmployees.includes(this.Employees[i].EmployeeId)) box.checked = true;
                else box.checked = false;
        }
    },
    methods: {
        loadData: async function(){
            this.departments = await departmentService.getDepartment();
            this.positions = await positionService.getPosition();

            //lấy dữ employee vào data
            this.amountEmployee = await employeeServices.GetCountEmployee();
            this.employeePage = Math.ceil(this.amountEmployee / this.pagi);
            this.Employees = await employeeServices.GetEmployeePage(0,this.employeePage);

        },
        openDialog: async function (){
            this.$emit('open-dialog',this.departments,this.positions);
        },
        openDialogInsert: function(index){
            this.$emit('open-dialogInsert'
                        ,this.Employees[index]
                        ,this.departments
                        ,this.positions
            );
        },
        loadEmployee: async function(number){
            var s = (number-1)*this.employeePage;
            this.Employees = await employeeServices.GetEmployeePage(s,this.employeePage);
        },
        handlCheckbox: function(index){
            var box = document.getElementById(this.Employees[index].EmployeeCode);
            if(box.checked){
                this.deleteEmployees.push(this.Employees[index].EmployeeId);
                console.log(this.deleteEmployees)
            }
            else{
                var id = this.Employees[index].EmployeeId;
                this.deleteEmployees = this.deleteEmployees.filter(item => item !== id);
                console.log(this.deleteEmployees);
            }
        },
        deleteemployees: async function(){
            for(let i=0; i<this.deleteEmployees.length ; i++){
                await employeeServices.deleteEmployee(this.deleteEmployees[i]);
            }
            location.reload();
        },
        handleSearch: function(search){
            this.search = search;
        },
        formDate: function(daTe){
            var date = new Date(daTe);
            
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();
            if(!day)    return "";

            return day + '-' + month + '-' + year;
        },
        departmentName: function(departmentId){
            for(var i=0; i<this.departments.length;i++){
                if(this.departments[i].EmployeeDepartmentId == departmentId){
                    return this.departments[i].EmployeeDepartmentName;
                }
            }
            return "Không xác định";
        },
        positionName:  function(positionId){
            for(var i=0 ; i<this.positions.length ; i++){
                if(this.positions[i].EmployeePositionId == positionId)
                    return this.positions[i].EmployeePosiitionName;
            }
        },
    },
    filters: {
        GenderName: function(Gender){
            if(Gender == 1){
                return "Nữ";
            }
            if(Gender == 0){
                return "Nam";
            }
            return "Không xác định";
        },
        StatusJob: function(status){
            if(status == 0){
                return "Đang làm việc";
            }
            if(status == 1){
                return "Đang nghỉ việc";
            }
            return "Không xác định";
        },
    },
//     computed: {
//         filteredList() {
//             if(this.search!=''){
//                 return this.employees.data.filter(employee => {
//                     return employee.EmployeeCode.toLowerCase().includes(this.search.toLowerCase())|
//                             employee.FullName.toLowerCase().includes(this.search.toLowerCase())|
//                             employee.PhoneNumber.toLowerCase().includes(this.search.toLowerCase())
//                 })
//             }
//             return this.Employees;
//         }
//    },
}
</script>

<style scoped>
#btn-delete{
    background-color: #F65454; 
    color: white;
    border-radius: 4px;
}
#btn-delete:hover{
    background-color: #FF6900;
    cursor: pointer;

}
.c_table{
    background-color: #e5e5e5;
}
</style>