<template>
  <div id="app">
    <Menu />
    <div class="panel-right">
        <Header/>
        <Employee 
          v-on:open-dialog="handleDialogAdd"
          v-on:open-dialogInsert="handleDialogInsert"
        />
    </div>

    <div :class="{back_dialog: isDialogForm}"></div>
    <DialogForm 
      v-if="isDialogForm" 
      v-on:close-dialog="handleCloseDialog" 
      :isInsert="isInsert"
      :employee="employee"
      :departmentName="Departments"
      :positionName="Positions"
      v-on:open-dialogWarn="handlDialogWarn"
    />
    <div :class="{back_dialog: isDialogWarn}"></div>
    <DialogWarn 
      v-if="isDialogWarn"
      v-on:close-dialogWarn="isDialogWarn =! isDialogWarn"
      :EmployeeId="employeeId"
    />
    
  </div>
</template>

<script>
import './assets/css/main.css'
import Menu from './components/Menu.vue'
import Header from './components/Header.vue'
import Employee from './components/Employee.vue'
import DialogForm from './components/common/DialogForm'
import DialogWarn from './components/common/DialogWarn'


export default {
  name: 'App',
  data() {
    return{
      isDialogForm: false,
      isDialogWarn: false,
      isInsert: false,
      employee: {},
      Departments: [],
      Positions: [],
      employeeId: '',
    }
  },
  methods: {
    handleDialogInsert: function(employee,departmentName,positionName){
      this.isDialogForm = !this.isDialogForm;
      this.isInsert = true;
      this.employee = employee;
      this.Departments = departmentName;
      this.Positions = positionName;
    },
    handleDialogAdd: function(departmentName,positionName){
      this.isDialogForm = !this.isDialogForm;
      this.employee = {};
      this.Departments = departmentName;
      this.Positions = positionName;
    },
    handleCloseDialog: function(){
      this.isDialogForm = !this.isDialogForm;
      this.isInsert = false;
    },
    handlDialogWarn: function(id){
      this.employeeId = id;
      this.isDialogWarn = !this.isDialogWarn;
    },
  },
  components: {
    Menu,
    Header,
    Employee,
    DialogForm,
    DialogWarn,
  }
}
</script>

<style>
.back_dialog{
    background: black;
    opacity: 0.4;
    width: 100%;
    height: 100vh;
    position: absolute;
    top: 0;
}
</style>
