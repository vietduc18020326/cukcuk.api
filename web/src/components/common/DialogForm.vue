<template>
    <div class="customer-dialog">
        <div class="form-dialog">
            <div class="form-header">
                <p class="title">THÔNG TIN NHÂN VIÊN</p>
                <i class="icon-x" @click="closeDialog"></i>
            </div>
            <div class="form-body">
                <div class="avatar">
                    <div class="icon-avatar"></div>
                    <div class="caption-avatar">
                        (Vui lòng chọn ảnh có định dạng <strong>.jpg,.jpeg,<br />.png,.gif</strong>)
                    </div>
                </div>
                <form class="form-near-avat" id="form-1">
                    <h3>A. THÔNG TIN CHUNG:</h3>
                    <div class="underlined"></div>
                    <input id="employeeId" type="hidden" value=""/>
                    <div class="cus-left" id="cus_id">
                        <label for="cus-id">Mã nhân viên (*)</label>
                        <input id="employeeCode" type="text" v-model="EmployeeCodeMax" required disabled/>
                        <span class="form-message"></span>
                    </div>
                    <div class="cus-right">
                        <label for="cus-name">Họ và tên (*)</label>
                        <input id="fullName" type="text" v-model.trim="$v.Employee.FullName.$model" required/>
                        <span class="form-message" v-if="!$v.Employee.FullName.required">Trường này không được để trống</span>
                    </div>
                    <div class="cus-left">
                        <label for="cus-birth">Ngày sinh</label>
                        <input id="dateOfBirth" type="date" v-model="DateOfBirth"/>
                    </div>
                    <div class="cus-right">
                        <label for="sex">Giới tính</label>
                        <select id="gender" v-model="Employee.Gender">
                            <option value="0">Nam</option>
                            <option value="1">Nữ</option>
                            <option value="2">Không xác định</option>
                        </select>
                    </div>
                    <div class="cus-left">
                        <label>Số CMTND/Căn cước (*)</label>
                        <input id="indentifyNumber" type="text" v-model.trim="Employee.IndentifyNumber" required/>
                        <span class="form-message" v-if="!$v.Employee.IndentifyNumber.required">Trường này không được để trống</span>
                    </div>
                    <div class="cus-right">
                        <label>Ngày cấp</label>
                        <input id="releaseDay" type="date" v-model="ReleaseDay"/>
                    </div>
                    <div class="clear"></div>
                    <div class="cus-left">
                        <label>Nơi cấp</label>
                        <input id="releaseLocation" type="text" v-model="Employee.ReleaseLocation"/>
                    </div>
                    <div class="clear"></div>
                    <div class="cus-left">
                        <label>Email (*)</label>
                        <input id="email" type="email" v-model.trim="Employee.Email" required/>
                        <span class="form-message" v-if="!$v.Employee.Email.required">Trường này không được để trống</span>
                    </div>
                    <div class="cus-right">
                        <label>Số điện thoại (*)</label>
                        <input id="phoneNumber" type="text" v-model.trim="Employee.PhoneNumber" required/>
                        <span class="form-message" v-if="!$v.Employee.PhoneNumber.required">Trường này không được để trống</span>
                    </div>
                    <div class="clear"></div>
                    <h3>B. THÔNG TIN CÔNG VIỆC:</h3>
                    <div class="underlined"></div>
                    <div class="cus-left">
                        <label for="position">Công việc</label>
                        <select id="employeePosition" v-model="positionCode">
                            <option value="1">Trưởng phòng</option>
                            <option value="2">Thư ký</option>
                            <option value="3">Nhân viên</option>
                        </select>
                    </div>
                    <div class="cus-right">
                        <label for="department">Phòng ban</label>
                        <select id="employeeDepartment" v-model="departmentCode">
                            <option value="3">Phòng nhân sự</option>
                            <option value="2">Phòng kế toán</option>
                            <option value="1">Phòng tài chính</option>
                        </select>
                    </div>
                    <div class="cus-left">
                        <label>Mã số thuế cá nhân</label>
                        <input id="employeeTaxCode" type="text" v-model="Employee.EmployeeTaxCode"/>
                    </div>
                    <div class="cus-right">
                        <label>Mức lương cơ bản</label>
                        <input id="salary" type="text" v-model="Employee.Salary"/>
                    </div>
                    <div class="cus-left">
                        <label>Ngày gia nhập công ty</label>
                        <input id="joinDate" type="date" v-model="JoinDate"/>
                    </div>
                    <div class="cus-right">
                        <label for="status_job">Tình trạng công việc</label>
                        <select id="statusJob" v-model="Employee.StatusJob">
                            <option value="0">Đang làm việc</option>
                            <option value="1">Đang nghỉ việc</option>
                        </select>
                    </div>
                </form>
            </div>
            
            <div class="form-footer">
                <div class="btn-footer">
                    <button class="btn-cancel" @click="closeDialog">Hủy</button>
                    <button 
                        class="btn-delete" 
                        :class="{checkBtn: !InsertForm}"
                        @click="openWarn"
                    >Xóa</button>
                    <button class="btn-save" @click="handleSaveEmployee">Lưu</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import employeeService from '../../services/employeeServices.js'
import { required} from 'vuelidate/lib/validators'

export default {
    name: 'DialogForm',
    props:['isInsert','employee','departmentName','positionName'],
    data() {
        return{
            InsertForm: false,
            Employee: {},
            departments: [],
            positions: [],
            departmentCode: 0,
            positionCode: 0,
            EmployeeCodeMax: '',
            ReleaseDay: '',
            DateOfBirth: '',
            JoinDate: '',
        }
    },
    validations: {
        Employee:{
            FullName: {
                required
            },
            Email: {
                required
            },
            PhoneNumber: {
                required
            },
            IndentifyNumber: {
                required
            }
        }
    },
    async mounted(){
        this.InsertForm = this.isInsert;
        this.Employee = this.employee;
        if(!this.InsertForm){
            this.EmployeeCodeMax = this.getCodeMax(await employeeService.getEmployeeMax());
        }
        else{
            this.EmployeeCodeMax = this.Employee.EmployeeCode;
            this.ReleaseDay = this.ConvertDate(this.employee.ReleaseDay);
            this.DateOfBirth = this.ConvertDate(this.employee.DateOfBirth);
            this.JoinDate = this.ConvertDate(this.employee.JoinDate);
        }
        this.departments = this.departmentName;
        this.positions = this.positionName;
        this.departmentCode = this.getDepartmentCode(this.Employee.EmployeeDepartmentId);
        this.positionCode = this.getPositionCode(this.Employee.EmployeePositionId);
    },
    methods: {
        closeDialog: function(){
            this.$emit('close-dialog');
            console.log(this.$v.Employee.FullName);
        },
        openWarn: function(){
            this.$emit('open-dialogWarn',this.Employee.EmployeeId);
        },
        handleSaveEmployee: function(){
            this.Employee.EmployeeCode = this.EmployeeCodeMax;
            this.Employee.DateOfBirth = this.DateOfBirth;
            this.Employee.JoinDate = this.JoinDate;
            this.Employee.ReleaseDay = this.ReleaseDay;
            this.Employee.EmployeeDepartmentId = this.departments[this.departmentCode-1].EmployeeDepartmentId;
            this.Employee.EmployeePositionId = this.positions[this.positionCode-1].EmployeePositionId;
            if(this.isInsert){
                employeeService.updateEmployee(this.Employee);
                console.log(this.Employee);
            }
            else{
                employeeService.insertEmployee(this.Employee);
                console.log(this.Employee);
            }
            this.closeDialog();
        },
        getCodeMax: function(code){
            var a = "";
            for(let i=0;i<code.length;i++){
                if(code[i]>='0'&&code[i]<='9'){
                    a += code[i];
                }
            }
            var b = Number(a);
            b = b+1;
            return 'NV' + String(b);
        },
        ConvertDate: function(daTe){
            var date = new Date(daTe);

            var day = date.getDate()>9 ? date.getDate() : '0' + date.getDate();
            var month = (date.getMonth() + 1)>9 ? date.getMonth() + 1 : '0'+(date.getMonth() + 1);
            var year = date.getFullYear();
            if(!day)    return "";

            return year + '-' + month + '-' + day;
        },
        getDepartmentCode: function(id){
            if(id === "447816de-6069-15bf-de3d-c9e9e9038fe2"){
                return 3;
            }
            if(id === "39446ba3-3071-7af6-a5a9-deaf32c96293"){
                return 2;
            }
            return 1;
        },
        getPositionCode: function(id){
            if(id === "13a507d0-342d-6292-6de3-2ceaa9cf3765"){
                return 1;
            }
            if(id === "1f60e54f-196f-45f3-5498-ae38c5379e4b"){
                return 2;
            }
            return 3;
        }
    },
}
</script>

<style scoped>
.checkBtn{
    display: none;
}
</style>