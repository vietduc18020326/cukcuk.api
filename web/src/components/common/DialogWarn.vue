<template>
    <div class="warn" title="Cảnh báo">
        <div id="title">Xóa nhân viên</div>
        <div id="title-warn">Bạn có chắc chắn muốn xóa nhân viên này ?</div>
        <div class="btn-footer">
            <div class="btn-warn">
                <button class="btn-no" @click="closeWarn">Không</button>
                <button class="btn-yes" @click="deleteEmployee">Có</button>
            </div>
        </div>
    </div>
</template>

<script>
// import axios from 'axios';
import employeeServices from '../../services/employeeServices.js'

export default {
    name: 'DialogWarn',
    data() {
        return{
            employeeId: '',
        }
    },
    props: ['EmployeeId'],
    mounted(){
        this.employeeId = this.EmployeeId;
    },
    methods: {
        closeWarn: function(){
            this.$emit('close-dialogWarn');
        },
        deleteEmployee: function(){
            console.log(this.EmployeeId);
            employeeServices.deleteEmployee(this.EmployeeId);
            location.reload();
        }
    }
    
}
</script>