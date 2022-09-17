<template>
    <Child :name='"pavankog"' @getdata="childData"/>
</template>
<script>
import Child from './child.vue';
export default {
    name:"EmployeeData",
    components: { Child },
    methods:{
        childData(val){
            console.log(val);
        }
    }
}
</script>