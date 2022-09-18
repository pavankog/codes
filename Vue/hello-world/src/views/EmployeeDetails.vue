<template>
  <pre> {{employees}} </pre>
</template>
<script>
import Employee from '../Employee.js'
export default {
  data() {
    return {
      employees: undefined
    }
  },
  mounted() {
    this.employees = Employee;
  }
}
</script>
