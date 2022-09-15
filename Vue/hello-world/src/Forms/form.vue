<template>
    {{getemployeeCount}} 
    {{this.count}}
    <div>
        <input v-model="employee.name" type="text" />
        <input type="radio" v-model="employee.chooseColor" value='red' />red
        <input type="radio" v-model="employee.chooseColor" value='blue' />blue
        <input type="radio" v-model="employee.chooseColor" value='green' />green
        <div :style="
        [{'color':employee.chooseColor}, this.employee.BackGroudcolorGroup]"
            :class="['fontClass',{changeFont:this.employee.addClass}, ]">
            color
        </div>
        <button type="button" @click="addFonts">add font size</button>

    </div>
</template>
<script>
export default {
    name: 'formComponent',
    data() {
        return {
            employee: { name: 'test', chooseColor: 'black', BackGroudcolorGroup: {}, addClass: false },
            count:5
        }
    },
    methods: {
        changeWidth() {
            this.employee.addClass = true;
        },
        addFonts() {
  this.count=6;
            this.employee.addClass = true;
        },
        addBackGroundColor() {
            this.employee.BackGroudcolorGroup = { 'background-color': 'green' }
        }
    },
    computed:{
        getemployeeCount(){
            return this.count;
        }
    }
}

</script>
<style>
.fontClass {
    font-size: 30px;
    color: 'black'
}

.changeFont {
    font-size: 70px;
}
</style>