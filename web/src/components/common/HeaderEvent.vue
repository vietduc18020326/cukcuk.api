<template>
    <div class="content-header">
                <div class="ch-left">
                    <h2>Danh sách nhân viên</h2>
                    <div class="search">
                        <div class="icon-search"></div>
                        <input 
                            type="text" 
                            name="Tìm kiếm" 
                            class="input-search" 
                            placeholder="Tìm kiếm theo Mã, Tên hoặc số điện thoại" 
                            id="search-table"
                            v-model="search"
                            v-on:keyup="searchTable"
                        />
                        <select>
                            <option>Tất cả phòng ban</option>
                            <option v-for="(department,index) in departments" :key="index">
                                {{department.EmployeeDepartmentName}}
                            </option>
                        </select>
                        <select>
                            <option>Tất cả vị trí</option>
                            <option v-for="(position,index) in positions" :key="index">
                                {{position.EmployeePosiitionName}}
                            </option>
                        </select>
                    </div>
                </div>
                <div class="ch-right">
                    <div class="add">
                        <button type="button" id="button-add" @click="openDialog">
                            <i class="icon-add"></i>
                            Thêm nhân viên
                        </button>
                        
                    </div>
                    <div class="load">
                        <button id="button-load">
                            <i class="icon-load"></i>
                        </button>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
</template>

<script>
import departmentService from "../../services/departmentService";
import positionService from "../../services/positionService";

export default {
    name: 'HeaderEvent',
    data() {
        return{
            search: null,
            departments: [],
            positions: [],
        }
    },
    async mounted() {
        this.departments = await departmentService.getDepartment();
        this.positions = await positionService.getPosition();
    },
    methods: {
        openDialog: function(){
            this.$emit('open-dialog');
        },
        searchTable: function(){
            console.log(this.search)
            this.$emit('searchTable',this.search);
        },
    }
}
</script>

<style scoped>
.search select{
    width: 25%;
    margin-right: 15px;
    padding: 10px;
    border-radius: 4px;
}
</style>
