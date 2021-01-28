<template>
    <div class="content-footer">
                <div class="fl">
                    <p>Hiển thị 1-100/{{totalemployee}} nhân viên</p>
                </div>
                <div class="pagination">
                    <div class="page-numbers">
                        <div class="page-number" @click="handlActive(0)"><i class="icon-pagination firstpage"></i></div>
                        <div class="page-number" @click="prevPage"><i class="icon-pagination prevpage"></i></div>
                        <div class="wp-pagi-nb" v-for="(number,index) in numbers" :key="index">
                            <div 
                                class="pagi-nb" 
                                :class="{pagi_num_active: number.active}" 
                                @click="handlActive(index)"
                            >
                            {{ number.number }}
                            </div>
                        </div>
                        <div class="page-number" @click="nextPage"><i class="icon-pagination nextpage"></i></div>
                        <div class="page-number" @click="handlActive(numbers.length-1)"><i class="icon-pagination lastpage"></i></div>
                    </div>
                </div>
                <div class="fr">
                    <p>{{amountEmployeePage}} nhân viên/trang</p>
                </div>
            </div>
</template>

<script>
export default {
    name: 'Pagination',
    props: ['totalemployee','amountEmployeePage'],
    data() {
        return{
            numbers: [
                {number: 1, active: true},
                {number: 2, active: false},
                {number: 3, active: false},
                {number: 4, active: false},
                {number: 5, active: false},
                {number: 6, active: false},
                {number: 7, active: false},
                {number: 8, active: false},
                {number: 9, active: false},
                {number: 10, active: false},
            ],
            indexActive: 0,
        }
    },
    methods: {
        checkActive: function(index){
            for(var i=0; i<this.numbers.length; i++){
                if(i==index){    this.numbers[i].active = true;  this.indexActive = i;}
                else    this.numbers[i].active = false;
            }
        },
        handlActive: function(index){
            this.checkActive(index);
            this.$emit('loadData',this.numbers[index].number);
        },
        nextPage: function(){
            if(this.indexActive+1 < this.numbers.length){
                this.handlActive(this.indexActive + 1);
            }
        },
        prevPage: function(){
            if((this.indexActive-1) >= 0){
                this.handlActive(this.indexActive - 1);
            }
        },
    },
}
</script>