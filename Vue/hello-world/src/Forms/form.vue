<template>
    <form>
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

    </form>
</template>
<script>
export default {
    name: 'formComponent',
    data() {
        return {
            employee: { name: 'test', chooseColor: 'black', BackGroudcolorGroup: {}, addClass: false }
        }
    },
    methods: {
        changeWidth() {
            this.employee.addClass = true;
        },
        addFonts() {

            this.employee.addClass = true;
        },
        addBackGroundColor() {
            this.employee.BackGroudcolorGroup = { 'background-color': 'green' }
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