<template>
 <div class="nam">hey {{name}}</div>
 <button  @click="passdata()"> child button </button>
</template>
<script>

export default {
    name:'userDetails',
    props: {
        name: {
            type: String,
            reqired: true,
            default: 'hi'
        }
    },
    methods:{
        passdata(){
            this.$emit('getdata',{name:'monalisa'});
        }
    }
}
</script>