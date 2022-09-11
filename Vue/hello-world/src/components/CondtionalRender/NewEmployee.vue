<template>
    <div>
        <div v-for="employee in Employees" :key="employee.name" style="cursor:pointer">
            <li @click="SelectedEmployee=employee"> {{employee.name}}</li>
        </div>
        <div v-if="SelectedEmployee">
            <input type="text" v-model="SelectedEmployee.name" />
        </div>
    </div>
</template>
<script>
export default {
    name: 'NewEmployee',
    data() {
        return {
            SelectedEmployee: undefined,
            Employees: [
                {
                    name: 'pavan',
                    address: { area: 'testarea', street: 'teststreet' }
                }, {
                    name: 'manju',
                    address: { area: 'testarea', street: 'teststreet' }
                }
            ]
        }
    }
}
</script>
<style>

</style>