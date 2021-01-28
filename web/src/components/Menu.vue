<template>
    <div class="panel-left">
        <div class="logo">
            <div class="expend-icon"></div>
            <div class="logo-page"></div>
        </div>
        <div 
            class="menu"
            v-for="(menu,index) in Menu"
            :key="index"
        >
            <div class="menu__item">
                <div class="menu__item--icon" :class="menu.class"></div>
                <div class="menu__item--text">
                    {{menu.name}}
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Menu',
    data() {
        return{
            Menu: [
                {name:'Tổng quan', class: 'icon-overview'},
                {name:'Báo cáo', class: 'icon-report'},
                {name:'Mua hàng', class: 'icon-pay'},
                {name:'Danh sách khách hàng', class: 'icon-listcustomer'},
                {name:'Danh sách nhân viên', class: 'icon-listcustomer'},
                {name:'Thiết lập hệ thống', class: 'icon-setting'},
            ]
        }
    }
}
</script>